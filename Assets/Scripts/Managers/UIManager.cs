using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoCounter, _ammoCounterBack, _maxAmmoCounter, _maxAmmoCounterBack, pickupWeaponText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpWeaponText(bool is_active)
    {
        pickupWeaponText.gameObject.SetActive(is_active);
    }


public void UpdateAmmo(int ammoRemaining, int maxAmmo)
    {
        _ammoCounter.text = ammoRemaining + " / " + maxAmmo;
        _ammoCounterBack.text = ammoRemaining + " / " + maxAmmo;


    }

    public void UpdateReserveAmmo(int reserveAmmo)
    {
        _maxAmmoCounter.text = reserveAmmo.ToString();
        _maxAmmoCounterBack.text = reserveAmmo.ToString();
    }
}
