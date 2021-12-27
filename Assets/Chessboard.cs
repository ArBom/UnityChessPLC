using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;


public class Chessboard : MonoBehaviour
{
    const string IPaddr = "192.168.0.100";
    private S7Client s7Client;
    public ProcChequer procChequer;
    Rect rect;
    public ProcChequer[,] chequers;
    private void Awake()
    {
        s7Client = new S7Client();

        chequers = new ProcChequer[8,8];

        for (int a=0; a!=8; ++a)
        {
            for (int b=0; b!=8; ++b)
            {
                
               // ProcChequer pc = Instantiate();
                    //chequers.Add(pc);
                //ScriptableObject temp = ScriptableObject.CreateInstance("ProcChequer");
                //GameObject temp = GameObject.CreatePrimitive(squa);

                //GameObject temp = GameObject.CreatePrimitive()
                //temp.transform.Translate(new Vector3(a, 0, b));
            }
        }

        s7Client.ConnectTo(IPaddr,0,0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
