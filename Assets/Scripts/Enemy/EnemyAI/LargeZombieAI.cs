using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeZombieAI : EnemyAI
{
    ZombieLarge _largeZ;

    // Start is called before the first frame update
    public override void Init()
    {
        _largeZ = GetComponentInChildren<ZombieLarge>();
        _attackTime = 2.5f;
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

    public override int GetAttackDamage()
    {
        return _largeZ.GetAttackDamage();
    }

}
