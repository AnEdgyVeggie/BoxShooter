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
    protected float _aggressionRange, _distanceToTarget, _attackRange;
    protected Animator _anim;
    protected float _speed = 3, _rotateSpeed = 5, _attackTime =2;
    protected Player _player;
    [SerializeField]
    protected bool _atEnviroment = false, _atPlayer = false, _isAttacking = false;

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
        this._distanceToTarget = Vector3.Distance(this.transform.position, this._player.transform.position);

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

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
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
