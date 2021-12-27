using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcCamera : MonoBehaviour
{
    private float cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(0f, .5f, -2f);
        cameraRotation = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            cameraRotation -= .1f;
            if (cameraRotation < -45.0f)
            {
                cameraRotation = -45.0f;
            }
        }

        if (Input.GetKey("right"))
        {
            cameraRotation += .1f;
            if (cameraRotation > 45.0f)
            {
                cameraRotation = 45.0f;
            }
        }

        // Rotate the camera
        Camera.main.transform.localEulerAngles = new Vector3(0.0f, cameraRotation, 0.0f);
    }
}
