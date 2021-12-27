using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public enum Color
    {
        Black = 0,
        White = 64
    };

    public enum ChessmanType
    {
        EMPTY = 0,
        PAWN = 1,
        ROCK = 2,
        KNIGHT = 4,
        BISHOP = 8,
        QUEEN = 16,
        KING = 32,
    }

    public struct ChequerPos
    {
        public short column { get; set; }
        public short row { get; set; }
    }

    public static class ChequerPosHelper
    {
        public static short ChequerPos2int(ChequerPos chequerPosIn)
        {
            return (short)(chequerPosIn.column * 8 + chequerPosIn.row);
        }

        public static ChequerPos Int2ChequerPos(int intIn)
        {
            ChequerPos chequerPos = new ChequerPos
            {
                column = Convert.ToInt16(intIn / 8),
                row = Convert.ToInt16(intIn % 8)
            };
            return chequerPos;
        }
    }
}
