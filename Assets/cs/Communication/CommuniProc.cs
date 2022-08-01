using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.UI;
using System;

public class CommuniProc : MonoBehaviour
{
    public Chessboard chessboard;
    public ProcCamera procCamera;
    public Toggle BlackPlayerT;
    public Toggle WhitePlayerT;

    readonly string IPaddr = "192.168.0.1";
    readonly int rack = 0;
    readonly int slot = 1;

    private uint LastClickPLCcheqer = 0;
    private uint _LastClickPLCchequer
    {
        get { return LastClickPLCcheqer; }
        set
        {
            if (value != LastClickPLCcheqer)
            {
                LastClickPLCcheqer = value;
                //TODO Next steps
            }
        }
    }

    private readonly object CommuniLock = new object();
    private static System.Timers.Timer SendingTimer;

    const byte TRUE = 0b0_0000_0001;
    const byte FALSE = 0b0_0000_0000;

    private bool WhiteTour = true;
    private bool WhitePLC = false;
    private bool BlackPLC = false;

    const ushort WhitePLCpos = 1;
    const ushort BlackPLCpos = 2;

    private S7Client s7Client;

    private void Awake()
    {
        s7Client = new S7Client();

        procCamera.showComm += ChangeActive;
        chessboard.turnChange += UpdateData;
        BlackPlayerT.onValueChanged.AddListener(delegate { BToggleCh(BlackPlayerT); });
        WhitePlayerT.onValueChanged.AddListener(delegate { WToggleCh(WhitePlayerT); });

        SendingTimer = new System.Timers.Timer(333);
        SendingTimer.Elapsed += OnSendingTimerTick;
        SendingTimer.AutoReset = true;

        StartStopSendingTimer();
    }


    // Start is called before the first frame update
    void Start()
    {
        SetUnactive();
    }

    void BToggleCh(Toggle change)
    {
        BlackPLC = change.isOn;
        StartStopSendingTimer();
    }

    void WToggleCh(Toggle change)
    {
        WhitePLC = change.isOn;
        StartStopSendingTimer();
    }

    private void StartStopSendingTimer()
    {
        if (WhitePLC || BlackPLC)
            SendingTimer.Start();
        else
            SendingTimer.Stop();
    }

    void UpdateData(Assets.Color newColor)
    {
        Task CommT = new Task(() => 
        {
            lock (CommuniLock)
            {
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
                    {
                        White = TRUE;
                        WhiteTour = true;
                    }
                    else
                    {
                        White = FALSE;
                        WhiteTour = false;
                    }

                    byte WhiteTS = WhitePLC ? TRUE : FALSE;
                    WhiteTS <<= WhitePLCpos;

                    byte BlackTS = BlackPLC ? TRUE : FALSE;
                    BlackTS <<= BlackPLCpos;


                    byte restB = (byte)(WhiteTS | BlackTS | White);
                    byte[] rest = { restB };
                    int writing2 = s7Client.DBWrite(1, 128, 1, rest);

                    if (writing1 == 0 && writing2 == 0)
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
            }
        });

        CommT.Start();
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

    private void OnSendingTimerTick(System.Object source, System.Timers.ElapsedEventArgs e)
    {
        if ((WhiteTour && WhitePLC) || (!WhiteTour && BlackPLC))
        {
            lock (CommuniLock)
            {
                int connected = s7Client.ConnectTo(IPaddr, rack, slot);

                if (connected == 0)
                {
                    byte[] readBuf = new byte[1];
                    s7Client.DBRead(1, 129, 1, readBuf);

                    _LastClickPLCchequer = (uint)readBuf[0];
                }
            }
        }
    }
}
