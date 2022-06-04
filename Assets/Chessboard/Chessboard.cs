using Assets;
using System;
using System.Collections;
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


    public (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting) Moves;

    private void Awake()
    {
        //chequer table
        chequers = new CubeRS[8,8]; //[column,row]

        for (ushort c=0; c!=8; ++c)
        {
            for (ushort r=0; r!=8; ++r)
            {
                var p = Instantiate(CubeR, new Vector3(c * 1.0f, 0, r * 1.0f), Quaternion.identity); //TODO size
                p.GetComponent<CubeRS>().SetChequerPos(r, c);
                chequers[c, r] = p.GetComponent<CubeRS>();
            }
            CubeRS.chessboard = this;
        }

        //list of moves
        Moves.possible = new List<ChequerPos>();
        Moves.confuting = new List<ChequerPos>();

        //chessmans
        CreateChessman(ChessmanType.KNIGHT, new ChequerPos { column = 1, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.KNIGHT, new ChequerPos { column = 6, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.BISHOP, new ChequerPos { column = 2, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.BISHOP, new ChequerPos { column = 5, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.ROOK, new ChequerPos { column = 3, row = 3 }, Assets.Color.White);
        CreateChessman(ChessmanType.ROOK, new ChequerPos { column = 7, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.KING, new ChequerPos { column = 4, row = 0 }, Assets.Color.White);
        CreateChessman(ChessmanType.QUEEN, new ChequerPos { column = 3, row = 0 }, Assets.Color.White);

        CreateChessman(ChessmanType.PAWN, new ChequerPos { column = 0, row = 1 }, Assets.Color.White);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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



    public void Clicked(ushort row)
    {
        foreach (var a in chequers)
        {
            if (a.chequerPos.Value.row == row)
            {
                //a.SetColor();
            }
        }
    }

    public void CleanColor()
    {
        foreach (var c in chequers)
        {
            c.ResetColor();
        }
    }

    public void GiveColors()
    {
        chequers[Moves.marked.column, Moves.marked.row].SetBlueColor();

        foreach (var c in Moves.possible)
        {
            chequers[c.column, c.row].SetGreenColor();
        }

        foreach (var c in Moves.confuting)
        {
            chequers[c.column, c.row].SetRedColor();
        }
    }

    public CanMoveInto Check(Assets.Color? YourColor, ChequerPos Pos)
    {
        if (Pos.column > 7 || Pos.row > 7 || Pos.column < 0 || Pos.row < 0)
        {
            return CanMoveInto.NoExist;
        }

        if (chequers[Pos.column, Pos.row].chessman == null)
        {
            return CanMoveInto.Empty;
        }

        if (YourColor.HasValue)
        {
            if (chequers[Pos.column, Pos.row].chessman.color == YourColor)
            {
                return CanMoveInto.TakenY;
            }

            if (chequers[Pos.column, Pos.row].chessman.color != YourColor)
            {
                return CanMoveInto.TakenO;
            }
        }

        return CanMoveInto.NoExist;
    }
}
