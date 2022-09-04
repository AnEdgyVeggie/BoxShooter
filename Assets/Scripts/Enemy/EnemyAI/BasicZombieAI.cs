using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieAI : EnemyAI
{
    public override void Init()
    {
        _pointsOnDeath = 25;
        _pointsOnHit = 5;
        _attackDamage = 20;
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
}
