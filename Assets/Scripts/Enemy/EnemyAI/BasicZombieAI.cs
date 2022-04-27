using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieAI : EnemyAI
{
    protected Animator _anim;

    public override void Init()
    {
        _anim = GetComponent<Animator>();

        _speed = 2.5f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        _anim.enabled = true;
        yield return new WaitForSeconds(1.433f);
        _anim.SetTrigger("Attack");
        _anim.enabled = false;
    }
}
