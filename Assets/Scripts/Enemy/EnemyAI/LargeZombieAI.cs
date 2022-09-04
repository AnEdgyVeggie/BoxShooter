using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeZombieAI : EnemyAI
{

    // Start is called before the first frame update
    public override void Init()
    {
        _attackTime = 2.5f;
        _health = 35;
        _pointsOnDeath = 60;
        _pointsOnHit = 10;
        _attackDamage = 45;
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
        // animation
        _isAttacking = false;
        yield return new WaitForSeconds(_attackTime); 
    }
}
