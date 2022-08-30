using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float _mouseSensitivity = 1;
    bool _paused;

    Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {

        if (!_player.GetGameOver())
        {
                    if (!_paused)
        {
            float _mouseX = Input.GetAxis("Mouse X");

            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y += _mouseX * _mouseSensitivity;
            transform.localEulerAngles = newRotation;
        }
    }
        }


    public void SetPaused(bool pauseGame)
    {
        _paused = pauseGame;
    }
}
