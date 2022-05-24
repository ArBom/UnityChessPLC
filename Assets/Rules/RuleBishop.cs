using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    class RuleBishop
    {
        public (List<ChequerPos> possible, List<ChequerPos> confuting) Moves (ChequerPos chequerPos) //IN: position of moving chessman
        {
            Chessboard chessboard = new Chessboard(); //TODO change it!
            Color color = chessboard.chequers[chequerPos.column, chequerPos.row].chessman.color;

            List<ChequerPos> possible = new List<ChequerPos>();
            List<ChequerPos> confuting = new List<ChequerPos>();

            short columnToCheck = chequerPos.column;
            short rowToCheck = chequerPos.row;
            CanMoveInto canMoveInto;

            for (short direction = 0; direction<5; direction++)
            {
                switch(direction)
                {
                    case 0: //north-east
                        {
                            do
                            {
                                canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                                columnToCheck++;
                                rowToCheck++;
                            }
                            while (canMoveInto == CanMoveInto.Empty);
                        }
                        break;

                    case 1: //north-west
                        {
                            do 
                            {
                                canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                                columnToCheck--;
                                rowToCheck++;
                            }
                            while (canMoveInto == CanMoveInto.Empty);
                        }
                        break;

                    case 2: //south-east
                        {
                            do
                            {
                                canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                                columnToCheck++;
                                rowToCheck--;
                            }
                            while (canMoveInto == CanMoveInto.Empty);
                        }
                        break;

                    case 3: //south-west
                        {
                            do
                            {
                                canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });
                                columnToCheck--;
                                rowToCheck--;
                            }
                            while (canMoveInto == CanMoveInto.Empty);
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
}
