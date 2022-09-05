using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : Explosion
{
    public override void Init()
    {
        base.Init();
        Debug.Log("here");
        _damage = 60;
    }

}