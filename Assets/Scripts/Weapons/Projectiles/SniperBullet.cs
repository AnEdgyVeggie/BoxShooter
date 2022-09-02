using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet

{

    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();
        _rigidBody = GetComponent<Rigidbody>();
        _bulletSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}