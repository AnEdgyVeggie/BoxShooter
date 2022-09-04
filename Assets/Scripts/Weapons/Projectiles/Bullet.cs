using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float _bulletSpeed = 30;
    public float mdamage;
    protected bool _canDamage = true;

    protected Rigidbody _rigidBody;
    protected Player _player;
    protected EnemyAI _enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemy = GameObject.Find("Enemies").GetComponentInChildren<EnemyAI>();
        Init();
    }

    public virtual void Init()
    {
        // use Init as inherited start classs
        Destroy(gameObject, _player.GetTravelTime());

    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        BulletTrajectory();
    }

    private void BulletTrajectory()
    {
        _rigidBody.transform.Translate
            (Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    public void SetDamage(float damage)
    {
        mdamage = damage;
        Debug.Log(mdamage);
    }
    public void SetSpeed(float speed)
    {
        _bulletSpeed = speed;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enviroment")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy")
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (_canDamage == true)
            {
                _canDamage = false;
                enemy.TakeDamage(mdamage);
            }
            Destroy(gameObject);
        }
    }
}
