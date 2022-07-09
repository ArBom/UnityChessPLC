using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProcQueen : Chessman
{
    private void Awake()
    {
        MakeData();
        AddPier(true, true, .32f, .12f);
        Marge();

        chessmanType = ChessmanType.QUEEN;
    }

    void MakeData()
    {
        pointsOfCoping = new Vector3[] {
            new Vector3(0f, .5f, 0f),
            new Vector3(0f, .4f, .023333f), new Vector3(0.009567f, .4f, 0.021316f), new Vector3(0.01734f, .4f, 0.016133f), new Vector3(0.022193f, .4f, 0.00721f),  new Vector3(0.023201f, .4f, -0.001572f),
            new Vector3(0.023206f, .4f, -0.011667f), new Vector3(0.013717f, .4f, -0.018877f),  new Vector3(0.00485f, .4f, -0.022827f),
            new Vector3(-0.00485f, .4f, -0.022823f), new Vector3(-0.013717f, .4f, -0.018877f), new Vector3(-0.020207f, .4f, -0.011667f), new Vector3(-0.023207f, .4f, -0.002438f),
            new Vector3(-0.022193f, .4f, 0.00721f), new Vector3(-0.01734f, .4f, 0.015613f), new Vector3(-0.009563f, .4f,  0.021317f), new Vector3(0, .4f, 0.023333f),

            new Vector3(0f, .435f, 0f),
            new Vector3(0f, .4f, .07f), new Vector3(0.01455f, .42f, 0.068467f), new Vector3(0.02847f, .43f, 0.06395f), new Vector3(0.04115f, .42f, 0.05663f), new Vector3(0.05202f, .4f, 0.0484f), new Vector3(0.06062f, .37f,  0.035f), new Vector3(0.06658f, .4f, 0.02163f), new Vector3(0.06962f, .42f, 0.004715f), new Vector3(0.06962f, .43f, -0.004715f),
            new Vector3(0.06658f, .42f, -0.02163f), new Vector3(0.06062f, .4f, -0.035f), new Vector3(0.05202f, .37f, -0.04684f), new Vector3(0.04115f, .4f, -0.05663f), new Vector3(0.02847f, .42f, -0.06545f), new Vector3(0.01455f, .43f, -0.06848f), new Vector3(0, .42f, -0.07f),
            new Vector3(-0.01455f, .4f, -0.06847f), new Vector3(-0.02843f, .37f, -0.06395f), new Vector3(-0.04115f, .4f, -0.05663f), new Vector3(-0.05202f, .42f,  -0.04684f), new Vector3(-0.06062f, .43f, -0.035f), new Vector3(-0.06658f, .42f,  -0.02163f), new Vector3(-0.06962f, .4f, -0.007315f), new Vector3(-0.06962f, .37f,  0.007315f),
            new Vector3(-0.06658f, .4f,  0.02163f), new Vector3(-0.06062f, .42f, 0.035f), new Vector3(-0.05202f, .43f, 0.04684f), new Vector3(-0.04115f, .42f, 0.05663f), new Vector3(-0.02869f, .4f, 0.06395f), new Vector3(-0.01019f, .37f, 0.06847f), new Vector3(0, .4f, 0.07f),
            new Vector3(0f, .25f, 0f),
        };

        triangleElementsOfCoping = new int[] {
            0,1,2, 0,2,3, 0,3,4, 0,3,4, 0,4,5, 0,5,6, 0,6,7, 0,7,8, 0,8,9, 0,9,10, 0,10,11, 0,11,12, 0,12,13, 0,13,14, 0,14,15, 0,15,16, 0,16,1,
            17,48,18, 17,18,19, 17,19,20, 17,20,21, 17,21,22, 17,22,23, 17,23,24, 17,24,25, 17,25,26, 17,26,27, 17,27,28, 17,28,29, 17,29,30, 17,30,31, 17,31,32, 17,32,33, 17,33,34, 17,34,35, 17,35,36, 17,36,37, 17,37,38, 17,38,39, 17,39,40, 17,40,41, 17,41,42, 17,42,43, 17,43,44, 17,44,45, 17,45,46, 17,46,47, 17,47,48,
            18,48,49, 19,18,49, 20,19,49, 21,20,49, 22,21,49, 23,22,49, 24,23,49, 25,24,49, 26,25,49, 27,26,49, 28,27,49, 29,28,49, 30,29,49, 31,30,49, 32,31,49, 33,32,49, 34,33,49, 35,34,49, 36,35,49, 37,36,49, 38,37,49, 39,38,49, 40,39,49, 41,40,49, 42,41,49, 43,42,49, 44,43,49, 45,44,49, 46,45,49, 47,46,49, 48,47,49,

        };

        MakeData2();
    }

    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) Moves()
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();
        List<ChequerPos> protect = new List<ChequerPos>();

        short columnToCheck = position.Value.column;
        short rowToCheck = position.Value.row;

        CanMoveInto canMoveInto;

        do //N
        {
            rowToCheck++;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        rowToCheck = position.Value.row;

        do //NE
        {
            rowToCheck++;
            columnToCheck--;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        rowToCheck = position.Value.row;
        columnToCheck = position.Value.column;

        do //E
        {
            columnToCheck--;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        columnToCheck = position.Value.column;

        do //SE
        {
            rowToCheck--;
            columnToCheck--;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        rowToCheck = position.Value.row;
        columnToCheck = position.Value.column;

        do //S
        {
            rowToCheck--;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        rowToCheck = position.Value.row;

        do //SW
        {
            rowToCheck--;
            columnToCheck++;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        rowToCheck = position.Value.row;
        columnToCheck = position.Value.column;

        do //W
        {
            columnToCheck++;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        columnToCheck = position.Value.column;

        do //NW
        {
            columnToCheck++;
            rowToCheck++;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.TakenY)
            {
                protect.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked);
        columnToCheck = position.Value.column;

        ChequerPos marked = this.position.Value;

        return (marked, possible, confuting, protect);
    }
}
