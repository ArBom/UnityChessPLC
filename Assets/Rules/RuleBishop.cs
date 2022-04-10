using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    class RuleBishop
    {
        Chessboard chessboard;

        public (List<ChequerPos> posible, List<ChequerPos> attack) moves (ChequerPos chequerPos)
        {
            //Color color = chessboard.chequers[chequerPos.column, chequerPos.row].chessman.color;

            List<ChequerPos> possible = new List<ChequerPos>();

            List<ChequerPos> atttack = new List<ChequerPos>();

            return (possible, atttack);
        }
    }
}
