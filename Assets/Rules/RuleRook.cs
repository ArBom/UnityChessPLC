using System.Collections;
using System.Collections.Generic;
using Assets;

public class RuleRock
{
    public (List<ChequerPos> possible, List<ChequerPos> confuting) Moves(ChequerPos chequerPos) //IN: position of moving chessman
    {
        Chessboard chessboard = new Chessboard(); //TODO change it!
        Color color = chessboard.chequers[chequerPos.column, chequerPos.row].chessman.color;

        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();

        short columnToCheck = chequerPos.column;
        short rowToCheck = chequerPos.row;

        CanMoveInto canMoveInto;

        for (short direction = 0; direction < 5; direction++)
        {
            switch (direction)
            {
                case 0: //north
                    {
                        do
                        {
                            columnToCheck++;
                            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });   
                            if (canMoveInto == CanMoveInto.TakenO)
                            {
                                confuting.Add(new ChequerPos() { column=columnToCheck, row=rowToCheck });
                            }
                            if (canMoveInto == CanMoveInto.Empty)
                            {
                                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                            }
                        }
                        while (canMoveInto == CanMoveInto.Empty);
                    }
                    break;

                case 1: //south
                    {
                        columnToCheck--;
                        canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        if (canMoveInto == CanMoveInto.TakenO)
                        {
                            confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        }
                        if (canMoveInto == CanMoveInto.Empty)
                        {
                            possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        }
                    }
                    break;

                case 2: //west
                    {
                        rowToCheck++;
                        canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        if (canMoveInto == CanMoveInto.TakenO)
                        {
                            confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        }
                        if (canMoveInto == CanMoveInto.Empty)
                        {
                            possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        }
                    }
                    break;

                case 3: //east
                    {
                        rowToCheck--;
                        canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        if (canMoveInto == CanMoveInto.TakenO)
                        {
                            confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        }
                        if (canMoveInto == CanMoveInto.Empty)
                        {
                            possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
                        }
                    }
                    break;

                default: canMoveInto = CanMoveInto.NoExist; break;
            }

            if (canMoveInto == CanMoveInto.Empty)
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO)
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });

            direction++;
        }

        return (possible, confuting);
    }
}
