using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class ProcKnight : Chessman
{
    private void Awake()
    {
        MakeData();
        AddPier(true, false, .26f, .11f);
        Marge();

        chessmanType = ChessmanType.KNIGHT;
    }

    /*private void OnMouseUpAsButton()
    {
        
    }*/


    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting) Moves() //IN: position of moving chessman
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();

        short columnToCheck = position.Value.column;
        short rowToCheck = position.Value.row;

        CanMoveInto canMoveIntoTemp;
        ChequerPos chequerPosTemp;

        //TODO all colors below

        //NNE
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row + 2) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //NEE
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column + 2), row = (short)(position.Value.row + 1) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SEE
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column + 2), row = (short)(position.Value.row - 1) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SEE
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column + 1), row = (short)(position.Value.row - 2) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SSW
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column - 2), row = (short)(position.Value.row - 1) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //SWW
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row - 2) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //NWW
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column - 2), row = (short)(position.Value.row + 1) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        //NNW
        chequerPosTemp = new ChequerPos() { column = (short)(position.Value.column - 1), row = (short)(position.Value.row + 2) };
        canMoveIntoTemp = this.chessboard.Check(color, chequerPosTemp);
        if (canMoveIntoTemp == CanMoveInto.Empty)
            possible.Add(chequerPosTemp);
        else if (canMoveIntoTemp == CanMoveInto.TakenO)
            confuting.Add(chequerPosTemp);

        ChequerPos marked = this.position.Value;

        return (marked, possible, confuting);
    }

    void MakeData()
    {
        pointsOfCoping = new Vector3[] 
        {
            new Vector3(-0.015f, .40f, -0.07f), new Vector3(-0.015f, .40f, .07f), new Vector3(0f, .35f, -0.07f), new Vector3(0f, .35f, .07f),
            new Vector3(0.04f, .45f, -.059f), new Vector3(0.04f, .45f, .059f), new Vector3(0f, .4f, -.025f), new Vector3(0f, .4f, .025f),
            new Vector3(0.1f, .30f, -.059f), new Vector3(0.1f, .30f, .059f), new Vector3(0.1f, .25f, -.059f), new Vector3(0.1f, .25f, .059f),
            new Vector3(0f, .25f, -.059f), new Vector3(0, .25f, .059f),
            new Vector3(0.09f, .15f, -.059f), new Vector3(0.09f, .15f, .059f),
            new Vector3(0.085f, .12f, -.044f), new Vector3(0.085f, .12f, .044f), new Vector3(-0.05f, .12f, -.054f), new Vector3(-0.05f, .12f, .054f),
            //przod głowy powyżej
            new Vector3(-0.065f, .39f, 0.034f), new Vector3(-0.065f, .39f, -0.034f), new Vector3(-0.089f, .36f, 0.02f), new Vector3(-0.089f, .36f, -0.02f), new Vector3(-0.105f, .3f, 0.024f), new Vector3(-0.105f, .3f, -0.024f), new Vector3(-0.1f, .12f, 0f),
            //grzbiet i uszy powyżej
            new Vector3(-0.085f, .12f, 0.028f), new Vector3(-0.085f, .12f, -0.028f)
        };

        triangleElementsOfCoping = new int[] 
        {
            6,7,2, 2,7,3, 2,4,6, 3,7,5, 0,4,2, 1,3,5,
            2,3,8, 3,9,8, 8,9,10, 9,11,10, 2,10,12, 2,8,10, 3,13,9, 11,9,13, 13,15,14, 12,13,14,
            14,15,16, 15,17,16, 15,19,17, 14,16,18, 13,19,15, 12,14,18,
            //przód głowy powyżej
            6,20,7, 6,21,20, 5,20,1, 7,20,5, 21,4,0, 21,6,4, 20,21,22, 21,23,22, 22,23,24, 23,25,24, 24,25,26,
            //grzbiet i uszy powyżej
            3,1,13,  0,2,12, 13,1,20, 0,12,21, 13,20,19, 12,18,21, 25,28,26, 24,26,27, 18,28,25,
            19,24,27, 18,23,21, 18,25,23, 19,22,24, 19,20,22,
        };

        MakeData2();
    }
}
