using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float _mouseSensitivity = 1;

    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * _mouseSensitivity;
        transform.localEulerAngles = newRotation;
    }
}
