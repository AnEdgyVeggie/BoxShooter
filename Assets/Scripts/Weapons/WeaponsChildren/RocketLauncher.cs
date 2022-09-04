using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapons
{
    [SerializeField]
    GameObject _laserPrefab;
    
    public override void Init()
    {
        _fullClip = 1;
        _currentClip = 1;
        _reserveAmmo = _fullClip * 5;
        _reloadTime = 3;
        _damage = 50;
        _travelTime = 10;
    }

    private void Update()
    {

    }

    public override void RefillAmmo()
    {
        Init();
    }

    public override void EquipPlayer(Player player)
    {
        base.EquipPlayer(player);

        transform.localPosition = new Vector3(0.604f, 0.115f, 0.47f);
        transform.localRotation = Quaternion.identity;
        transform.Rotate(0, 90, 90);
        this.gameObject.layer = LayerMask.NameToLayer("EquippedWeapon");
    }

    public void TurnOnLaser()
    {
        _laserPrefab.SetActive(true);
    }
}
