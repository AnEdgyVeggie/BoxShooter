using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected UIManager _uiManager;

    // set protecteds to get/set
    [SerializeField]
    protected int _fullClip = 50;     //ammo when active clip is full
    [SerializeField]
    protected int _currentClip = 50;  //ammo in active clip
    [SerializeField]
    protected int _reserveAmmo = 150;  //ammo remaining to be used
    [SerializeField]
    protected float _reloadTime = 1.5f, _travelTime = 2.5f;
    [SerializeField]
    protected float _damage = 2;


    protected Player _player;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (_uiManager == null)
        { Debug.LogError("UI manager is NULL in Weapons Script"); }

        _player = GameObject.Find("Player").GetComponent<Player>();

        Init();
    }
    public virtual void Init()
    {
        // for inherited classes to set properties
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponentInChildren<Player>();
            _uiManager.PickUpWeaponText(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                EquipPlayer(player);
                RemovePickUpWeaponUI();
            }
        }
    }

    public virtual void RefillAmmo()
    {
        _fullClip = 50;
        _currentClip = _fullClip;
        _reserveAmmo = _fullClip * 4;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.PickUpWeaponText(false);
        }
    }

    public virtual void EquipPlayer(Player player)
    {
        player.SwapWeaponWithNew(this);
        player.SetWeaponActive();
        this.transform.SetParent(player.transform, true);
    }
    void RemovePickUpWeaponUI()
    {
        _uiManager.PickUpWeaponText(false);
    }


    public void SetAmmoProperties(int weaponClip, int weaponReserves)
    {
        _currentClip = weaponClip;
        _reserveAmmo = weaponReserves;
    }
    public float GetTravelTime() { return _travelTime; }

    public float GetReloadTime() { return _reloadTime; }
    public float GetDamage() { return _damage; }
    public int GetFullClip() { return _fullClip; }
    public int GetCurrentClip() { return _currentClip; }
    public int GetReserveAmmo() { return _reserveAmmo; }

}
