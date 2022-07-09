using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.cs;

using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class ProcPawn : Chessman
{
    private void Awake()
    {
        AddPier(false, true, .2f, .1f);
        Marge();

        chessmanType = ChessmanType.PAWN;
    }

    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) Moves()
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();
        List<ChequerPos> protect = new List<ChequerPos>();

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

        CanMoveInto canMoveInto;

        canMoveInto = chessboard.Check(this.color, new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row + direction) });
        if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            confuting.Add(new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row + direction) });
        else if (canMoveInto == CanMoveInto.TakenY || canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            protect.Add(new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row + direction) });

        canMoveInto = chessboard.Check(this.color, new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row + direction) });
        if (canMoveInto == CanMoveInto.TakenO || canMoveInto == CanMoveInto.TakenOButChecked)
            confuting.Add(new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row + direction) });
        else if (canMoveInto == CanMoveInto.TakenY || canMoveInto == CanMoveInto.Empty || canMoveInto == CanMoveInto.EmptyButChecked)
            protect.Add(new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row + direction) });

        ChequerPos marked = this.position.Value;

        return (marked, possible, confuting, protect);
    }
}
