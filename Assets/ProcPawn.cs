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
        AddPier(false, true, .2f, .1f);
        Marge();
        chessmanType = ChessmanType.PAWN;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting) Moves()
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

        ChequerPos marked = this.position.Value;

        return (marked, possible, confuting);
    }
}
