using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.cs;
using UnityEngine;

namespace Assets.cs
{
    public static class CheckMoves
    {
        public static Chessboard chessboard;

        public static bool checkCastlingPos(Assets.Color YourColor, bool longCastling, (List<ChequerPos> ByWhite, List<ChequerPos> ByBlack) Checked, CubeRS[,] TableOfChequers = null)
        {
            int Row = YourColor == Assets.Color.White ? 0 : 7;
            int RookCol = longCastling ? 0 : 7;
            List<ChequerPos> check = YourColor == Color.White ? Checked.ByBlack : Checked.ByWhite;

            CubeRS[,] LocalChequers;

            if (TableOfChequers != null)
            {
                LocalChequers = TableOfChequers;
            }
            else
            {
                LocalChequers = chessboard.chequers;
            }

            //We skazananych polach w ogóle są bierki
            if (LocalChequers[4, Row].chessman == null || LocalChequers[RookCol, Row].chessman == null)
                return false;

            //https://pl.wikipedia.org/wiki/Roszada
            //król nie wykonał ruchu od początku partii,
            if (!LocalChequers[4, Row].chessman.nieDrgnal)
                return false;

            //wieża uczestnicząca w roszadzie nie wykonała ruchu od początku partii,
            if (!LocalChequers[RookCol, Row].chessman.nieDrgnal)
                return false;

            //pomiędzy królem i tą wieżą nie ma innych bierek,
            bool Others = false;

            if (longCastling)
            {
                for (int ColCh = 1; ColCh<4; ColCh++)
                {
                    if (LocalChequers[ColCh, Row].chessman != null)
                        Others = true;
                }
            }
            else
            {
                for (int ColCh = 5; ColCh < 7; ColCh++)
                {
                    if (LocalChequers[ColCh, Row].chessman != null)
                        Others = true;
                }
            }

            if (Others)
                return false;

            //król nie jest szachowany,
            if (check.Exists(c =>
                             c.column == 4 &&
                             c.row == Row))
                return false;

            //pole, przez które przejdzie król nie jest atakowane przez bierki przeciwnika,
            if (longCastling)
            {
                if (check.Exists(c =>
                                 c.column == 3 &&
                                 c.row == Row))
                    return false;
            }
            else
            {
                if (check.Exists(c =>
                                 c.column == 5 &&
                                 c.row == Row))
                    return false;
            }

            //roszada nie spowoduje, że król znajdzie się pod szachem.
            if (longCastling)
            {
                if (check.Exists(c =>
                                 c.column == 2 &&
                                 c.row == Row))
                    return false;
            }
            else
            {
                if (check.Exists(c =>
                                 c.column == 6 &&
                                 c.row == Row))
                    return false;
            }

            //król i wieża znajdują się na tej samej linii (białe na pierwszej, a czarne – na ósmej linii).
            //Zapewnione sposobem implementacji powyższego

            return true;
        }

        public static CanMoveInto CheckMove(Assets.Color? YourColor, ChequerPos Pos, (List<ChequerPos> ByWhite, List<ChequerPos> ByBlack) check , CubeRS[,] TableOfChequers = null)
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
                if (!YourColor.HasValue || check.ByBlack == null || check.ByWhite == null) //Your color isnt inmportant
                    return CanMoveInto.Empty;
                else if (YourColor == Assets.Color.White && check.ByBlack != null) //you are white && you need info about chequed
                {
                    if (check.ByBlack.Exists(cp =>
                                             cp.column == Pos.column &&
                                             cp.row == Pos.row))
                        return CanMoveInto.EmptyButChecked; //You are white && you want to move checked chequer
                    else return CanMoveInto.Empty; //You are white && you want to move to the save chequer
                }
                else if (YourColor == Assets.Color.Black && check.ByWhite != null) //you are black && you need info about chequed
                {
                    if (check.ByWhite.Exists(cp =>
                                             cp.column == Pos.column &&
                                             cp.row == Pos.row))
                        return CanMoveInto.EmptyButChecked; //You are black && you want to move checked chequer
                    else return CanMoveInto.Empty; //You are black && you want to move to the save chequer
                }
                else return CanMoveInto.Empty; //You are black or white && you Dont need info about chequed
            }

            if (YourColor.HasValue)
            {
                if (LocalChequers[Pos.column, Pos.row].chessman.color == YourColor.Value)
                {
                    return CanMoveInto.TakenY;
                }

                if (LocalChequers[Pos.column, Pos.row].chessman.color != YourColor.Value)
                {
                    if (YourColor.Value == Assets.Color.White)
                    {
                        if (check.ByBlack.Exists(cp =>
                                                 cp.column == Pos.column &&
                                                 cp.row == Pos.row))
                            return CanMoveInto.TakenOButChecked;
                    }
                    else
                    {
                        if (check.ByWhite.Exists(cp =>
                                                 cp.column == Pos.column &&
                                                 cp.row == Pos.row))
                            return CanMoveInto.TakenOButChecked;
                    }

                    return CanMoveInto.TakenO;
                }
            }

            return CanMoveInto.NoExist;
        }

    }
}
