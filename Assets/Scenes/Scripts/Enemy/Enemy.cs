using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _health = 3;
    protected float _speed = 3;
    protected GameObject[] _loot;
    private Player _player;

    public float Health { get; set; }
    public float Speed { get; set; }

    public Enemy()
    {  
        Health = _health;
        Speed = _speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null in Enemy Script");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }    
    }
}
