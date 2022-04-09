using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 5, _gravity = .01f;

    private Vector3 velocity;
    private CharacterController _charControl;
    // Start is called before the first frame update
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        if (_charControl == null)
        { Debug.LogError("Character Controller is NULL"); }
    }

    // Update is called once per frame
    void Update()
    {
        
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical);
            velocity = direction * _playerSpeed * Time.deltaTime;
            velocity.y -= _gravity;

            velocity = transform.transform.TransformDirection(velocity);
            _charControl.Move(velocity);

    }
}
