using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

[RequireComponent(typeof(MeshFilter))]
public class ProcBishop : Chessman
{
    private void Awake()
    {
        MakeData();
        AddPier(true, true, .275f, .15f);
        Marge();

        chessmanType = ChessmanType.BISHOP;
    }

    void MakeData()
    {
        pointsOfCoping = new Vector3[]
        {
            new Vector3(0f, .45f, 0f), new Vector3(0f, .38f, 0f),

            new Vector3(0f, 0.42f, 0.02f), new Vector3(0.00563f, 0.42f, 0.01919f), new Vector3(0.01081f, 0.42f, 0.01683f), new Vector3(0.01511f, 0.42f, 0.0131f), new Vector3(0.01819f, 0.42f, 0.00831f), new Vector3(0.0198f, 0.42f, 0.00285f), new Vector3(0.0198f, 0.42f, -0.00285f), new Vector3(0.01819f, 0.42f, -0.00831f), new Vector3(0.01511f, 0.42f, -0.0131f), new Vector3(0.01081f, 0.42f, -0.01683f), new Vector3(0.00563f, 0.42f, -0.01919f), new Vector3(0f, 0.42f, -0.02f),  new Vector3(0f, 0.42f, -0.02f), new Vector3(-0.00563f, 0.42f, -0.01919f), new Vector3(-0.01081f, 0.42f, -0.01683f), new Vector3(-0.01511f, 0.42f, -0.0131f), new Vector3(-0.01819f, 0.42f, -0.00831f), new Vector3(-0.0198f, 0.42f, -0.00285f), new Vector3(-0.0198f, 0.42f, 0.00285f), new Vector3(-0.01819f, 0.42f, 0.00831f), new Vector3(-0.01511f, 0.42f, 0.0131f), new Vector3(-0.01081f, 0.42f, 0.01683f), new Vector3(-0.00563f, 0.42f, 0.01919f),
            new Vector3(0f, 0.395f, 0.0f),
            new Vector3(0f, 0.38f, 0.06f), new Vector3(0.0169f, 0.38f, 0.05757f), new Vector3(0.03244f, 0.38f, 0.05048f), new Vector3(0.04534f, 0.38f, 0.03929f), new Vector3(0.05458f, 0.38f, 0.02492f), new Vector3(0.05939f, 0.38f, 0.00854f), new Vector3(0.05939f, 0.38f, -0.00854f), new Vector3(0.05458f, 0.38f, -0.02492f), new Vector3(0.04534f, 0.38f, -0.03929f), new Vector3(0.03244f, 0.38f, -0.05048f), new Vector3(0.0169f, 0.38f, -0.05757f), new Vector3(0f, 0.38f, -0.06f),   new Vector3(0f, 0.38f, -0.06f), new Vector3(-0.0169f, 0.38f, -0.05757f), new Vector3(-0.03244f, 0.38f, -0.05048f), new Vector3(-0.04534f, 0.38f, -0.03929f), new Vector3(-0.05458f, 0.38f, -0.02492f), new Vector3(-0.05939f, 0.38f, -0.00854f), new Vector3(-0.05939f, 0.38f, 0.00854f), new Vector3(-0.05458f, 0.38f, 0.02492f), new Vector3(-0.04534f, 0.38f, 0.03929f), new Vector3(-0.03244f, 0.38f, 0.05048f), new Vector3(-0.0169f, 0.38f, 0.05757f), new Vector3(0f, 0.38f, 0.06f),
            new Vector3(0f, 0.35f, 0.08f), new Vector3(0.02254f, 0.35f, 0.07676f), new Vector3(0.04325f, 0.35f, 0.0673f), new Vector3(0.06046f, 0.35f, 0.05239f), new Vector3(0.07277f, 0.35f, 0.03323f), new Vector3(0.07919f, 0.35f, 0.01139f), new Vector3(0.07919f, 0.35f, -0.01139f), new Vector3(0.07277f, 0.35f, -0.03323f), new Vector3(0.06046f, 0.35f, -0.05239f), new Vector3(0.04325f, 0.35f, -0.0673f), new Vector3(0.02254f, 0.35f, -0.07676f), new Vector3(0f, 0.35f, -0.08f),   new Vector3(0f, 0.35f, -0.08f), new Vector3(-0.02254f, 0.35f, -0.07676f), new Vector3(-0.04325f, 0.35f, -0.0673f), new Vector3(-0.06046f, 0.35f, -0.05239f), new Vector3(-0.07277f, 0.35f, -0.03323f), new Vector3(-0.07919f, 0.35f, -0.01139f), new Vector3(-0.07919f, 0.35f, 0.01139f), new Vector3(-0.07277f, 0.35f, 0.03323f), new Vector3(-0.06046f, 0.35f, 0.05239f), new Vector3(-0.04325f, 0.35f, 0.0673f), new Vector3(-0.02254f, 0.35f, 0.07676f), new Vector3(0f, 0.35f, 0.08f),
            new Vector3(0f, 0.3f, 0.06f), new Vector3(0.0169f, 0.3f, 0.05757f), new Vector3(0.03244f, 0.3f, 0.05048f), new Vector3(0.04534f, 0.3f, 0.03929f), new Vector3(0.05458f, 0.3f, 0.02492f), new Vector3(0.05939f, 0.3f, 0.00854f), new Vector3(0.05939f, 0.3f, -0.00854f), new Vector3(0.05458f, 0.3f, -0.02492f), new Vector3(0.04534f, 0.3f, -0.03929f), new Vector3(0.03244f, 0.3f, -0.05048f), new Vector3(0.0169f, 0.3f, -0.05757f), new Vector3(0f, 0.3f, -0.06f),   new Vector3(0f, 0.3f, -0.06f), new Vector3(-0.0169f, 0.3f, -0.05757f), new Vector3(-0.03244f, 0.3f, -0.05048f), new Vector3(-0.04534f, 0.3f, -0.03929f), new Vector3(-0.05458f, 0.3f, -0.02492f), new Vector3(-0.05939f, 0.3f, -0.00854f), new Vector3(-0.05939f, 0.3f, 0.00854f), new Vector3(-0.05458f, 0.3f, 0.02492f), new Vector3(-0.04534f, 0.3f, 0.03929f), new Vector3(-0.03244f, 0.3f, 0.05048f), new Vector3(-0.0169f, 0.3f, 0.05757f), new Vector3(0f, 0.3f, 0.06f),
            new Vector3(0f, .25f, 0f)
        };

        triangleElementsOfCoping = new int[]
        {
            0,2,3, 0,3,4, 0,4,5, 0,5,6, 0,6,7, 0,7,8, 0,8,9, 0,9,10, 0,10,11, 0,11,12, 0,12,13, 0,13,14, 0,14,15, 0,15,16, 0,16,17, 0,17,18, 0,18,19, 0,19,20, 0,20,21, 0,21,22, 0,22,23, 0,23,24, 0,24,2,
            2,1,3, 3,1,4, 4,1,5, 5,1,6, 6,1,7, 7,1,8, 8,1,9, 9,1,10, 10,1,11, 11,1,12, 12,1,13, 13,1,14, 14,1,15, 15,1,16, 16,1,17, 17,1,18, 18,1,19, 19,1,20, 20,1,21, 21,1,22, 22,1,23, 23,1,24 ,24,1,2,
            25,26,27, 25,27,28, 25,28,29, 25,29,30, 25,30,31, 25,31,32, 25,32,33, 25,33,34, 25,34,35, 25,35,36, 25,36,37, 25,37,38, 25,38,39, 25,39,40, 25,40,41, 25,41,42, 25,42,43, 25,43,44, 25,44,45, 25,45,46, 25,46,47, 25,47,48, 25,48,49, 
            27,26,50, 27,50,51, 28,27,51, 28,51,52, 29,28,52, 29,52,53, 30,29,53, 30,53,54, 31,30,54, 31,54,55, 32,31,55, 32,55,56, 33,32,56, 33,56,57, 34,33,57, 34,57,58, 35,34,58, 35,58,59, 36,35,59, 36,59,60, 37,36,60, 37,60,61, 38,37,61, 38,61,62, 39,38,62, 39,62,63, 40,39,63, 40,63,64, 41,40,64, 41,64,65, 42,41,65, 42,65,66, 43,42,66, 43,66,67, 44,43,67, 44,67,68, 45,44,68, 45,68,69, 46,45,69, 46,69,70, 47,46,70, 47,70,71, 48,47,71, 48,71,72, 49,48,72, 49,72,73,
            73,74,75, 73,75,51, 51,75,76, 51,76,52, 52,76,77, 52,77,53, 53,77,78, 53,78,54, 54,78,79, 54,79,55, 55,79,80, 55,80,56, 56,80,81, 56,81,57, 57,81,82, 57,82,58, 58,82,83, 58,83,59, 59,83,84, 59,84,60, 60,84,85, 60,85,61, 61,85,86, 61,86,62, 62,86,87, 62,87,63, 63,87,88, 63,88,64, 64,88,89, 64,89,65, 65,89,90, 65,90,66, 66,90,91, 66,91,67, 67,91,92, 67,92,68, 68,92,93, 68,93,69, 69,93,94, 69,94,70, 70,94,95, 70,95,71, 71,95,96, 71,96,72, 72,96,97, 72,97,73,
            75,74,98, 76,75,98, 77,76,98, 78,77,98, 79,78,98, 80,79,98, 81,80,98, 82,81,98, 83,82,98, 84,83,98, 85,84,98, 86,85,98, 87,86,98, 88,87,98, 89,88,98, 90,89,98, 91,90,98, 92,91,98, 93,92,98, 94,93,98, 95,94,98, 96,95,98, 97,96,98,
        };

        MakeData2();
    }

    public override (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting) Moves()
    {
        List<ChequerPos> possible = new List<ChequerPos>();
        List<ChequerPos> confuting = new List<ChequerPos>();

        short columnToCheck = position.Value.column;
        short rowToCheck = position.Value.row;

        CanMoveInto canMoveInto;

        do //NE
        {
            rowToCheck++;
            columnToCheck--;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty);
        rowToCheck = position.Value.row;
        columnToCheck = position.Value.column;

        do //SE
        {
            rowToCheck--;
            columnToCheck--;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty);
        rowToCheck = position.Value.row;
        columnToCheck = position.Value.column;

        do //SW
        {
            rowToCheck--;
            columnToCheck++;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty);
        rowToCheck = position.Value.row;
        columnToCheck = position.Value.column;

        do //NW
        {
            columnToCheck++;
            rowToCheck++;
            canMoveInto = chessboard.Check(color, new ChequerPos() { column = columnToCheck, row = rowToCheck });

            if (canMoveInto == CanMoveInto.TakenO)
            {
                confuting.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
            if (canMoveInto == CanMoveInto.Empty)
            {
                possible.Add(new ChequerPos() { column = columnToCheck, row = rowToCheck });
            }
        }
        while (canMoveInto == CanMoveInto.Empty);
        columnToCheck = position.Value.column;

        ChequerPos marked = this.position.Value;

        return (marked, possible, confuting);
    }
}
