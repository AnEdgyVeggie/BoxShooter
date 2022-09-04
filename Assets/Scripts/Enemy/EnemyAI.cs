using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Navigation")]
    [SerializeField]
    protected NavMeshAgent _navMesh;
    [SerializeField]
    protected float _aggressionRange, _distanceToTarget, _attackRange, _speed = 3, _rotateSpeed = 6, _attackTime = 2;
    protected bool _canDamage = true;


    protected Animator _anim;
    protected Player _player;

    [Header("Stats")]
    protected float _health = 15;
    protected int _pointsOnHit, _pointsOnDeath, _attackDamage;

    [Header("Loot")]
    [SerializeField]
    protected Loot[] _droppable;
    [SerializeField]
    protected bool _isAttacking = false;



    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null in Enemy Script");

        _anim = GetComponent<Animator>();

        Init();
    }

    public virtual void Init()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        FindDistanceToTarget();
        LookRotation();
    }

    private void FindDistanceToTarget()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);

        if (_distanceToTarget > _navMesh.stoppingDistance)
        {
            _navMesh.destination = _player.transform.position;
        }
        else
        {
            Attack();
        }
    }

    private void LookRotation()
    {
        Vector3 targetDirection = _player.transform.position - transform.position;

        float singleStep = 1 * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public virtual void Attack()
    {

    }
    public void TakeDamage(float damage)
    {
        if (_canDamage)
        {
            _health -= damage;
            _player.IncreaseScore(_pointsOnHit);
            _canDamage = false;
            StartCoroutine(CanDamageRoutine());

            if (_health <= 0)
            {
                _player.IncreaseScore(_pointsOnDeath);
                if (Random.Range(0, 75) == 1)
                {
                    Instantiate(_droppable[Random.Range(0, _droppable.Length)], transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }

    IEnumerator CanDamageRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        _canDamage = true;
    }

    public virtual int GetAttackDamage()
    {
        return 0;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_isAttacking)
            {
                Player player = other.GetComponent<Player>();
                player.DecreaseHealth(GetAttackDamage());
            }

        }
    }

}
