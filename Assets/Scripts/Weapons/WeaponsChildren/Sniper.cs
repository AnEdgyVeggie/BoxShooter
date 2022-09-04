using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapons
{
    // Start is called before the first frame update
    public override void Init()
    {
        _fullClip = 7;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 5;
        _reloadTime = 2;
        _damage = 45;
        _travelTime = 1;
        _fireRate = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void RefillAmmo()
    {
        _fullClip = 7;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 5;
    }

    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);

        transform.localPosition = new Vector3(0.292f, 0.1215f, 1.513f);
        transform.localRotation = Quaternion.identity;
        transform.Rotate(0, 180, 0);

        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
    }

}
