using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{

    private CinemachineVirtualCamera _CM;
    // Start is called before the first frame update
    void Start()
    {
        _CM = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        float fov = _CM.m_Lens.FieldOfView;
       // Debug.Log(fov);
        if (fov <= 45 && fov >= 30)
        {
            if (Input.GetKey(KeyCode.PageUp))
            {
                fov++;
            }
        }
        if (fov <= 45 && fov >= 30)
        {
            if (Input.GetKey(KeyCode.PageDown))
            {
                fov--;
            }
        }

    }
}
