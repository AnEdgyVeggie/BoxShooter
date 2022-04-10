using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{

    public override void Init()
    {
        _fullClip = 30;
        _currentClip = 30;
        _reserveAmmo = 90;
        _reloadTime = 1f;
        _damage = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);
        transform.localPosition = new Vector3(0.288f, 0.115f, 0.723f);
        transform.localRotation = Quaternion.identity;
        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
    }

}
