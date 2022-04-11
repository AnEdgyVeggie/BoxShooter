using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapons
{
    Animator anim;
    // Start is called before the first frame update
    public override void Init()
    {
        _fullClip = 50;
        _currentClip = 50;
        _reserveAmmo = 150;
        _reloadTime = 2.5f;
        _damage = 17.5f;
        anim = GetComponent<Animator>();
    }



    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);
        transform.localPosition = new Vector3(0.288f, 0.115f, 1.11f);
        transform.localRotation = Quaternion.identity;
        transform.Rotate(0, 180, 0);
        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
        anim.SetBool("InInventory", true);
    }
}
