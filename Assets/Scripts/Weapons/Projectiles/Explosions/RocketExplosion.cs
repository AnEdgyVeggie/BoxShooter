using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    private float _damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            
        }
        if (other.tag == "Player")
        {

        }
    }

}
