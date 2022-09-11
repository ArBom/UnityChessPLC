using Assets;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.cs;
using System.Threading.Tasks;

/*
 * row            N
 * ↓            W ╬ E
 * .              S
 * 4  █ █
 * 3 █ █
 * 2  █ █      (A2)=(chequers[0,1])=(ChequerPos2ushort->1)
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

    public AudioClip confutingAC;
    public AudioClip movingAC;
    public AudioClip kingindangerAC;
    private AudioClip toPlay;

    public uint[] s7ChType = new uint[64];
    private uint s7LastClick;
    private uint[] s7Moves = new uint[64];
    private bool s7WhiteTour;

    private History history;
    private HistoryMove historyMove;

    public Assets.Color actualTurn
    {
        get;
        private set;
    }

    public delegate void TurnChange(Assets.Color newColor);
    public event TurnChange turnChange;

    public delegate void MarkedCubeCh(ChequerPos chequerPos, string Icon);
    public event MarkedCubeCh markedCubeCh;

    private ChequerPos ChequerPosAfterPromo;
    private AudioSource audioSource;

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

        for (short c=-1; c!=9; ++c)
        {
            for (short r=-1; r!=9; ++r)
            {
                var p = Instantiate(CubeR, new Vector3(c * 1.0f, -.06f, r * 1.0f), Quaternion.identity);
                p.transform.parent = this.transform;
                p.GetComponent<CubeRS>().SetChequerPos(r, c);

                if(r > -1 && c >-1 && r < 8 && c < 8)
                    chequers[c, r] = p.GetComponent<CubeRS>();
            }
            CubeRS.chessboard = this;
        }

        //list of moves
        Moves.possible = new List<ChequerPos>();
        Moves.confuting = new List<ChequerPos>();
        Moves.protect = new List<ChequerPos>();
        Moves.marked = null;

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
        turnChange += PlayAudioClip;
        turnChange += ResetEP;

        //audio Source
        audioSource = this.GetComponent<AudioSource>();

        //History
        history = new History();
        historyMove = new HistoryMove();
        turnChange += AddHistoryMove;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            if (Input.GetKeyDown("s"))
            {
                history.SaveToFile();
            }
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
        if (Moves.marked == null)
            return false;

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

            //En passant tag
            if (Math.Abs(Moves.marked.row - newChequerPos.row) == 2)
            {
                ((ProcPawn)(chequers[Moves.marked.column, Moves.marked.row].chessman)).EnPassantPossible = true;
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

            toPlay = movingAC;
            MoveTo(newChequerPos);
            return true;
        }
        else if(Moves.confuting.Exists(o =>
                                       o.column == newChequerPos.column &&
                                       o.row == newChequerPos.row))
             {
                toPlay = confutingAC;
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
        short rookSC;
        short rookEC;

        if (kingEnd.column == 2)
        {
            rookSC = 0;
            rookEC = 3;

            //History
            historyMove.Comment = "O-O-O";
        }
        else
        {
            rookSC = 7;
            rookEC = 5;

            //History
            historyMove.Comment = "O-O";
        }

        chequers[rookSC, row].chessman.SetValues(new ChequerPos() {column = (short)rookEC, row = row }, null);

        //History
        historyMove.Castling = true;

        //Move of the king
        MoveTo(kingEnd);
    }

    private void MoveTo(ChequerPos newChequerPos)
    {
        historyMove.ChessmanIcon = FontDic.ChessnansSymbols[chequers[Moves.marked.column, Moves.marked.row].chessman.s7ChType()];

        chequers[Moves.marked.column, Moves.marked.row].chessman.SetValues(newChequerPos, null);

        //History
        historyMove.BeginP = Moves.marked.NameOfThis();

        UnmarkAndSwitchoffLights();

        Checked = CheckChecked();
        if (CheckIsKingsSave().Count != 0)
        {
            toPlay = kingindangerAC;
            historyMove.KingInDanger = true;
        }

        //History
        historyMove.EndP = newChequerPos.NameOfThis();

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

        //History
        historyMove.Promotion = true;
        historyMove.Comment = chessmanType.ToString();
    }

    private void ConfuteAndMove(ChequerPos newChequerPos)
    {
        if (chequers[newChequerPos.column, newChequerPos.row].chessman == null)    //en passant
        {
            //History
            historyMove.Comment = "e.p.";

            chequers[newChequerPos.column, Moves.marked.row].chessman.position.row = newChequerPos.row;
            chequers[newChequerPos.column, Moves.marked.row].chessman.ConfutedHandler += MoveTo;  //MoveTo() is used in time of animation...
            chequers[newChequerPos.column, Moves.marked.row].chessman.Confution();                //...animation is started in Confution()
        }
        else                                                                       //classical
        {
            chequers[newChequerPos.column, newChequerPos.row].chessman.ConfutedHandler += MoveTo; //MoveTo() is used in time of animation...
            chequers[newChequerPos.column, newChequerPos.row].chessman.Confution();               //...animation is started in Confution()
        }

        //History
        historyMove.Confution = true;
    }

    private List<Assets.Color> CheckIsKingsSave(CubeRS[,] chequersIn = null)
    {
        CubeRS[,] chequersT = chequersIn ?? chequers;

        ChequerPos positionOfBlackKing = new ChequerPos();
        ChequerPos positionOfWhiteKing = new ChequerPos();

        foreach (var CRS in chequersT)
            CRS.isKingCheckedHere = false;

        foreach (var CRS in chequersT)
            if (CRS.chessman != null)
                if (CRS.chessman.chessmanType == ChessmanType.KING)
                    if (CRS.chessman.color == Assets.Color.White)
                        positionOfWhiteKing = CRS.chessman.position;
                    else
                        positionOfBlackKing = CRS.chessman.position;

        List<Assets.Color> ToReturn = new List<Assets.Color>(); 

        if (Checked.ByBlack.Any(c =>
                                c.column == positionOfWhiteKing.column &&
                                c.row == positionOfWhiteKing.row))
        {
            chequersT[positionOfWhiteKing.column, positionOfWhiteKing.row].isKingCheckedHere = true;
            ToReturn.Add(Assets.Color.White);
        }

        if (Checked.ByWhite.Any(c =>
                                c.column == positionOfBlackKing.column &&
                                c.row == positionOfBlackKing.row))
        {
            chequersT[positionOfBlackKing.column, positionOfBlackKing.row].isKingCheckedHere = true;
            ToReturn.Add(Assets.Color.Black);
        }

        return ToReturn;
    }

    private void ChangeTurn()
    {
        ////
        s7ChTypeCalc();
        ////

        if (actualTurn == Assets.Color.White)
            actualTurn = Assets.Color.Black;
        else
            actualTurn = Assets.Color.White;

        turnChange?.Invoke(actualTurn);
    }

    private void AddHistoryMove (Assets.Color c)
    {
        history.NewMove(historyMove);
        historyMove = new HistoryMove();
    }

    private void PlayAudioClip(Assets.Color c)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        if (toPlay == movingAC)
            audioSource.volume = 0.3f;
        else
            audioSource.volume = 1f;

        audioSource.PlayOneShot(toPlay);
    }

    //reset En Passant after move
    private void ResetEP(Assets.Color color)
    {
        foreach (var ch in chequers)
        {
            //chequer cannot be empty one
            if (ch.chessman == null)
                continue;

            //chequer have to be taken by pawn
            if (ch.chessman.chessmanType != ChessmanType.PAWN)
                continue;

            //pawn on the chequer have to be opposite
            if (ch.chessman.color != color)
                continue;
           
            //RESET e.p. value below
            ((ProcPawn)(ch.chessman)).EnPassantPossible = false;
        }
    }

    //TODO przemyslec ponizsze
    public CanMoveInto Check(Assets.Color? YourColor, ChequerPos Pos, List<ChequerPos> ByWhite = null, List<ChequerPos> ByBlack = null, CubeRS[,] TableOfChequers = null)
    {
        CanMoveInto Toreturn = CheckMoves.CheckMove(YourColor, Pos, Checked, this.chequers);

        return Toreturn;       
    }

    private void s7ChTypeCalc()
    {
        foreach(var c in chequers)
        {
            if (c.chessman != null)
                s7ChType[ChequerPosHelper.ChequerPos2ushort(c.chequerPos)] = c.chessman.s7ChType();
            else
                s7ChType[ChequerPosHelper.ChequerPos2ushort(c.chequerPos)] = 0;
        }
    }

    public void CubeEnter(ChequerPos chequerPos, string Icon)
    {
        markedCubeCh?.Invoke(chequerPos, Icon);
    }

    public void CubeExit(ChequerPos chequerPos)
    {
        markedCubeCh?.Invoke(chequerPos, null);
    }
}
