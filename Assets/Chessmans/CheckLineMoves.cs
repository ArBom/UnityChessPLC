using System.Collections.Generic;
using Assets;

public static class CheckLineMoves
{
    public static (List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) ObliqueLinesOfMove(ChequerPos pos, Assets.Color color, Chessboard chessboard)
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();
        List<ChequerPos> protect = new List<ChequerPos>();

        short columnToCheck = pos.column;
        short rowToCheck = pos.row;

        CanMoveInto canMoveInto;

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
        rowToCheck = pos.row;
        columnToCheck = pos.column;

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
        rowToCheck = pos.row;
        columnToCheck = pos.column;

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
        rowToCheck = pos.row;
        columnToCheck = pos.column;

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
        columnToCheck = pos.column;

        return (possible, confuting, protect);
    }

    public static (List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) NonObliqueLinesOfMove(ChequerPos pos, Assets.Color color, Chessboard chessboard)
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();
        List<ChequerPos> protect = new List<ChequerPos>();

        short columnToCheck = pos.column;
        short rowToCheck = pos.row;

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
        rowToCheck = pos.row;

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
        rowToCheck = pos.row;

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
        columnToCheck = pos.column;

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

        return (possible, confuting, protect);
    }
}
