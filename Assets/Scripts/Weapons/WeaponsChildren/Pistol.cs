using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    Animator anim;

    public override void Init()
    {
        _fullClip = 16;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 5;
        _reloadTime = 1f;
        _damage = 10;
        _travelTime = 0.9f;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void RefillAmmo()
    {
        Debug.LogWarning("Ammo Reload Pistol");
        _fullClip = 16;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 5;
    }

    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);
        transform.localPosition = new Vector3(0.288f, 0.115f, 0.723f);
        transform.localRotation = Quaternion.identity;
        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
        anim.SetBool("InInventory", true);
    }


}
