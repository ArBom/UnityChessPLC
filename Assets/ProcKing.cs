using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using Assets.cs;

[RequireComponent(typeof(MeshFilter))]
public class ProcKing : Chessman
{
    private void Awake()
    {
        MakeData();
        AddPier(true, true, .34f, .14f);
        Marge();

        chessmanType = ChessmanType.KING;
    }


    void MakeData()
    {
        pointsOfCoping = new Vector3[] {
            new Vector3(-0.023f, .54f, -0.023f), new Vector3(-0.023f, .54f, 0.023f), new Vector3(0.023f, .54f, 0.023f), new Vector3(0.023f, .54f, -0.023f),
            new Vector3(-0.0165f, .485f, -0.0165f), new Vector3(-0.0165f, .485f, 0.0165f), new Vector3(0.0165f, .485f, 0.0165f), new Vector3(0.0165f, .485f, -0.0165f),
            new Vector3(-0.023f, .5f, -0.063f), new Vector3(-0.023f, .5f, 0.063f), new Vector3(0.023f, .5f, -0.063f), new Vector3(0.023f, .5f, 0.063f), new Vector3(-0.023f, .45f, -0.063f), new Vector3(-0.023f, .45f, 0.063f), new Vector3(0.023f, .45f, -0.063f), new Vector3(0.023f, .45f, 0.063f),
            new Vector3(-0.0165f, .465f, -0.0165f), new Vector3(-0.0165f, .465f, 0.0165f), new Vector3(0.0165f, .465f, 0.0165f), new Vector3(0.0165f, .465f, -0.0165f),
            new Vector3(-0.023f, .38f, -0.023f), new Vector3(-0.023f, .38f, 0.023f), new Vector3(0.023f, .38f, 0.023f), new Vector3(0.023f, .38f, -0.023f),

            new Vector3(0f, .435f, 0f),
            new Vector3(0f, .4f, .07f), new Vector3(0.01455f, .4f, 0.068467f), new Vector3(0.02847f, .4f, 0.06395f), new Vector3(0.04115f, .4f, 0.05663f), new Vector3(0.05202f, .4f, 0.0484f), new Vector3(0.06062f, .4f, 0.035f), new Vector3(0.06658f, .4f, 0.02163f), new Vector3(0.06962f, .4f, 0.004715f), new Vector3(0.06962f, .4f, -0.004715f),
            new Vector3(0.06658f, .4f, -0.02163f), new Vector3(0.06062f, .4f, -0.035f), new Vector3(0.05202f, .4f, -0.04684f), new Vector3(0.04115f, .4f, -0.05663f), new Vector3(0.02847f, .4f, -0.06545f), new Vector3(0.01455f, .4f, -0.06848f), new Vector3(0, .4f, -0.07f),
            new Vector3(-0.01455f, .4f, -0.06847f), new Vector3(-0.02843f, .4f, -0.06395f), new Vector3(-0.04115f, .4f, -0.05663f), new Vector3(-0.05202f, .4f, -0.04684f), new Vector3(-0.06062f, .4f, -0.035f), new Vector3(-0.06658f, .4f, -0.02163f), new Vector3(-0.06962f, .4f, -0.007315f), new Vector3(-0.06962f, .4f, 0.007315f),
            new Vector3(-0.06658f, .4f, 0.02163f), new Vector3(-0.06062f, .4f, 0.035f), new Vector3(-0.05202f, .4f, 0.04684f), new Vector3(-0.04115f, .4f, 0.05663f), new Vector3(-0.02869f, .4f, 0.06395f), new Vector3(-0.01019f, .4f, 0.06847f), new Vector3(0, .4f, 0.07f),
            new Vector3(0f, .25f, 0f),
        };

        triangleElementsOfCoping = new int[] 
        {
            0,1,3, 1,2,3,
            0,4,1, 1,4,5, 1,5,2, 2,5,6, 2,6,3, 3,6,7, 3,7,4, 3,4,0,
            4,7,8, 6,5,9, 7,10,8, 9,11,6, 8,10,12, 10,14,12, 7,14,10, 14,7,19, 14,19,12, 19,16,12, 16,8,12, 16,4,8,
            9,13,15, 9,15,11, 6,11,15, 6,15,18, 17,13,9, 17,9,5,
            17,15,13,
            17,18,15, 16,17,4, 4,17,5, 18,6,15, 7,6,19, //przedostatnie zaczynało się od 19.
            16,20,17, 17,20,21,
            18,22,19, 19,22,23, 18,21,22, 18,17,21, 20,19,23, 19,20,16,
            24,54,25, 24,25,26, 24,26,27, 24,27,28, 24,28,29, 24,29,30, 24,30,31, 24,31,32, 24,32,33, 24,33,34, 24,34,35, 24,35,36, 24,36,37, 24,37,38, 24,38,39, 24,39,40, 24,40,41, 24,41,42, 24,42,43, 24,43,44, 24,44,45, 24,45,46, 24,46,47, 24,47,48, 24,48,49, 24,49,50, 24,50,51, 24,51,52, 24,52,53, 24,52,53, 24,53,54,
            25,54,56, 26,25,56, 27,26,56, 28,27,56, 29,28,56, 30,29,56, 31,30,56, 32,31,56, 33,32,56, 34,33,56, 35,34,56, 36,35,56, 37,36,56, 38,37,56, 39,38,56, 40,39,56, 41,40,56, 42,41,56, 43,42,56, 44,43,56, 45,44,56, 46,45,56, 47,46,56, 48,47,56, 49,48,56, 50,49,56, 51,50,56, 52,51,56, 52,51,56, 53,52,56, 54,53,56, 55,54,56,
            18,19,6,
        };

        MakeData2();
    }

    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) Moves()
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();
        List<ChequerPos> protect = new List<ChequerPos>();

        ChequerPos tempChequerPos = new ChequerPos();
        CanMoveInto canMoveInto;

        //N & S There is no Pama-Krabbégo castling https://pl.wikipedia.org/wiki/Roszada_Pama-Krabbégo

        //TODO dopisać sprawzenie czy pole nie jest szachowane

        //N
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row + 1), column = (short)position.Value.column };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //NE
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row + 1), column = (short)(position.Value.column + 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //E
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row), column = (short)(position.Value.column + 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
            if (nieDrgnal)
            {
                if (CheckMoves.checkCastlingPos(this.color, false, chessboard.Checked, chessboard.chequers))
                    possible.Add(new ChequerPos() { row = (short)(position.Value.row), column = (short)(position.Value.column + 2) });
            }
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //SE
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row - 1), column = (short)(position.Value.column + 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //S
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row - 1), column = (short)(position.Value.column) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //SW
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row - 1), column = (short)(position.Value.column - 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //W
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row), column = (short)(position.Value.column - 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
            if (nieDrgnal)
            {
                if (CheckMoves.checkCastlingPos(this.color, true, chessboard.Checked, chessboard.chequers))
                    possible.Add(new ChequerPos() { row = (short)(position.Value.row), column = (short)(position.Value.column - 2) });
            }
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        //NW
        tempChequerPos = new ChequerPos() { row = (short)(position.Value.row + 1), column = (short)(position.Value.column - 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenY)
        {
            protect.Add(tempChequerPos);
        }

        ChequerPos marked = this.position.Value;

        return (marked, possible, confuting, protect);
    }
}
