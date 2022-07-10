using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using System;

public class ProcCamera : MonoBehaviour
{
    public Chessboard chessboard;
    public ProcUI UI;

    private const float speed = 40f;
    private float angleCamera;
    private float targetCameraAngle = 360;

    private const float ChessboardY = 0;
    private const float SettingY = 8;
    private float CameraY = 0;
    private float TargetCameraY = 0;

    public delegate void ShowComm(bool show);
    public ShowComm showComm;

    private const bool camMove = true;

    // Start is called before the first frame update
    void Start()
    {
        UI.GetComponent<ProcUI>().clicked += this.ShowSetti;

        if (camMove)
        {
            chessboard.turnChange += TurnChange;
            angleCamera = -180;
            TurnChange(Assets.Color.White);
        }
    }

    private void ShowSetti(bool show)
    {
        if (!show)
            TargetCameraY = SettingY;
        else
        {
            TargetCameraY = ChessboardY;
            showComm?.Invoke(false);
        }
    }

    private void TurnChange(Assets.Color newColor)
    {
        if (newColor == Assets.Color.White)
            targetCameraAngle = 180;
        else
            targetCameraAngle = 360;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camMove)
            return;

        if (angleCamera == targetCameraAngle && CameraY == TargetCameraY)
            return;

        CalcCamposChessboard();
        CalcCamposSetti();
    }

    private void CalcCamposChessboard()
    {
        if (angleCamera == targetCameraAngle)
            return;

        angleCamera = angleCamera + speed * Time.deltaTime;

        if (angleCamera >= targetCameraAngle)
        {
                if (targetCameraAngle == 180)
                    angleCamera = 180;
                else if (targetCameraAngle == 360)
                {
                    angleCamera = 0;
                    targetCameraAngle = 0;
                }
        }

        float x = (float)(4 + (13 * Math.Sin(2 * Math.PI * angleCamera / 360)));
        float z = (float)(3 + (6.5 * Math.Cos(2 * Math.PI * angleCamera / 360)));
        float y = (float)(6 + (6 * Math.Sin(2 * Math.PI * angleCamera / 180)));

        Camera.main.transform.position = new Vector3(x, y, z);
    }

    private void CalcCamposSetti()
    {
        if (CameraY != TargetCameraY)
        {
            float delta = speed * Time.deltaTime / 8;

            if (CameraY > TargetCameraY)
                delta = -delta;

            CameraY += delta;

            if (CameraY > SettingY)
            {
                CameraY = SettingY;
                showComm?.Invoke(true);
            }

            if (CameraY < ChessboardY)
            {
                CameraY = ChessboardY;
            }
        }

        Camera.main.transform.LookAt(new Vector3(4, CameraY, 4));
    }
}
