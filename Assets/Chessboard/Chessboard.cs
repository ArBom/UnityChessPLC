using Assets;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;

/*
 * row            N
 * ↓            W ╬ E
 * .              S
 * 4  █ █
 * 3 █ █
 * 2  █ █      (A1)=(chequers[0,0])=
 * 1 █ █
 *   ABCD. ←column
 */

public class Chessboard : MonoBehaviour
{
    public CubeRS[,] chequers; //[column,row]
    public GameObject CubeR = null;

    public GameObject ChKnight;
    public GameObject ChRook;
    public GameObject ChKing;
    public GameObject ChQueen;
    public GameObject ChBishop;
    public GameObject ChPawn;

    public Assets.Color actualTurn
    {
        get;
        private set;
    }

    public delegate void TurnChange(Assets.Color newColor);
    public event TurnChange turnChange;

    public (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting) Moves;
    public (List<ChequerPos> ByWhite, List<ChequerPos> Byblack) Checked;

    private void Awake()
    {
        //chequer table
        chequers = new CubeRS[8,8]; //[column,row]

        for (ushort c=0; c!=8; ++c)
        {
            for (ushort r=0; r!=8; ++r)
            {
                var p = Instantiate(CubeR, new Vector3(c * 1.0f, -.06f, r * 1.0f), Quaternion.identity);
                p.GetComponent<CubeRS>().SetChequerPos(r, c);
                chequers[c, r] = p.GetComponent<CubeRS>();
            }
            CubeRS.chessboard = this;
        }

        //list of moves
        Moves.possible = new List<ChequerPos>();
        Moves.confuting = new List<ChequerPos>();

        //list act chequer
        Checked.Byblack = new List<ChequerPos>();
        Checked.ByWhite = new List<ChequerPos>();

        //chessmans
        //white ones
        CreateChessman(ChessmanType.KNIGHT, new ChequerPos { column = 1, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.KNIGHT, new ChequerPos { column = 6, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.BISHOP, new ChequerPos { column = 2, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.BISHOP, new ChequerPos { column = 5, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.ROOK, new ChequerPos { column = 0, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.ROOK, new ChequerPos { column = 7, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.KING, new ChequerPos { column = 4, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.QUEEN, new ChequerPos { column = 3, row = 0 }, Assets.Color.White);

        for (short wp = 0; wp<8; ++wp)
            CreateChessman(ChessmanType.PAWN, new ChequerPos { column = wp, row = 1 }, Assets.Color.White);

        //black ones
        CreateChessman(ChessmanType.KNIGHT, new ChequerPos { column = 1, row = 7 }, Assets.Color.Black);
        CreateChessman(ChessmanType.KNIGHT, new ChequerPos { column = 6, row = 7 }, Assets.Color.Black);

        CreateChessman(ChessmanType.BISHOP, new ChequerPos { column = 2, row = 7 }, Assets.Color.Black);
        CreateChessman(ChessmanType.BISHOP, new ChequerPos { column = 5, row = 7 }, Assets.Color.Black);

        CreateChessman(ChessmanType.ROOK, new ChequerPos { column = 0, row = 7 }, Assets.Color.Black);
        CreateChessman(ChessmanType.ROOK, new ChequerPos { column = 7, row = 7 }, Assets.Color.Black);

        CreateChessman(ChessmanType.KING, new ChequerPos { column = 3, row = 7 }, Assets.Color.Black);
        CreateChessman(ChessmanType.QUEEN, new ChequerPos { column = 4, row = 7 }, Assets.Color.Black);

        for (short bp = 0; bp < 8; ++bp)
            CreateChessman(ChessmanType.PAWN, new ChequerPos { column = bp, row = 6 }, Assets.Color.Black);


        //Set the actual turn
        actualTurn = Assets.Color.White;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private (List<ChequerPos> ByWhite, List<ChequerPos> Byblack) CheckChecked(CubeRS[,] chequers)
    {
        HashSet<ChequerPos> ByWhiteHS = new HashSet<ChequerPos>();
        HashSet<ChequerPos> ByBlackHS = new HashSet<ChequerPos>();

        foreach (var ChM in chequers)
        {
            if (ChM.chessman != null)
            {
                List<ChequerPos> confuting = ChM.chessman.Moves().confuting;

                if(ChM.chessman.color == Assets.Color.White)
                {
                    foreach (var ChP in confuting)
                        ByWhiteHS.Add(ChP);
                }
                else
                {
                    foreach (var ChP in confuting)
                        ByBlackHS.Add(ChP);
                }
            }      
        }

        List<ChequerPos> ByWhite = new List<ChequerPos>(ByWhiteHS);
        List<ChequerPos> ByBlack = new List<ChequerPos>(ByBlackHS);

        return (ByWhite, ByBlack);
    }

    private void CreateChessman(ChessmanType newChessmanType, ChequerPos newChequerPos, Assets.Color color)
    {
        if (newChessmanType == ChessmanType.EMPTY) //new chessman cannot be EMPTY
            return;

        if (Check(null, newChequerPos) != CanMoveInto.Empty) //Chequer must be vacant
            return;

        GameObject newChessman;

        switch (newChessmanType)
        {
            case ChessmanType.KNIGHT:
                newChessman = Instantiate(ChKnight);
                break;

            case ChessmanType.ROOK:
                newChessman = Instantiate(ChRook);
                break;

            case ChessmanType.KING:
                newChessman = Instantiate(ChKing);
                break;

            case ChessmanType.QUEEN:
                newChessman = Instantiate(ChQueen);
                break;

            case ChessmanType.BISHOP:
                newChessman = Instantiate(ChBishop);
                break;

            case ChessmanType.PAWN:
                newChessman = Instantiate(ChPawn);
                break;

            default:
                newChessman = new GameObject();
                break;
        }

        newChessman.GetComponent<Chessman>().SetValues(newChequerPos, color);
        chequers[newChequerPos.column, newChequerPos.row].SetChessman(newChessman);
    }

    public void UnmarkAndSwitchoffLights()
    {
        Moves.marked = new ChequerPos();
        Moves.possible = new List<ChequerPos>();
        Moves.confuting = new List<ChequerPos>();

        foreach (var c in chequers)
        {
            c.ResetColor();
        }
    }

    public void GiveColors()
    {
        foreach (var c in Moves.possible)
        {
            chequers[c.column, c.row].SetGreenColor();
        }

        foreach (var c in Moves.confuting)
        {
            chequers[c.column, c.row].SetRedColor();
        }

        chequers[Moves.marked.column, Moves.marked.row].SetBlueColor();
    }

    public bool TryMoveInto(ChequerPos newChequerPos)
    {
        if(Moves.possible.Exists(o => 
                                 o.column == newChequerPos.column &&
                                 o.row == newChequerPos.row))                                
        {
            MoveTo(newChequerPos);
            return true;
        }
        else if(Moves.confuting.Exists(o =>
                                       o.column == newChequerPos.column &&
                                       o.row == newChequerPos.row))
        {
            ConfuteAndMove(newChequerPos);
            return true;
        }

        return false;
    }

    private void MoveTo(ChequerPos newChequerPos)
    {
        //chequers[newChequerPos.column, newChequerPos.row].chessman = chequers[Moves.marked.column, Moves.marked.row].chessman;
        chequers[Moves.marked.column, Moves.marked.row].chessman.SetValues(newChequerPos, null);
        //chequers[Moves.marked.column, Moves.marked.row].chessman = null;
        ChangeTurn();

        UnmarkAndSwitchoffLights();
    }

    private void ConfuteAndMove(ChequerPos newChequerPos)
    {
        chequers[newChequerPos.column, newChequerPos.row].chessman.ConfutedHandler += MoveTo; //MoveTo() is used in time of animation...
        chequers[newChequerPos.column, newChequerPos.row].chessman.Confution();               //...animation is started in Confution()
    }

    private void ChangeTurn()
    {
        if (actualTurn == Assets.Color.White)
            actualTurn = Assets.Color.Black;
        else
            actualTurn = Assets.Color.White;

        turnChange?.Invoke(actualTurn);
    }

    public CanMoveInto Check(Assets.Color? YourColor, ChequerPos Pos, List<ChequerPos> ByWhite = null, List<ChequerPos> ByBlack = null, CubeRS[,] TableOfChequers = null)
    {
        CubeRS[,] LocalChequers;

        if (TableOfChequers != null)
        {
            LocalChequers = TableOfChequers;
        }
        else
        {
            LocalChequers = chequers;
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
