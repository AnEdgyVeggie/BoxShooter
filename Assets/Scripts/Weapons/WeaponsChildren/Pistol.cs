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
        _reserveAmmo = _fullClip;
        _reloadTime = 1f;
        _damage = 10;
        _travelTime = 0.9f;
        _fireRate = 0.2f;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_weaponplatter.gameObject == false)
        {
            anim.SetBool("InInventory", true);
        }
    }

    public override void RefillAmmo()
    {
        Init();
    }

    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);
       
        transform.localPosition = new Vector3(0.288f, 0.115f, 0.723f);
        transform.localRotation = Quaternion.identity;
        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
    }

    public void SetOnStatus(bool ground)
    {
        _weaponplatter.gameObject.SetActive(ground);
    }

}
