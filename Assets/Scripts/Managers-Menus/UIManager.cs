using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _ammoCounter, _ammoCounterBack, _maxAmmoCounter, _maxAmmoCounterBack, _pickupWeaponText, _paused,
        _weaponType, _weaponTypeBack, _scoreDisplay, _scoreDisplayBack, _round, _roundBack, _visitStore, _displayHealth, _displayArmor;
    [SerializeField]
    Store _storeUI;

    [SerializeField]
    bool _inMenu = false;

    Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GameObject.Find("GameOverScreen").GetComponent<Animator>();
       
    }

    public void PickUpWeaponText(bool is_active)
    {
        _pickupWeaponText.gameObject.SetActive(is_active);
    }

    public void UpdateAmmo(int ammoRemaining, int maxAmmo)
    {
        _ammoCounter.text = ammoRemaining + "/" + maxAmmo;
        _ammoCounterBack.text = ammoRemaining + "/" + maxAmmo;
    }

    public void UpdateReserveAmmo(string reserveAmmo)
    {
        _maxAmmoCounter.text = reserveAmmo;
        _maxAmmoCounterBack.text = reserveAmmo;
    }
    public void UpdateWeaponType(string weapon)
    {
        _weaponType.text = weapon;
        _weaponTypeBack.text = weapon;
    }
    
    public void PausedGame(bool isActive)
    {
        _paused.gameObject.SetActive(isActive);
        _inMenu = isActive;
    }
    public void DisplayScore(int score)
    {
        _scoreDisplay.text = "$" + score.ToString();
        _scoreDisplayBack.text = "$" + score.ToString();
    }
    public void DisplayRound(int round)
    {
        _round.text = "Round " + round.ToString();
        _roundBack.text = "Round " + round.ToString();
    }
    public void VisitStoreEnable(bool isActive)
    {
        _visitStore.gameObject.SetActive(isActive);
    }
    public void DisplayHealth(int health)
    {
        _displayHealth.text = "Health: " + health;
    }
    public void DisplayArmor(int armor)
    {
        _displayArmor.text = "Armor: " + armor;
    }

    public void GameOverScreen()
    {
        _anim.SetTrigger("GameOver");
    }

    public bool GetInMenu() { return _inMenu; }
    public void SetInMenu(bool isActive) { _inMenu = isActive; }
}
