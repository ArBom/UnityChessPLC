using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

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


    void MakeData()
    {
        MakePier(false, .42f, .15f);
    }

    void MakePier(bool coping, float high, float radius)
    {
        points = new Vector3[100];
        points[0] = new Vector3(0f, 0.93f*high, 0f);

        for (int a = 1; a<25; a++)
        {
            points[a] = new Vector3((float)(0.75f*radius*Math.Sin(2*Math.PI*a/24)), high, (float)(0.75f*radius*Math.Cos(2*Math.PI*a/24)));
        }

        points[25] = new Vector3(0f, high*.7f, 0f);

        for (int a = 26; a < 50; a++)
        {
            points[a] = new Vector3((float)(radius * Math.Sin(2 * Math.PI * a / 24)), 0.94f * high, (float)(radius * Math.Cos(2 * Math.PI * a / 24)));
        }

        for (int a = 50; a < 74; a++)
        {
            points[a] = new Vector3((float)(0.6f*radius * Math.Sin(2 * Math.PI * a / 24)), 0, (float)(0.6f*radius * Math.Cos(2 * Math.PI * a / 24)));
        }

        points[74] = new Vector3(0f, high*.3f, 0f);

        for (int a = 75; a < 99; a++)
        {
            points[a] = new Vector3((float)(radius * Math.Sin(2 * Math.PI * a / 24)), 0, (float)(radius * Math.Cos(2 * Math.PI * a / 24)));
        }

        triangleElements = new int[] {
            0,1,2, 0,2,3, 0,3,4, 0,4,5, 0,5,6, 0,6,7, 0,7,8, 0,8,9, 0,9,10, 0,10,11, 0,11,12, 0,12,13, 0,13,14, 0,14,15, 0,15,16, 0,16,17, 0,17,18, 0,18,19, 0,19,20, 0,20,21, 0,21,22, 0,22,23, 0,23,24, 0,24,1,
            25,2,1, 25,3,2, 25,4,3, 25,5,4, 25,6,5, 25,7,6, 25,8,7, 25,9,8, 25,10,9, 25,11,10, 25,12,11, 25,13,12, 25,14,13, 25,15,14, 25,16,15, 25,17,16, 25,18,17, 25,19,18, 25,20,19, 25,21,20, 25,22,21, 25,23,22, 25,24,23, 25,1,24,
            //
            0,26,27, 0,27,28, 0,28,29, 0,29,30, 0,30,31, 0,31,32, 0,32,33, 0,33,34, 0,34,35, 0,35,36, 0,36,37, 0,37,38, 0,38,39, 0,39,40, 0,40,41, 0,41,42, 0,42,43, 0,43,44, 0,44,45, 0,45,46, 0,46,47, 0,47,48, 0,48,49, 0,49,26,
            25,27,26, 25,28,27, 25,29,28, 25,30,29, 25,31,30, 25,32,31, 25,33,32, 25,34,33, 25,35,34, 25,36,35, 25,37,36, 25,38,37, 25,39,38, 25,40,39, 25,41,40, 25,42,41, 25,43,42, 25,44,43, 25,45,44, 25,46,45, 25,47,46, 25,48,47, 25,49,48, 25,26,49,
            //
            0,50,51, 0,51,52, 0,52,53, 0,53,54, 0,54,55, 0,55,56, 0,56,57, 0,57,58, 0,58,59, 0,59,60, 0,60,61, 0,61,62, 0,62,63, 0,63,64, 0,64,65, 0,65,66, 0,66,67, 0,67,68, 0,68,69, 0,69,70, 0,70,71, 0,71,72, 0,72,73, 0,73,50,
            74,75,76, 74,76,77, 74,77,78, 74,78,79, 74,79,80, 74,80,81, 74,81,82, 74,82,83, 74,83,84, 74,84,85, 74,85,86, 74,86,87, 74,87,88, 74,88,89, 74,89,90, 74,90,91, 74,91,92, 74,92,93, 74,93,94, 74,94,95, 74,95,96, 74,96,97, 74,97,98, 74,98,75,
        };
    }

    public override (List<ChequerPos> possible, List<ChequerPos> confuting) Moves()
    {
        bool nieDrgnal = true; //TODO change it

        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();

        short columnToCheck = position.Value.column;
        short rowToCheck = position.Value.row;

        short direction = (color == Assets.Color.White) ? (short)1 : (short)-1; //is it go N or S of chesboard

        if (chessboard.Check(null, new ChequerPos() { column = position.Value.column, row = (short)(position.Value.row + direction) }) == CanMoveInto.Empty) //is it possible move 1 square
        {
            possible.Add(new ChequerPos() { column = position.Value.column, row = (short)(position.Value.row + direction) });

            if (nieDrgnal) //is it first move of pawn
            {
                if (chessboard.Check(null, new ChequerPos() { column = position.Value.column, row = (short)(position.Value.row + 2 * direction) }) == CanMoveInto.Empty) //is it possible move 2 square
                    possible.Add(new ChequerPos() { column = position.Value.column, row = (short)(position.Value.row + 2 * direction) });
            }
        }

        if (chessboard.Check(null, new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row + direction) }) == CanMoveInto.TakenO)
            confuting.Add(new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row + direction) });

        if (chessboard.Check(null, new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row + direction) }) == CanMoveInto.TakenO)
            confuting.Add(new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row + direction) });

        return (possible, confuting);
    }
}
