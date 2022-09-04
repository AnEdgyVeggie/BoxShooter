using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    protected float _damage = 40;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(_damage);
        }
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.DecreaseHealth((int)_damage);

        }
        Destroy(gameObject, 3f);
    }

}
