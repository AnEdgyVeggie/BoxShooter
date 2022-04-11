using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _health = 15, _speed = 3;
    protected int _pointsOnHit, _pointsOnDeath;

    protected GameObject[] _loot;
    protected Player _player;
    protected EnemySpawnManager _enSpawn;
    protected bool _canDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null in Enemy Script");

        _enSpawn = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        Init();
    }

    public virtual void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
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
                Destroy(this.gameObject);
                _player.IncreaseScore(_pointsOnDeath);
                _enSpawn.DecrementEnemiesAlive();
            }
        }
    }

    IEnumerator CanDamageRoutine()
    {
        yield return new WaitForSeconds(0.075f);
        _canDamage = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            bullet.SetSpeed(0);
            TakeDamage(_player.GetDamage());

            Destroy(bullet.gameObject);
        }
    }
}
