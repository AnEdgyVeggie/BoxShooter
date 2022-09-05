using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    protected float _damage = 40;
    SphereCollider _sphereCollider;


    public void Start()
    {
        Init();
    }
    public virtual void Init()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine(DisableColliderRoutine());
    }

    IEnumerator DisableColliderRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        _sphereCollider.enabled = false;
    }

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
