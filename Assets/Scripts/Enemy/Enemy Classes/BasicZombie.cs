using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombie : Enemy
{
    public override void Init()
    {
        _pointsOnDeath = 25;
        _pointsOnHit = 5;
        _attackDamage = 20;
    }
}
