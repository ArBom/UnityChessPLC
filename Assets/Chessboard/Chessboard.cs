using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using Sharp7;


public class Chessboard : MonoBehaviour
{
    public Collider coll;

    Rect rect;
    public cubeS[,] chequers;
    public GameObject go = null;

    private void Awake()
    {
        chequers = new cubeS[8,8];

        for (ushort a=0; a!=8; ++a)
        {
            for (ushort b=0; b!=8; ++b)
            {
                var p = Instantiate(go, new Vector3(a * 1.0f, 0, b * 1.0f), Quaternion.identity);
                p.GetComponent<cubeS>().SetChequerPos(a, b);
                chequers[a, b] = p.GetComponent<cubeS>();
            }
            cubeS.chessboard = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
            /*if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (coll.Raycast(ray, out hit, 100))
                {
                    Debug.Log("Clicked on Sth");
                }
            }*/
    }

    public void Clicked(ushort row)
    {
        foreach (var a in chequers)
        {
            if (a.chequerPos.Value.row == row)
            {
                a.SetColor();
            }
        }
    }
}
