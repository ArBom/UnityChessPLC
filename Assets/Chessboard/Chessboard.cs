using Assets;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;
using Assets.cs;

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

    public ProcPromotionWin procPromotionWin;

    public Assets.Color actualTurn
    {
        get;
        private set;
    }

    public delegate void TurnChange(Assets.Color newColor);
    public event TurnChange turnChange;

    ChequerPos ChequerPosAfterPromo;

    public (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) Moves;
    public (List<ChequerPos> ByWhite, List<ChequerPos> ByBlack) Checked
    {
        get;
        protected set;
    }

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
        Checked = (new List<ChequerPos>(), new List<ChequerPos>());

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

        CreateChessman(ChessmanType.KING, new ChequerPos { column = 4, row = 7 }, Assets.Color.Black);
        CreateChessman(ChessmanType.QUEEN, new ChequerPos { column = 3, row = 7 }, Assets.Color.Black);

        for (short bp = 0; bp < 8; ++bp)
            CreateChessman(ChessmanType.PAWN, new ChequerPos { column = bp, row = 6 }, Assets.Color.Black);


        //Set the actual turn
        actualTurn = Assets.Color.White;

        //delegates       
        procPromotionWin.choose += Promo;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private (List<ChequerPos> ByWhite, List<ChequerPos> Byblack) CheckChecked(CubeRS[,] chequers = null)
    {
        HashSet<ChequerPos> ByWhiteHS = new HashSet<ChequerPos>();
        HashSet<ChequerPos> ByBlackHS = new HashSet<ChequerPos>();

        CubeRS[,] chequersTemp;

        if (chequers != null)
            chequersTemp = chequers;
        else
            chequersTemp = this.chequers;


        foreach (var ChM in chequersTemp)
        {
            if (ChM.chessman != null)
            {
                List<ChequerPos> confutingAndProtected = ChM.chessman.Moves().protect;
                confutingAndProtected.AddRange(ChM.chessman.Moves().confuting);

                if(ChM.chessman.chessmanType != ChessmanType.PAWN)
                    confutingAndProtected.AddRange(ChM.chessman.Moves().possible);

                if(ChM.chessman.color == Assets.Color.White)
                {
                    foreach (var ChP in confutingAndProtected)
                        ByWhiteHS.Add(ChP);
                }
                else
                {
                    foreach (var ChP in confutingAndProtected)
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
        if(chequers[Moves.marked.column, Moves.marked.row].chessman.chessmanType == ChessmanType.PAWN)
        {
            if(newChequerPos.row == 0 || newChequerPos.row == 7)
            {
                List<ChequerPos> evryPos = new List<ChequerPos>(Moves.possible);
                evryPos.AddRange(Moves.confuting);

                if (evryPos.Exists(o =>
                          o.column == newChequerPos.column &&
                          o.row == newChequerPos.row))
                {
                    PromoAndMoveAgain(newChequerPos);
                    return false;
                }
            }
        }

        if(Moves.possible.Exists(o => 
                                 o.column == newChequerPos.column &&
                                 o.row == newChequerPos.row))                                
        {
            if (chequers[Moves.marked.column, Moves.marked.row].chessman.chessmanType == ChessmanType.KING && Math.Abs(Moves.marked.column - newChequerPos.column) == 2)
            {
                Castling(Moves.marked, newChequerPos);
                return true;
            }

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

        UnmarkAndSwitchoffLights();
        procPromotionWin.HideYourself();
        return false;
    }

    private void Castling (ChequerPos kingStart, ChequerPos kingEnd)
    {
        if (kingStart.row != kingEnd.row)
            return;

        //Move of rook
        short row = kingStart.row;
        int rookSC = kingEnd.column == 2 ? 0 : 7;
        int rookEC = kingEnd.column == 2 ? 3 : 5;

        chequers[rookSC, row].chessman.SetValues(new ChequerPos() {column = (short)rookEC, row = row }, null);

        //Move of the king
        MoveTo(kingEnd);
    }

    private void MoveTo(ChequerPos newChequerPos)
    {
        chequers[Moves.marked.column, Moves.marked.row].chessman.SetValues(newChequerPos, null);

        UnmarkAndSwitchoffLights();

        Checked = CheckChecked();
        CheckIsKingsSave();

        ChangeTurn();
    }

    private void PromoAndMoveAgain(ChequerPos newChequerPos)
    {
        ChequerPosAfterPromo = newChequerPos;

        Assets.Color color = chequers[Moves.marked.column, Moves.marked.row].chessman.color;
        procPromotionWin.ShowYourself(color);

    }

    private void Promo(ChessmanType chessmanType)
    {
        Destroy(chequers[Moves.marked.column, Moves.marked.row].chessman.gameObject);
        chequers[Moves.marked.column, Moves.marked.row].chessman = null;
        CreateChessman(chessmanType, new ChequerPos { column = Moves.marked.column, row = Moves.marked.row }, actualTurn);
        TryMoveInto(ChequerPosAfterPromo);
        ChequerPosAfterPromo = new ChequerPos();
    }

    private void ConfuteAndMove(ChequerPos newChequerPos)
    {
        chequers[newChequerPos.column, newChequerPos.row].chessman.ConfutedHandler += MoveTo; //MoveTo() is used in time of animation...
        chequers[newChequerPos.column, newChequerPos.row].chessman.Confution();               //...animation is started in Confution()
    }

    private List<Assets.Color> CheckIsKingsSave(/*CubeRS[,] chequersIn = null*/) //TODO dopisać dla alternatywnych szachownic
    {
        //CubeRS[,] chequersT = chequersIn == null ? this.chequers : chequersIn;

        ChequerPos positionOfBlackKing = new ChequerPos();
        ChequerPos positionOfWhiteKing = new ChequerPos();

        foreach (var CRS in chequers)
            CRS.isKingCheckedHere = false;

        foreach (var CRS in chequers)
            if (CRS.chessman != null)
                if (CRS.chessman.chessmanType == ChessmanType.KING)
                    if (CRS.chessman.color == Assets.Color.White)
                        positionOfWhiteKing = CRS.chessman.position.Value;
                    else
                        positionOfBlackKing = CRS.chessman.position.Value;

        List<Assets.Color> ToReturn = new List<Assets.Color>(); 

        if (Checked.ByBlack.Any(c =>
                                c.column == positionOfWhiteKing.column &&
                                c.row == positionOfWhiteKing.row))
        {
            chequers[positionOfWhiteKing.column, positionOfWhiteKing.row].isKingCheckedHere = true;
            ToReturn.Add(Assets.Color.White);
        }

        if (Checked.ByWhite.Any(c =>
                                c.column == positionOfBlackKing.column &&
                                c.row == positionOfBlackKing.row))
        {
            chequers[positionOfBlackKing.column, positionOfBlackKing.row].isKingCheckedHere = true;
            ToReturn.Add(Assets.Color.Black);
        }

        return ToReturn;
    }

    private void ChangeTurn()
    {
        if (actualTurn == Assets.Color.White)
            actualTurn = Assets.Color.Black;
        else
            actualTurn = Assets.Color.White;

        turnChange?.Invoke(actualTurn);
    }

    //TODO przemyslec ponizsze
    public CanMoveInto Check(Assets.Color? YourColor, ChequerPos Pos, List<ChequerPos> ByWhite = null, List<ChequerPos> ByBlack = null, CubeRS[,] TableOfChequers = null)
    {
        CanMoveInto Toreturn = CheckMoves.CheckMove(YourColor, Pos, Checked, this.chequers);

        return Toreturn;       
    }
}
