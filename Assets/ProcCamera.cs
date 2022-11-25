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
    private const bool camCanBounce = true;
    private float Lookat = 3.5f;
    private Vector4 camBounTarPos;

    // Start is called before the first frame update
    void Start()
    {
        UI.GetComponent<ProcUI>().clicked += this.ShowSetti;

        if (camMove)
        {
            chessboard.turnChange += TurnChange;
            chessboard.markedCubeCh += Bounce;
            angleCamera = -180;
            TurnChange(Assets.Color.White);
        }

        camBounTarPos = new Vector4(4, 6, -4f, 3.5f);
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
            #pragma warning disable CS0162
            return;
            #pragma warning restore CS0162

        if (angleCamera == targetCameraAngle && CameraY == TargetCameraY && TargetCameraY != SettingY)
        {
            if (camCanBounce)
            {
                Camera.main.transform.position = new Vector3(.92f * Camera.main.transform.position.x + .08f * camBounTarPos.x,
                                                             .92f * Camera.main.transform.position.y + .08f * camBounTarPos.y,
                                                             .92f * Camera.main.transform.position.z + .08f * camBounTarPos.z);

                Lookat = .93f * Lookat + 0.07f * camBounTarPos.w;

                Camera.main.transform.LookAt(new Vector3(3.5f, 0, Lookat));
            }
            return;
        }

        CalcCamposChessboard();
        CalcCamposSetti();

        camBounTarPos = Camera.main.transform.position;
        camBounTarPos.w = Lookat;
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

        float x = (float)(3.5f + (13 * Math.Sin(2 * Math.PI * angleCamera / 360)));
        float z = (float)(3.5f + (6.5 * Math.Cos(2 * Math.PI * angleCamera / 360)));
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

        Camera.main.transform.LookAt(new Vector3(3.5f, CameraY, 3.5f));
    }

    private void Bounce(ChequerPos chequerPos, string Icon)
    {
        if (!camCanBounce)
            #pragma warning disable CS0162
            return;
            #pragma warning restore CS0162

        if (Camera.main.transform.position.z < 0) //Camera looks as white player
        {
            camBounTarPos = Camera.main.transform.position;

            camBounTarPos.x = (float)(.165*(chequerPos.column - 3.5) + 3.5);
            camBounTarPos.y = 6 + .175f * (chequerPos.row - 2);
            camBounTarPos.z = -3.5f + .2f * chequerPos.row;
            camBounTarPos.w = chequerPos.row < 2 ? 3.5f : (float)(3.5 - 0.17*(chequerPos.row - 2));

            if (Icon == null)
            {
                camBounTarPos.x = 3.5f;
                camBounTarPos.y = 6;
                camBounTarPos.z = -4f;
                camBounTarPos.w = 3.5f;
            }
        }
        else //Camera looks as black player
        {
            camBounTarPos = Camera.main.transform.position;
            camBounTarPos.w = 3.5f;

            camBounTarPos.x = (float)(.165 * (chequerPos.column - 3.5) + 3.5);
            camBounTarPos.y = 6 + .175f * (2 - chequerPos.row);
            camBounTarPos.z = 10.5f - .2f * (7 - chequerPos.row);
            camBounTarPos.w = chequerPos.row > 5 ? 3.5f : (float)(3.5 + 0.17 * (-chequerPos.row + 5));

            if (Icon == null)
            {
                camBounTarPos.x = 3.5f;
                camBounTarPos.y = 6;
                camBounTarPos.z = 10.5f;
                camBounTarPos.w = 3.5f;
            }
        }
    }
}
