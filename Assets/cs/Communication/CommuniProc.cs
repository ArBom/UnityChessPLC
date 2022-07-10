using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;

public class CommuniProc : MonoBehaviour
{
    public Chessboard chessboard;
    public ProcCamera procCamera;

    readonly string IPaddr = "192.168.0.1";
    readonly int rack = 0;
    readonly int slot = 1;

    private S7Client s7Client;

    private void Awake()
    {
        procCamera.showComm += ChangeActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUnactive();
    }

    void UpdateData(Assets.Color newColor)
    {
        s7Client = new S7Client();
        int connected = s7Client.ConnectTo(IPaddr, rack, slot);

        if (connected == 0)
        {
            print("Connection: OK");

            const int START_INDEX = 0;
            var db1Buffer = new byte[64];
            for (int a = 0; a != db1Buffer.Length; ++a)
            {
                db1Buffer[a] = (byte)chessboard.s7ChType[a];
            }

            int writing = s7Client.DBWrite(1, START_INDEX, db1Buffer.Length, db1Buffer);

            if (writing == 0)
            {
                print("Writing: OK");
            }
            else
            {
                s7Client.ErrorText(writing);
            }
        }
        else
        {
            print(s7Client.ErrorText(connected));
        }

        s7Client.Disconnect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeActive(bool active)
    {
        if (active)
            SetActive();
        else
            SetUnactive();
    }

    private void SetActive()
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);
    }

    private void SetUnactive()
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
