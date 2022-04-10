using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _bulletSpeed = 20;

    Rigidbody _rigidBody;
    Player _player;
    Enemy _enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemy = GameObject.Find("Enemies").GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BulletTrajectory();
    }

    void BulletTrajectory()
    {
        _rigidBody.transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            float damage = _player.GetDamage();
            _enemy.TakeDamage(damage);
            Debug.Log("Hit Before");
            Destroy(this.gameObject);
            _bulletSpeed = 0;
            Debug.Log("Hit After");
        }

        if (other.tag == "Enviroment")
        {
            Debug.LogWarning("BULLET HIT WALL");
            Destroy(this.gameObject);
        }
    }
}
