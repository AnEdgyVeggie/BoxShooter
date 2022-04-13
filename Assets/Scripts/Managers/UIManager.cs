using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _ammoCounter, _ammoCounterBack, _maxAmmoCounter, _maxAmmoCounterBack, _pickupWeaponText, _paused,
        _weaponType, _weaponTypeBack, _scoreDisplay, _scoreDisplayBack, _round, _roundBack;


    // Start is called before the first frame update
    void Start()
    {
        _weaponType.text = "Empty";
        _scoreDisplay.text = "$0";
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

    public void UpdateReserveAmmo(int reserveAmmo)
    {
        _maxAmmoCounter.text = reserveAmmo.ToString();
        _maxAmmoCounterBack.text = reserveAmmo.ToString();
    }
    public void UpdateWeaponType(string weapon)
    {
        _weaponType.text = weapon;
        _weaponTypeBack.text = weapon;
    }
    
    public void PausedGame(bool isActive)
    {
        _paused.gameObject.SetActive(isActive);
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
}
