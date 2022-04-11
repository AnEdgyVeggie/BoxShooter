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
    GameObject _laser;
    
    [SerializeField]
    int _currentClip, _reserveAmmo, _fullClip;
    [SerializeField]
    float _reloadTime, _damage;
    [SerializeField]
    bool _isReloading = false, _canfire = true, _weaponsActive = false;

    // SCORE AND OBJECTIVE VARIABLES
    [SerializeField]
    int _score = 0;


    // GAMEOBJECT VARIABLES
    Player _player;
    UIManager _uiManager;
    PlayerAnimation _playAnim;

    // GAME MANAGER VARIABLES
    bool _paused = false;

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
        if (!_paused) {

            if (Input.GetMouseButtonDown(0) && _currentClip > 0)
            {
                FireWeapon();
            }


            if (Input.GetAxisRaw("Scroll") > 0 || Input.GetAxisRaw("Scroll") < 0)
            {
                SwapWeaponInventory();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadWeapon();
            }
        }

    }


    void FireWeapon()
    {
        if (_weaponsActive)
        {
            Vector3 weaponPosition = _weaponInventory[0].transform.position;
            Vector3 playerRotation = _player.transform.eulerAngles;

            if (Input.GetMouseButtonDown(0) && _isReloading == false && _canfire == true)
            {
                Instantiate(_bulletPrefab, new Vector3(weaponPosition.x, weaponPosition.y, weaponPosition.z), Quaternion.Euler(0, playerRotation.y + 90, 90));


                _currentClip--;
                _uiManager.UpdateAmmo(_currentClip, _fullClip);

                if (_currentClip == 0)
                {
                    _canfire = false;
                    Debug.Log("Out of ammo");
                }
            }
            if ((_currentClip < _reserveAmmo && Input.GetKeyDown(KeyCode.R)) || _currentClip == 0 && Input.GetMouseButtonDown(0) && _reserveAmmo > 0)
            {
                StartCoroutine(WeaponReloadRoutine());
            }
        }
    }

    void ReloadWeapon()
    {
        if (!_isReloading)
        {
            _canfire = false;
            _isReloading = true;
            StartCoroutine(WeaponReloadRoutine());
        }
        else
        {
            return;
        }
    }

    IEnumerator WeaponReloadRoutine()
    {
        int refillAmmo = _fullClip - _currentClip;
        yield return new WaitForSeconds(_reloadTime);
        if (_reserveAmmo >= refillAmmo) {
            _reserveAmmo -= (_fullClip - _currentClip);
            _currentClip = _fullClip;
        } 
        else
        {
            _currentClip = _reserveAmmo;
            _reserveAmmo = 0;
        }
        _isReloading = false;
        _canfire = true;
        _uiManager.UpdateAmmo(_currentClip, _fullClip);
        _uiManager.UpdateReserveAmmo(_reserveAmmo);
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
        _weaponInventory[0].SetAmmoProperties(_currentClip, _reserveAmmo);
        Weapons temp = _weaponInventory[1];
        _weaponInventory[1] = _weaponInventory[0];
        _weaponInventory[0] = temp;
        HideSecondWeapon();
        WeaponStatsGetters();
    }

    public void SetWeaponActive()
    {
        if (_weaponsActive == false)
        {
            _weaponsActive = true;
            _laser.SetActive(true);
        }
    }

    private void WeaponStatsGetters()
    {
        this._damage = _weaponInventory[0].GetDamage();
        this._currentClip = _weaponInventory[0].GetCurrentClip();
        this._reserveAmmo = _weaponInventory[0].GetReserveAmmo();
        this._reloadTime = _weaponInventory[0].GetReloadTime();
        this._fullClip = _weaponInventory[0].GetFullClip();

        _canfire = (_reserveAmmo > 0) ?  true : false;

        _uiManager.UpdateAmmo(_currentClip, _fullClip);
        _uiManager.UpdateReserveAmmo(_reserveAmmo);
        _uiManager.UpdateWeaponType(_weaponInventory[0].name);
    }
    public float GetDamage() { return _damage; }


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

    public void SetPaused(bool pauseGame)
    {
        _paused = pauseGame;
    }

    public void IncreaseScore(int points)
    { 
        _score = _score +  points;
        _uiManager.DisplayScore(_score);
    }


}
