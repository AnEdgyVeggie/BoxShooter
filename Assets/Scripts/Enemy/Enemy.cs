using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _health = 15;
    protected float _speed = 3;
    protected GameObject[] _loot;
    protected Player _player;
    protected EnemySpawnManager _enSpawn;
    protected bool _canDamage = true;

    public float Health { get; set; }
    public float Speed { get; set; }

    public Enemy()
    {  
        Health = _health;
        Speed = _speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null in Enemy Script");

        _enSpawn = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        if (_canDamage)
        {
            Health -= damage;
            _canDamage = false;
            StartCoroutine(CanDamageRoutine());
            Debug.LogWarning("DAMAGED");
        }
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            _enSpawn.DecrementEnemiesAlive();
        }    
    }

    IEnumerator CanDamageRoutine()
    {
        yield return new WaitForSeconds(0.05f);
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
