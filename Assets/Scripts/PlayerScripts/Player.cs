using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    // WEAPON AND AMMUNITION VARIABLES
    [Header("Weapon Stats")]
    int _currentClip, _reserveAmmo, _fullClip;
    [SerializeField]
    float _reloadTime, _damage, _travelTime;
    [SerializeField]
    bool _isReloading = false, _canfire = true, _isRPG = false, _damaged = false;

    // SCORE AND OBJECTIVE VARIABLES
    [Header("Score and Objective Variables")]
    int _score = 0,  _maxHealth = 150;
    int _armor = 0, _armorTier = 0, _maxArmor = 50;
    [SerializeField]
    int _damagedCounter = 0; 
    float _healTime = 7.5f;
    [SerializeField]
    int _health = 150;
    bool _gameOver = false;


    [Header("Prefabs")]
    [SerializeField]
    GameObject _bulletPrefab, _rocketPrefab;
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
    [SerializeField]
    protected NavMeshAgent _navMesh;


    // GAME MANAGER VARIABLES
    bool _paused = false;

    void Start()
    {
        _player = GetComponent<Player>();

        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        _playAnim = GetComponent<PlayerAnimation>();

        _audio = this.GetComponent<AudioSource>();
        HideCursor();

        WeaponStatsGetters();
       
        _uiManager.DisplayArmor(_armor);
        _uiManager.DisplayHealth(_health);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameOver)
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

                if (_weaponInventory[0].name == "Unarmed")
                {
                    _laser.gameObject.SetActive(false);
                }
                else
                {
                    _laser.gameObject.SetActive(true);
                }
            }

            if (_weaponInventory[0].name == "RPG")
            {
                _laser.gameObject.SetActive(false);
                RocketLauncher rpg = _weaponInventory[0] as RocketLauncher;
                rpg.TurnOnLaser();
                _isRPG = true;
            }
            else
            {
                _laser.gameObject.SetActive(true);
                _isRPG = false;
            }
        }

    }

    /// WEAPON METHODS
    /// All methods dealing with weapon function, stats,
    /// variables, etc.

    void FireWeapon()
    {
        Vector3 weaponPosition = _weaponInventory[0].transform.position;
        Vector3 playerRotation = _player.transform.eulerAngles;

        if (Input.GetMouseButtonDown(0) && _isReloading == false && _canfire == true)
        {
            if (_isRPG == false)
            {
                Instantiate(_bulletPrefab, new Vector3(weaponPosition.x, weaponPosition.y, weaponPosition.z), Quaternion.Euler(0, playerRotation.y + 90, 90));
                _audio.Play();
            }
            else
            {
                Instantiate(_rocketPrefab, new Vector3(weaponPosition.x, weaponPosition.y, weaponPosition.z), Quaternion.Euler(0, playerRotation.y + 90, 90));
            }


            _currentClip--;
            _uiManager.UpdateAmmo(_currentClip, _fullClip);

            if (_currentClip == 0)
            {
                _canfire = false;
            }
        }
        if ((_currentClip < _fullClip && Input.GetKeyDown(KeyCode.R)) || _currentClip == 0 && Input.GetMouseButtonDown(0) && _reserveAmmo > 0)
        {
            StartCoroutine(WeaponReloadRoutine());
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
    public void RefillAmmo()
    {
        _weaponInventory[0].RefillAmmo();
        _weaponInventory[1].RefillAmmo();
        WeaponStatsGetters();
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
    
    /// WEAPON STAT GETTERS
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
    public float GetTravelTime() {   return _travelTime; }


    /// OBJECTIVE METHODS
    /// Score, health, and methods dealing in
    /// survival or core gameplay

    public void DecreaseHealth(int hitpoints)
    {
        _damagedCounter++;
        _damaged = true;
        StartCoroutine(SetDamagedRoutine());

        if (_armor > 0)
        {
            hitpoints = hitpoints / 2;
            _armor -= hitpoints;
        }
        _health -= hitpoints;

        if (_health < 1)
        {
            _health = 0;
            GameOver();
        }

        _uiManager.DisplayArmor(_armor);
        _uiManager.DisplayHealth(_health);
    }

    IEnumerator SetDamagedRoutine()
    {
        yield return new WaitForSeconds(_healTime);
        _damagedCounter--;
        if (_damagedCounter == 0)
        {
            _damaged = false;
            RegainHealth();
        }
    }
    private void RegainHealth()
    { 
        if (!_damaged)
        {
            StartCoroutine(RegainHealthRoutine());
        }
    }
    IEnumerator RegainHealthRoutine()
    {
         while (_health < _maxHealth)
        {
            yield return new WaitForSeconds(0.1f);
            _health++;
            _uiManager.DisplayHealth(_health);
        }
    }

    public void AddArmor()
    {
        if (_armorTier < 3)
        {
            _armorTier++;
            _maxArmor = _armorTier * 50;
        }
        if (_armor < _maxArmor)
        {
            _armor += 50;
            if (_armor > _maxArmor)
            {
                _armor = _maxArmor;
            }
        }
        _uiManager.DisplayArmor(_armor);
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
    public int GetScore() { return _score; }

    public void GameOver()
    {
        _uiManager.GameOverScreen();
        _gameOver = true;
    }
    public bool GetGameOver()
    {
        return _gameOver;
    }

    /// Additional functions


    public void SetPaused(bool pauseGame)
    {
        _paused = pauseGame;
    }
    public bool GetPaused() { return _paused; }
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


}
