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

    public override void RefillAmmo()
    {
        Init();
    }

    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);
       
        transform.localPosition = new Vector3(0.288f, 0.115f, 0.723f);
        transform.localRotation = Quaternion.identity;
        gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
    }
    public override void ReloadWeapon()
    {
        StartCoroutine(PistolReloadRoutine());
    }
    IEnumerator PistolReloadRoutine() 
    {
         yield return new WaitForSeconds(_reloadTime);
        _currentClip = _fullClip;
        _player.SetIsReloading(false);
        _player.SetCanFire(true);
        _player.WeaponStatsGetters();
        _uiManager.UpdateAmmo(_currentClip, _fullClip);
    }


}
