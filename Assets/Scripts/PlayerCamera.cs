using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    float _playerSpeed = 5;

    Vector3 velocity;
    CharacterController _charControl;
    Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        if (_charControl == null)
        { Debug.LogError("Character Controller is NULL"); }

        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null) { Debug.LogWarning("RIGIDBODY is NULL in CAMERA script");  }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _rigidBody.velocity = new Vector3(horizontal, velocity.y, vertical) * _playerSpeed;
    }
}
