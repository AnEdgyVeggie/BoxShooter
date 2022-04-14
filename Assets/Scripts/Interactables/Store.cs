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
    Weapons[] _weaponsPurchasable;  // Rifle[0], pistol[1],
    [SerializeField]
    Store _storeUI;
    [SerializeField]
    Player _player;

    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
        if (_player.GetScore() > 2000)
        {
            _weaponsPurchasable[0].EquipPlayer(_player);
            _player.DecreaseScore(2000);
        }
        else
        {
            StartCoroutine(NotEnoughMoneyRoutine());
        }
    }
    public void BuyPistol()
    {
        if (_player.GetScore() > 800)
        {
            _weaponsPurchasable[1].EquipPlayer(_player);
            _player.DecreaseScore(800);
        }
        else 
        {
            StartCoroutine(NotEnoughMoneyRoutine());
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
        }

    }

    IEnumerator NotEnoughMoneyRoutine()
    {
        _notEnoughMoney.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _notEnoughMoney.gameObject.SetActive(false);
    }
}
