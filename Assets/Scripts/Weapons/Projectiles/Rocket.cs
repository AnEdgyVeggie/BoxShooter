using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    Animator _anim;
    AudioSource _audio;

    public override void Init()
    {
        _bulletSpeed = 15;
        _anim = GetComponent<Animator>();
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
            _anim.SetTrigger("Explode");
            _audio.Play();
            _bulletSpeed = 0;
            Destroy(this.gameObject, 2f);
        }
    }
}
