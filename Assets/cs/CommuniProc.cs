using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;

public class CommuniProc : MonoBehaviour
{
    public Chessboard chessboard;

    readonly string IPaddr = "192.168.0.1";
    readonly int rack = 0;
    readonly int slot = 1;

    private S7Client s7Client;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        chessboard.turnChange += UpdateData;
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
                if(db1Buffer[a] != 0)
                {
                    print("pole:" + a + ", wartosc:" + db1Buffer[a]);
                }
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
}
