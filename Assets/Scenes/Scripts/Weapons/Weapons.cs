using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected UIManager _uiManager;

    // set publics to get/set
    [SerializeField]
    protected int _fullClip = 50;     //ammo when active clip is full
    [SerializeField]
    protected int _currentClip = 50;  //ammo in active clip
    [SerializeField]
    protected int _reserveAmmo = 150;  //ammo remaining to be used
    [SerializeField]
    protected float _reloadTime = 1.5f;
    [SerializeField]
    protected float _damage = 2;

    protected BoxCollider boxCollider;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (_uiManager == null)
        { Debug.LogError("UI manager is NULL in Weapons Script"); }

        boxCollider = GetComponent<BoxCollider>();

        Init();
    }
    public virtual void Init()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.PickUpWeaponText(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponentInChildren<Player>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                EquipPlayer(player);
                StartCoroutine(RemoveColliderRoutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.PickUpWeaponText(false);
            
        }
    }

    public virtual void EquipPlayer(Player player)
    {
        player.SwapWeaponWithNew(this);
        player.SetWeaponActive();
        this.transform.SetParent(player.transform, true);
    }


    public float GetReloadTime() { return _reloadTime; }
    public float GetDamage() { return _damage; }
    public int GetFullClip() { return _fullClip; }
    public int GetCurrentClip() { return _currentClip; }
    public int GetReserveAmmo() { return _reserveAmmo; }

    IEnumerator RemoveColliderRoutine()
    {
        boxCollider.center = transform.position + new Vector3(20, 0, 0);
        yield return new WaitForSeconds(.1f);
        Destroy(boxCollider);
    }

}
