using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeapon : Weapons
{
    // Start is called before the first frame update
    public override void Init()
    {
        _fullClip = 0;
        _currentClip = 0;
        _reserveAmmo = 0;
        _reloadTime = 0;
        _damage = 0;
        _travelTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
