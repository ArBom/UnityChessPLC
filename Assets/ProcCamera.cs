using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using System;

public class ProcCamera : MonoBehaviour
{
    //private float cameraRotation;
    public Chessboard chessboard;
    private const float speed = 40f;
    private float angleCamera;
    private float targetCamera =360;

    private const bool camMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if (camMove)
        {
            chessboard.turnChange += TurnChange;
            angleCamera = -180;
            TurnChange(Assets.Color.White);
        }
    }

    private void TurnChange(Assets.Color newColor)
    {
        if (newColor == Assets.Color.White)
            targetCamera = 180;
        else
            targetCamera = 360;
    }

    // Update is called once per frame
    void Update()
    {
        #region kodPierwotny
        /*if (Input.GetKey("left"))
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
        Camera.main.transform.localEulerAngles = new Vector3(0.0f, cameraRotation, 0.0f);*/
        #endregion

        if (!camMove)
            return;

        if (angleCamera == targetCamera)
            return;

        angleCamera = angleCamera + speed * Time.deltaTime;
        
        if (angleCamera >= targetCamera)
        {
            if (targetCamera == 180)
                angleCamera = 180;
            else if (targetCamera == 360)
            {
                angleCamera = 0;
                targetCamera = 0;
            }
        }

        float x = (float)(4+(13 * Math.Sin(2 * Math.PI * angleCamera / 360)));
        float z = (float)(3+(6.5 * Math.Cos(2 * Math.PI * angleCamera / 360)));
        float y = (float)(6 + (6 * Math.Sin(2 * Math.PI * angleCamera / 180)));

        Camera.main.transform.position = new Vector3(x, y, z);
        Camera.main.transform.LookAt(new Vector3(4, 0, 4));
    }
}
