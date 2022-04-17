using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    float _liveTime = 180; // Time before Despawn

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _liveTime);
    }

    protected void DestroyLoot()
    {
        Destroy(this.gameObject);
    }
}
