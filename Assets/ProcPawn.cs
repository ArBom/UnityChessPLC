using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProcPawn : Chessman
{
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        chessmanType = ChessmanType.PAWN;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeData();
        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeData()
    {
        points = new Vector3[] {
            new Vector3(0f, .23f, 0f),
            new Vector3(0f, .19f, .023333f), new Vector3(0.009567f, .19f, 0.021316f), new Vector3(0.01734f, .19f, 0.016133f), new Vector3(0.022193f, .15f, 0.00721f),  new Vector3(0.023201f, .15f, -0.001572f),
            new Vector3(0.023206f, .19f, -0.011667f), new Vector3(0.013717f, .15f, -0.018877f),  new Vector3(0.00485f, .15f, -0.022827f),
            new Vector3(-0.00485f, .19f, -0.022823f), new Vector3(-0.013717f, .15f, -0.018877f), new Vector3(-0.020207f, .15f, -0.011667f), new Vector3(-0.023207f, .15f, -0.002438f),
            new Vector3(-0.022193f, .19f, 0.00721f), new Vector3(-0.01734f, .15f, 0.015613f), new Vector3(-0.009563f, .15f, 0.021317f), new Vector3(0, .15f, 0.023333f),

        };

        triangleElements = new int[] {
            0,1,2, 0,2,3, 0,3,4, 0,4,5, 0,5,6, 0,7,8, //0,8,7, 0,9,8, 0,10,9, 0,11,10, 0,12,11, 0,13,12, 0,14,13, 0,15,14, 0,16,15, 0,1,16,
        };
    }
}
