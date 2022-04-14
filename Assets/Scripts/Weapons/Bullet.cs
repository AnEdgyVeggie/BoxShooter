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
        Destroy(this.gameObject, _player.GetTravelTime());
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enviroment")
        { 
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        _bulletSpeed = speed;
    }
}
