using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;
using System.Threading.Tasks;

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
        chessboard.turnChange += UpdateData;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUnactive();
    }

    void UpdateData(Assets.Color newColor)
    {
        Task CommT = new Task(() => 
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

                int writing1 = s7Client.DBWrite(1, START_INDEX, db1Buffer.Length, db1Buffer);

                byte White;
                if (newColor == Assets.Color.White)
                    White = 0b0_0000_0001;
                else
                    White = 0b0_0000_0000;

                byte WhitePLC = 0b0_0000_0000; //2nd byte
                byte BlackPLC = 0b0_0000_0100; //3nd byte

                byte restB = (byte)(White & WhitePLC & BlackPLC);

                byte[] rest = { restB };

                int writing2 = s7Client.DBWrite(1, 128, 1, rest);



                if (writing1 == 0 && writing2 ==0)
                {
                    print("Writing: OK");
                }
                else
                {
                    s7Client.ErrorText(writing1);
                }
            }
            else
            {
                print(s7Client.ErrorText(connected));
            }

            s7Client.Disconnect();
        });

        CommT.Start();
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
        /*for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }*/
        this.gameObject.SetActive(false);
    }
}
