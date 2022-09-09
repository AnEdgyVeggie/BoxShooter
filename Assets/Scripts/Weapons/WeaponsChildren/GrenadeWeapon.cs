using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeWeapon : Weapons
{
    [SerializeField]
    float _timeUntilExplode = 1.5f, _timeUntilExplodeDefault;
    [SerializeField]
    bool _pinPulled = false, _grenadeThrown = false, _explodedInHand = false;
    [SerializeField]
    Grenade _throwable;
    [SerializeField]
    Explosion _grenadeExplosion;

    // Start is called before the first frame update
    public override void Init()
    {
        _fullClip = 1;
        _currentClip = 20;
        _reserveAmmo = 0;
        _damage = 2;
        _travelTime = 3.5f;
        _fireRate = 0.1f;
        _timeUntilExplodeDefault = _timeUntilExplode;
    }

    public override void FireWeapon()
    {
        if (_currentClip > 0 && !_explodedInHand)
        {
            Grenade grenade = _throwable;
            Instantiate(grenade, transform.position, Quaternion.Euler(0, _player.GetPlayerRotation().y + 90, 90));
            grenade.GetComponent<Grenade>().mdamage = _damage;
            grenade.GetComponent<Grenade>().mtimeToExplosion = _timeUntilExplode;
            _pinPulled = false;
            _currentClip--;
            _timeUntilExplode = _timeUntilExplodeDefault;
        }
        if (_currentClip > 0 && _explodedInHand)
        {
            StartCoroutine(ResetExplodedInHandRoutine());
        }
    }
  

    IEnumerator ResetExplodedInHandRoutine()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonUp(2));
        _explodedInHand = false;
    }

    public override void RefillAmmo()
    {
        _currentClip = 1;
        _reserveAmmo = 2;
    }
    public void PullPin()
    {
        _pinPulled = true;
        StartCoroutine(ExplosionCountdownRoutine());
    }
    IEnumerator ExplosionCountdownRoutine()
    {
        if (_timeUntilExplode > 0 && _grenadeThrown == false)
        {
            yield return new WaitForSeconds(0.1f);
            _timeUntilExplode -= 0.1f;
            StartCoroutine(ExplosionCountdownRoutine());
        }
        else if (_timeUntilExplode <= 0 && _grenadeThrown == false)
        {
            HandleExplosion();
        }
    }

    private void HandleExplosion()
    {
        _currentClip--;
        Instantiate(_grenadeExplosion, transform.position, Quaternion.identity);
        _timeUntilExplode = _timeUntilExplodeDefault;
        _pinPulled = false;
        gameObject.SetActive(false);
        _explodedInHand = true;
    }

    public bool GetExplodedInHand() {  return _explodedInHand; }
    public void SetExplodedInHand()
    {
        _explodedInHand = false;
    }
}
