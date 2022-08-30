using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLarge : Enemy
{
    public override void Init()
    {
        _health = 35;
        _pointsOnDeath = 60;
        _pointsOnHit = 10;
        _attackDamage = 45;
    }

}
