using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // WEAPON AND AMMUNITION VARIABLES
    [SerializeField]
    GameObject _bulletPrefab;
    
    [SerializeField]
    Weapons[] _weaponInventory;
    
    [SerializeField]
    int _currentClip, _reserveAmmo, _fullClip;
    float _reloadTime, _damage;
    [SerializeField]
    bool _isReloading = false, _canfire = true, _weaponsActive = false;


    // GameObject Scripts
    Player _player;
    UIManager _uiManager;
    PlayerAnimation _playAnim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();

        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        _playAnim = GetComponent<PlayerAnimation>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        MouseVisibility();

        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeaponInventory();
        }

    }

    void MouseVisibility()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { Cursor.visible = true; }
        else if (Input.GetMouseButtonDown(0))
        { Cursor.visible = false; }
    }
    

    void FireWeapon()
    {
       ////ector3 weaponPosition = _weaponOne.transform.position;     
       // Vector3 playerRotation = _player.transform.eulerAngles;

       // if (Input.GetMouseButtonDown(0) && _isReloading == false && _canfire == true)
       // {
       //     Instantiate(_bulletPrefab, new Vector3(weaponPosition.x, weaponPosition.y, weaponPosition.z), Quaternion.Euler(0, playerRotation.y + 90, 90));


       //     _ammoLoaded--;
       //     _uiManager.UpdateAmmo(_ammoLoaded, _weaponAmmo);

       //     if (_ammoLoaded < 1)
       //     {
       //         _canfire = false;
       //     }
       // }
       // if ((_ammoLoaded < _weaponAmmo && Input.GetKeyDown(KeyCode.R)) || _ammoLoaded == 0 && Input.GetMouseButtonDown(0))
       // {
       //     StartCoroutine(WeaponReloadRoutine());
    }
    
    public void SwapWeaponWithNew(Weapons swapped)
    {
        if (_weaponInventory[0])
        {
            _weaponInventory[1] = _weaponInventory[0];
        }

        _weaponInventory[0] = swapped;
        WeaponStatsGetters();
        HideSecondWeapon();

    }

    public void SwapWeaponInventory()
    {
        Weapons temp = _weaponInventory[1];
        _weaponInventory[1] = _weaponInventory[0];
        _weaponInventory[0] = temp;
        HideSecondWeapon();
 
    }

    public void SetWeaponActive()
    {
        if (_weaponsActive == false)
        {
            _weaponsActive = true;
        }
    }

    private void WeaponStatsGetters()
    {
        this._damage = _weaponInventory[0].GetDamage();
        this._currentClip = _weaponInventory[0].GetCurrentClip();
        this._reserveAmmo = _weaponInventory[0].GetReserveAmmo();
        this._reloadTime = _weaponInventory[0].GetReloadTime();
        this._fullClip = _weaponInventory[0].GetFullClip();
    }

    private void HideSecondWeapon()
    {
        if (_weaponInventory[1])
        {
            _weaponInventory[1].gameObject.SetActive(false);
        }
        if (_weaponInventory[0])
        {
            _weaponInventory[0].gameObject.SetActive(true);
        }
    }
}
