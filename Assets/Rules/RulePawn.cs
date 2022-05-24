using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    class RulePawn
    {
        public (List<ChequerPos> possible, List<ChequerPos> confuting) Moves(ChequerPos chequerPos) //IN: position of moving chessman
        {
            bool nieDrgnal = true; //TODO change it
            Chessboard chessboard = new Chessboard(); //TODO change it!
            Color color = chessboard.chequers[chequerPos.column, chequerPos.row].chessman.color;

            List<ChequerPos> possible = new List<ChequerPos>();
            List<ChequerPos> confuting = new List<ChequerPos>();

            short columnToCheck = chequerPos.column;
            short rowToCheck = chequerPos.row;

            short direction = (color == Color.White) ? (short)1 : (short)-1; //is it go N or S of chesboard

            if (chessboard.Check(null, new ChequerPos() { column = chequerPos.column, row = (short)(chequerPos.row+direction) }) == CanMoveInto.Empty) //is it possible move 1 square
            {
                possible.Add(new ChequerPos() { column = chequerPos.column, row = (short)(chequerPos.row + direction) });

                if(nieDrgnal) //is it first move of pawn
                {
                    if (chessboard.Check(null, new ChequerPos() { column = chequerPos.column, row = (short)(chequerPos.row + 2 * direction) }) == CanMoveInto.Empty) //is it possible move 2 square
                        possible.Add(new ChequerPos() { column = chequerPos.column, row = (short)(chequerPos.row + 2*direction)});
                }
            }

            if (chessboard.Check(null, new ChequerPos() { column = (short)(chequerPos.column+1), row = (short)(chequerPos.row + direction) }) == CanMoveInto.TakenO)
                confuting.Add(new ChequerPos() { column = (short)(chequerPos.column + 1), row = (short)(chequerPos.row + direction) });

            if (chessboard.Check(null, new ChequerPos() { column = (short)(chequerPos.column - 1), row = (short)(chequerPos.row + direction) }) == CanMoveInto.TakenO)
                confuting.Add(new ChequerPos() { column = (short)(chequerPos.column - 1), row = (short)(chequerPos.row + direction) });

            return (possible, confuting);
        }
    }
}
