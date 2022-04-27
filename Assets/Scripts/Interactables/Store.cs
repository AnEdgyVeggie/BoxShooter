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

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.VisitStoreEnable(true);
            Player player = other.GetComponent<Player>();
            if (Input.GetKey(KeyCode.E))
            {
                _uiManager.SetInMenu(true);
                _uiManager.gameObject.SetActive(false);
                _storeUI.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                player.SetPaused(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
        Player player = other.GetComponent<Player>();

        _uiManager.VisitStoreEnable(false);
        ExitMenu();
        }
    }

    public void ExitMenu()
    {
       _player.HideCursor();
        _uiManager.gameObject.SetActive(true);
        _uiManager.SetInMenu(false);
        _storeUI.gameObject.SetActive(false);
        _player.SetPaused(false);
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
            _player.AddArmor();
            _player.DecreaseScore(1500);
        }
        else
        {
            StartCoroutine(NotEnoughMoneyRoutine());
        }
    }
    public void BuyAmmo()
    {
        if (_player.GetScore() > 500)
        {
            _player.RefillAmmo();
            _player.DecreaseScore(500);
        }
        else
        {
            StartCoroutine(NotEnoughMoneyRoutine());
        }
    }

    IEnumerator NotEnoughMoneyRoutine()
    {
        _notEnoughMoney.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _notEnoughMoney.gameObject.SetActive(false);
    }
}
