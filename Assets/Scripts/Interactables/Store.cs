using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField]
    Text _notEnoughMoney;

    [SerializeField]
    UIManager _uiManager;
    [SerializeField]
    Rifle _riflePurchasable;
    [SerializeField]
    Pistol _pistolPurchasable;  
    [SerializeField]
    Store _storeUI;
    [SerializeField]
    Player _player;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.VisitStoreEnable(true);
            if (Input.GetKey(KeyCode.E))
            {
                _uiManager.SetInMenu(true);
                _uiManager.gameObject.SetActive(false);
                _storeUI.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
            _uiManager.VisitStoreEnable(false);
    }

    public void ExitMenu()
    {
        _player.HideCursor();
        _uiManager.gameObject.SetActive(true);
        _uiManager.SetInMenu(false);
        _storeUI.gameObject.SetActive(false);

    }

    public void BuyRifle()
    {
        Rifle newRifle = _riflePurchasable;
        if (_player.GetScore() > 2000 && _player.CheckWeaponsInInventory(newRifle) == false)
        {

            newRifle = Instantiate(_riflePurchasable);
            newRifle.name = "Rifle";
            newRifle.SetOnStatus(false);
            newRifle.EquipPlayer(_player);
            _player.DecreaseScore(2000);
        }
        else if (_player.GetScore() < 2000)
        {
            StartCoroutine(NotEnoughMoneyRoutine());
        } 
        else
        {
            return;
            //weapon alrady in inventory
        }
        
    }
    public void BuyPistol()
    {
        Pistol newPistol = _pistolPurchasable;
        if (_player.GetScore() > 800 && _player.CheckWeaponsInInventory(newPistol) == false)
        {
            newPistol = Instantiate(_pistolPurchasable);
            newPistol.name = "Pistol";
            newPistol.SetOnStatus(false);
            newPistol.EquipPlayer(_player);
            _player.DecreaseScore(800);
        }
        else if (_player.GetScore() < 800)
        {
            Destroy(newPistol);
            StartCoroutine(NotEnoughMoneyRoutine());
        }
        else
        {
            return;
            //weapon already in inventory
        }
    }
    public void BuyArmor()
    {
        if (_player.GetScore() > 1500)
        {

        }
    }
    public void BuyAmmo()
    {
        if (_player.GetScore() > 500)
        {
            _player.RefillAmmo();
        }
        else
        {
            StartCoroutine(NotEnoughMoneyRoutine());
            _player.DecreaseScore(500);
        }

    }

    IEnumerator NotEnoughMoneyRoutine()
    {
        _notEnoughMoney.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _notEnoughMoney.gameObject.SetActive(false);
    }
}
