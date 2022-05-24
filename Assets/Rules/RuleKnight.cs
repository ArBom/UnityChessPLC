using System.Collections;
using System.Collections.Generic;
using Assets;

public class RuleKnight
{
    public (List<ChequerPos> possible, List<ChequerPos> confuting) Moves(ChequerPos chequerPos) //IN: position of moving chessman
    {
        Chessboard chessboard = new Chessboard(); //TODO change it!
        Color color = chessboard.chequers[chequerPos.column, chequerPos.row].chessman.color;

        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();

        short columnToCheck = chequerPos.column;
        short rowToCheck = chequerPos.row;

        CanMoveInto canMoveIntoTemp;
        ChequerPos chequerPosTemp;

        //NNE
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column + 1), row = (short)(chequerPos.row + 2) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //NEE
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column + 2), row = (short)(chequerPos.row + 1) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SEE
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column + 2), row = (short)(chequerPos.row - 1) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SEE
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column + 1), row = (short)(chequerPos.row - 2) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SSW
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column - 2), row = (short)(chequerPos.row - 1) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SWW
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column - 1), row = (short)(chequerPos.row - 2) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //NWW
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column - 2), row = (short)(chequerPos.row + 1) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //NNW
        chequerPosTemp = new ChequerPos() { column = (short)(chequerPos.column - 1), row = (short)(chequerPos.row + 2) };
        canMoveIntoTemp = chessboard.Check(null, chequerPosTemp); //TODO color
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        return (possible, confuting);
    }
}
