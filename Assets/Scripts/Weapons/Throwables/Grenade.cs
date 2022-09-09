using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Rocket
{

    float _damage = 1;
    bool initialForce = false;
    public float mtimeToExplosion = 0;
    // Start is called before the first frame update

    public override void Init()
    {
        _bulletSpeed = 10;
        //Debug.Log($"Thrown with {mtimeToExplosion} time left");
        StartCoroutine(ExplosionCountdownRoutine());
    }
    // Update is called once per frame
    public override void FixedUpdate()
    {
        GrenadeTrajectory();
    }

    private void GrenadeTrajectory()
    {
        {
            if (!initialForce)
            {
                _rigidBody.AddForce(_player.transform.forward * _bulletSpeed, ForceMode.Impulse);
                initialForce = true;
            }
        }
    }

    IEnumerator ExplosionCountdownRoutine()
    {
        if (mtimeToExplosion > 0)
        {
            yield return new WaitForSeconds(0.1f);
            mtimeToExplosion -= 0.1f;
            //Debug.Log(_timeUntilExplode);
            StartCoroutine(ExplosionCountdownRoutine());
        }
        else
        {
            HandleExplosion();
        }
    }

    public override void HandleExplosion()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter(Collider other)
    {
        return;
    }
}
