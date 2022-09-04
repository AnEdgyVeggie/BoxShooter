using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapons
{
    Animator anim;
    public override void Init()
    {
        _fullClip = 25;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 4;
        _reloadTime = 2.5f;
        _damage = 17.5f;
        _travelTime = 1.3f;
        _fireRate = 0.3f;

    }
    private void Update()
    {

    }

    public override void RefillAmmo()
    {
        _fullClip = 25;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 4;
    }

    public override void EquipPlayer(Player player)
    {

        base.EquipPlayer(player);
        
        transform.localPosition = new Vector3(0.288f, 0.115f, 1.11f);
        transform.localRotation = Quaternion.identity;
        transform.Rotate(0, 180, 0);
        
        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");

    }

}
