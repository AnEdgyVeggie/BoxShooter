using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float _bulletSpeed = 30;

    protected Rigidbody _rigidBody;
    protected Player _player;
    protected Enemy _enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemy = GameObject.Find("Enemies").GetComponentInChildren<Enemy>();
        Init();
    }

    public virtual void Init()
    {
        // use Init as inherited start classs
        Destroy(this.gameObject, _player.GetTravelTime());
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        BulletTrajectory();
    }

    private void BulletTrajectory()
    {
        _rigidBody.transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
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
