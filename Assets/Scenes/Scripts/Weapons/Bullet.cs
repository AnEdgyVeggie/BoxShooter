using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpeed = 25;

    private Rigidbody _rigidBody;
    private Player _player;
    private Enemy _enemy;
    private Weapons _weapons;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();

        _weapons = GameObject.Find("Player").GetComponent<Weapons>();

        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            float damage = _weapons.GetDamage();
            _enemy.TakeDamage(damage);
           // Destroy(this.gameObject);
        }
        if (other.tag == "Enviroment")
        {
            Destroy(this.gameObject);
        }
    }
}
