using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieAI : EnemyAI
{
    protected Animator _anim;
    BasicZombie _basicZ;

    public override void Init()
    {
        _anim = GetComponent<Animator>();

        _basicZ = GetComponentInChildren<BasicZombie>();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        if (!_isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }

    }

    IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        this._anim.SetTrigger("Attack");
        yield return new WaitForSeconds(2);
        _isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_isAttacking)
            {
                Player player = other.GetComponent<Player>();
                player.DecreaseHealth(_basicZ.GetAttackDamage());
            }
           
        }
    }
}
