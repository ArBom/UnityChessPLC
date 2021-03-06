using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    // <ColorAndType>
    public enum Color
    {
        Black = 0,
        White = 64
    }

    public enum ChessmanType
    {
        EMPTY = 0,
        PAWN = 1,
        ROOK = 2,
        KNIGHT = 4,
        BISHOP = 8,
        QUEEN = 16,
        KING = 32,
    }
    // </ColorAndType>

    public static class FontDic
    {
        //Segoe UI Symbols
        public static readonly Dictionary<uint, string> ChessnansSymbols = new Dictionary<uint, string>
        {
            {1,  "\u265F" }, //Black Pawn
            {2,  "\u265C" }, //Black Rook
            {4,  "\u265E" }, //Black Knight
            {8,  "\u265D" }, //Black Bishop
            {16, "\u265B" }, //Black Queen
            {32, "\u265A" }, //Black King

            {65, "\u2659" }, //White Pawn
            {66, "\u2656" }, //White Rook
            {68, "\u2658" }, //White Knight
            {72, "\u2657" }, //White Bishop
            {80, "\u2655" }, //White Queen
            {96, "\u2654" }, //White King
        };
    }

    public enum CanMoveInto
    {
        NoExist,
        Empty,
        EmptyButChecked,
        TakenY,
        TakenO,
        TakenOButChecked,
        WRONGARG
    }

    public class ChequerPos
    {
        public short column { get; set; }
        public short row { get; set; }

        public string NameOfThis()
        {
            string ToReturn = null;

            if (row > -1 && column > -1 && row < 8 && column < 8)
            {
                char name1 = (char)(column + 65);
                string name2 = (row + 1).ToString();
                return name1 + name2;
            }
            else if (row == -1 || row == 8)
                ToReturn = ((char)(column + 65)).ToString();
            else if (column == -1 || column == 8)
                ToReturn = (row + 1).ToString();

            if (ToReturn == "0" || ToReturn == "9")
                ToReturn = "■";

            return ToReturn;
        }
    }

    public static class ChequerPosHelper
    {
        public static ushort ChequerPos2ushort(ChequerPos chequerPosIn)
        {
            return (ushort)(chequerPosIn.column * 8 + chequerPosIn.row);
        }

        public static ChequerPos Int2ChequerPos(ushort intIn)
        {
            if (intIn > 63)
            {
                return null;
            }
            ChequerPos chequerPos = new ChequerPos
            {
                column = Convert.ToInt16(intIn / 8),
                row = Convert.ToInt16(intIn % 8)
            };
            return chequerPos;
        }
    }
}
