using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

[RequireComponent(typeof(MeshFilter))]
public class ProcRook : Chessman
{
    private void Awake()
    {
        MakeData();
        AddPier(true, true, .27f, .15f);
        Marge();

        chessmanType = ChessmanType.ROOK;
    }

    void MakeData()
    {
        pointsOfCoping = new Vector3[] 
        {
            new Vector3(0f, .37f, 0f),

            new Vector3(0f, 0.37f, 0.065f), new Vector3(0.0325f, 0.37f, 0.05629f), new Vector3(0.05629f, 0.37f, 0.0325f), new Vector3(0.065f, 0.37f, 0f), new Vector3(0.05629f, 0.37f, -0.0325f), new Vector3(0.0325f, 0.37f, -0.05629f), new Vector3(0f, 0.37f, -0.065f), new Vector3(-0.0325f, 0.37f, -0.05629f), new Vector3(-0.05629f, 0.37f, -0.0325f), new Vector3(-0.065f, 0.37f, 0f), new Vector3(-0.05629f, 0.37f, 0.0325f), new Vector3(-0.0325f, 0.37f, 0.05629f),
            new Vector3(0f, 0.45f, 0.065f), new Vector3(0.0325f, 0.45f, 0.05629f), new Vector3(0.05629f, 0.45f, 0.0325f), new Vector3(0.065f, 0.45f, 0f), new Vector3(0.05629f, 0.45f, -0.0325f), new Vector3(0.0325f, 0.45f, -0.05629f), new Vector3(0f, 0.45f, -0.065f), new Vector3(-0.0325f, 0.45f, -0.05629f), new Vector3(-0.05629f, 0.45f, -0.0325f), new Vector3(-0.065f, 0.45f, 0f), new Vector3(-0.05629f, 0.45f, 0.0325f), new Vector3(-0.0325f, 0.45f, 0.05629f),
            new Vector3(0f, 0.45f, 0.085f), new Vector3(0.0425f, 0.45f, 0.07361f), new Vector3(0.07361f, 0.45f, 0.0425f), new Vector3(0.085f, 0.45f, 0f), new Vector3(0.07361f, 0.45f, -0.0425f), new Vector3(0.0425f, 0.45f, -0.07361f), new Vector3(0f, 0.45f, -0.085f), new Vector3(-0.0425f, 0.45f, -0.07361f), new Vector3(-0.07361f, 0.45f, -0.0425f), new Vector3(-0.085f, 0.45f, 0f), new Vector3(-0.07361f, 0.45f, 0.0425f), new Vector3(-0.0425f, 0.45f, 0.07361f),
            new Vector3(0f, 0.33f, 0.075f), new Vector3(0.0375f, 0.33f, 0.06495f), new Vector3(0.06495f, 0.33f, 0.0375f), new Vector3(0.075f, 0.33f, 0f), new Vector3(0.06495f, 0.33f, -0.0375f), new Vector3(0.0375f, 0.33f, -0.06495f), new Vector3(0f, 0.33f, -0.075f), new Vector3(-0.0375f, 0.33f, -0.06495f), new Vector3(-0.06495f, 0.33f, -0.0375f), new Vector3(-0.075f, 0.33f, 0f), new Vector3(-0.06495f, 0.33f, 0.0375f), new Vector3(-0.0375f, 0.33f, 0.06495f),
            
            new Vector3(0f, .2f, 0f),
        };

        triangleElementsOfCoping = new int[] 
        {
            0,1,2, 0,2,3, 0,3,4, 0,4,5, 0,5,6, 0,6,7, 0,7,8, 0,8,9, 0,9,10, 0,10,11, 0,11,12, 0,12,1,
            1,14,2, 1,13,14, 2,15,3, 2,14,15, 4,17,5, 4,16,17, 5,18,6, 5,17,18, 7,20,8, 7,19,20, 8,21,9, 8,20,21, 10,23,11, 10,22,23, 11,24,12, 11,23,24,
            14,13,25, 14,25,26, 15,14,26, 15,26,27, 17,16,28, 17,28,29, 18,17,29, 18,29,30, 20,19,31, 20,31,32, 21,20,32, 21,32,33, 23,22,34, 23,34,35, 24,23,35, 24,35,36,
            3,40,4, 3,39,40, 6,43,7, 6,42,43, 9,46,10, 9,45,46, 12,37,1, 12,48,37,
            13,37,25, 1,37,13, 3,27,39, 3,15,27, 4,28,16, 4,40,28, 6,18,42, 18,30,42, 7,31,19, 31,7,43, 9,21,33, 33,45,9, 10,34,22, 10,46,34, 12,24,36, 12,36,48,
            37,38,25, 38,26,25, 38,39,26, 39,27,26, 40,41,29, 40,29,28, 41,42,30, 41,30,29, 43,44,32, 43,32,31, 44,45,33, 44,33,32, 46,47,35, 46,35,34, 47,48,36, 47,36,35,
            49,48,47, 49,47,46, 49,46,45, 49,45,44, 49,44,43, 49,43,42, 49,42,41, 49,41,40, 49,40,39, 49,39,38, 49,38,37, 49,37,48,
        };

        MakeData2();
    }

    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting, List<ChequerPos> protect) Moves()
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();
        List<ChequerPos> protect = new List<ChequerPos>();

        (possible, confuting, protect) = CheckLineMoves.NonObliqueLinesOfMove(this.position, this.color, this.chessboard);

        ChequerPos marked = this.position;

        return (marked, possible, confuting, protect);
    }
}
