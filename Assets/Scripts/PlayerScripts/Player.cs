using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // WEAPON AND AMMUNITION VARIABLES
    [Header("Weapon Stats")]
    [SerializeField]
    int _currentClip, _reserveAmmo, _fullClip;
    [SerializeField]
    float _reloadTime, _damage, _travelTime;
    [SerializeField]
    bool _isReloading = false, _canfire = true, _weaponsActive = false;

    // SCORE AND OBJECTIVE VARIABLES
    [Header("Score and Objective Variables")]
    [SerializeField]
    int _score = 0;

    [Header("Prefabs")]
    [SerializeField]
    GameObject _bulletPrefab;
    [SerializeField]
    GameObject _laser;
    [SerializeField]
    Weapons[] _weaponInventory;

    // COMPONENT VARIABLES
    [Header("Components")]
    Player _player;
    UIManager _uiManager;
    PlayerAnimation _playAnim;
    AudioSource _audio;

    // GAME MANAGER VARIABLES
    bool _paused = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();

        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        _playAnim = GetComponent<PlayerAnimation>();

        _audio = this.GetComponent<AudioSource>();
        HideCursor();

        WeaponStatsGetters();

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
                _audio.Play();

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
        if (_weaponInventory[0].name == "Unarmed")
        {
            _weaponInventory[0] = swapped;
        }
        else if (_weaponInventory[0].name != "Unarmed" && _weaponInventory[1].name == "Unarmed")
        {
            _weaponInventory[1] = _weaponInventory[0];
            _weaponInventory[0] = swapped;
        }
        WeaponStatsGetters();
        HideSecondWeapon();
    }

    private void SwapWeaponInventory()
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
        _score +=  points;
        _uiManager.DisplayScore(_score);
    }
    public void DecreaseScore(int points)
    {
        _score -= points;
        _uiManager.DisplayScore(_score);

    }

    public void RefillAmmo()
    {
        Debug.LogError("Ammo Reload Player");
        _weaponInventory[0].RefillAmmo();
        _weaponInventory[1].RefillAmmo();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public bool CheckWeaponsInInventory(Weapons swappable)
    {
        foreach (Weapons weap in _weaponInventory)
        {
            if (weap.GetType() == swappable.GetType())
            {
                return true;
            }
        }
        return false;
    }

    // Getters
    private void WeaponStatsGetters()
    {
        this._damage = _weaponInventory[0].GetDamage();
        this._currentClip = _weaponInventory[0].GetCurrentClip();
        this._reserveAmmo = _weaponInventory[0].GetReserveAmmo();
        this._reloadTime = _weaponInventory[0].GetReloadTime();
        this._fullClip = _weaponInventory[0].GetFullClip();
        this._travelTime = _weaponInventory[0].GetTravelTime();

        _canfire = (_reserveAmmo > 0) ?  true : false;

        _uiManager.UpdateAmmo(_currentClip, _fullClip);
        _uiManager.UpdateReserveAmmo(_reserveAmmo);
        _uiManager.UpdateWeaponType(_weaponInventory[0].name);
    }

    public float GetDamage() { return _damage; }
    public float GetTravelTime() { return _travelTime; }
    public int GetScore() { return _score; }
}
