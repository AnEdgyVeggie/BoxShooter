using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieAI : EnemyAI
{
    BasicZombie _basicZ;

    public override void Init()
    {
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
        _isAttacking = false;
        yield return new WaitForSeconds(_attackTime);

    }

    public override int GetAttackDamage()
    {
        return _basicZ.GetAttackDamage();
    }


}
