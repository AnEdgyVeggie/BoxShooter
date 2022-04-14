using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _health = 15;
    protected int _pointsOnHit, _pointsOnDeath;

    protected GameObject[] _loot;
    protected Player _player;
    protected EnemySpawnManager _enSpawn;
    protected bool _canDamage = true;

    protected EnemyAI _AI;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null in Enemy Script");

        _enSpawn = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        Init();

        _AI = GetComponentInParent<EnemyAI>();
    }

    public virtual void Init()
    {

    }

    // Update is called once per frame

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
                _enSpawn.DecrementEnemiesAlive();
                _AI.DestroyEnemy();
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
            TakeDamage(
                _player.
                GetDamage()
                );

            Destroy(bullet.gameObject);
        }
    }
}
