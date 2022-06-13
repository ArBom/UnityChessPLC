using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.cs;

namespace Assets.cs
{
    public static class CheckMoves
    {
        public static Chessboard chessboard;

        public static CanMoveInto checkCastling(Assets.Color YourColor, bool longCastling, CubeRS[,] TableOfChequers = null)
        {
            CubeRS[,] LocalChequers;

            if (TableOfChequers != null)
            {
                LocalChequers = TableOfChequers;
            }
            else
            {
                LocalChequers = chessboard.chequers;
            }

            /*
            https://pl.wikipedia.org/wiki/Roszada
            król nie wykonał ruchu od początku partii,
            wieża uczestnicząca w roszadzie nie wykonała ruchu od początku partii,
            pomiędzy królem i tą wieżą nie ma innych bierek,
            król nie jest szachowany,
            pole, przez które przejdzie król nie jest atakowane przez bierki przeciwnika,
            roszada nie spowoduje, że król znajdzie się pod szachem.
            król i wieża znajdują się na tej samej linii (białe na pierwszej, a czarne – na ósmej linii).
            */

            return CanMoveInto.NoExist;
        }

        public static CanMoveInto CheckMove(Assets.Color? YourColor, ChequerPos Pos, List<ChequerPos> ByWhite = null, List<ChequerPos> ByBlack = null, CubeRS[,] TableOfChequers = null)
        {
            CubeRS[,] LocalChequers;

            if (TableOfChequers != null)
            {
                LocalChequers = TableOfChequers;
            }
            else
            {
                LocalChequers = chessboard.chequers;
            }


            if (Pos.column > 7 || Pos.row > 7 || Pos.column < 0 || Pos.row < 0)
            {
                return CanMoveInto.NoExist;
            }

            if (LocalChequers[Pos.column, Pos.row].chessman == null) //Chequer is empty
            {
                if (!YourColor.HasValue || ByBlack == null || ByWhite == null) //Your color isnt inmportant
                    return CanMoveInto.Empty;
                else if (YourColor == Assets.Color.White && ByBlack != null) //you are white && you need info about chequed
                {
                    if (ByBlack.Exists(cp =>
                                       cp.column == Pos.column &&
                                       cp.row == Pos.row))
                        return CanMoveInto.EmptyButChecked; //You are white && you want to move checked chequer
                    else return CanMoveInto.Empty; //You are white && you want to move to the save chequer
                }
                else if (YourColor == Assets.Color.Black && ByWhite != null) //you are black && you need info about chequed
                {
                    if (ByWhite.Exists(cp =>
                                        cp.column == Pos.column &&
                                        cp.row == Pos.row))
                        return CanMoveInto.EmptyButChecked; //You are black && you want to move checked chequer
                    else return CanMoveInto.Empty; //You are black && you want to move to the save chequer
                }
                else return CanMoveInto.Empty; //You are black or white && you Dont need info about chequed
            }

            if (YourColor.HasValue)
            {
                if (LocalChequers[Pos.column, Pos.row].chessman.color == YourColor)
                {
                    return CanMoveInto.TakenY;
                }

                if (LocalChequers[Pos.column, Pos.row].chessman.color != YourColor)
                {
                    return CanMoveInto.TakenO;
                }
            }

            return CanMoveInto.NoExist;
        }

    }
}
