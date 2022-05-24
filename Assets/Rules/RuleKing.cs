using System.Collections;
using System.Collections.Generic;
using Assets;

public class RuleKing
{
    public (List<ChequerPos> possible, List<ChequerPos> confuting) Moves(ChequerPos chequerPos) //IN: position of moving chessman
    {
        bool nieDrgnal = true; //TODO change it
        Chessboard chessboard = new Chessboard(); //TODO change it!
        Color color = chessboard.chequers[chequerPos.column, chequerPos.row].chessman.color;

        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();

        ChequerPos tempChequerPos = new ChequerPos();
        CanMoveInto canMoveInto;

        //N
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row + 1), column = (short)chequerPos.column };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //NE
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row + 1), column = (short)(chequerPos.column+1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //E
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row), column = (short)(chequerPos.column + 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //SE
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row - 1), column = (short)(chequerPos.column + 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //S
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row - 1), column = (short)(chequerPos.column) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //SW
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row - 1), column = (short)(chequerPos.column-1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //W
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row), column = (short)(chequerPos.column - 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        //TODO dopisać roszady
        //NW
        tempChequerPos = new ChequerPos() { row = (short)(chequerPos.row + 1), column = (short)(chequerPos.column - 1) };
        canMoveInto = chessboard.Check(color, tempChequerPos);
        if (canMoveInto == CanMoveInto.Empty)
        {
            possible.Add(tempChequerPos);
        }
        else if (canMoveInto == CanMoveInto.TakenO)
        {
            confuting.Add(tempChequerPos);
        }

        return (possible, confuting);
    }
}
