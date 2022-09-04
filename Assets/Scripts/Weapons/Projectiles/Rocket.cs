using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    AudioSource _audio;
    bool _hasExploded = false;
    [SerializeField]
    GameObject explosion;

    public override void Init()
    {
        _bulletSpeed = 15;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        RocketTrajectory();
    }

    private void RocketTrajectory()
    {
        _rigidBody.transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enviroment" || other.tag == "Enemy")
        {
            HandleExplosion();
            _bulletSpeed = 0.2f;
            
            Destroy(this.gameObject);
        }
    }

    private void HandleExplosion()
    {
        if (!_hasExploded)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            _audio.Play();
            _hasExploded = true;
        }
    }
}
