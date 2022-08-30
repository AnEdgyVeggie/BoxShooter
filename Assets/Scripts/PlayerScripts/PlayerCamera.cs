using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    float _playerSpeed = 5;

    Player _player;
    Vector3 velocity;
    Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null) { Debug.LogWarning("RIGIDBODY is NULL in CAMERA script");  }

        _player = GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.GetGameOver())
        {
            if (_player.GetPaused() == false)
            {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _rigidBody.velocity = new Vector3(horizontal, velocity.y, vertical) * _playerSpeed;
            }
        }

    }
}
