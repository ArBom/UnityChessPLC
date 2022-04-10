using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

[RequireComponent(typeof(MeshFilter))]
public class ProcBishop : Chessman
{
    new Mesh mesh;
    new Vector3[] points;
    new int[] triangleElements;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        chessmanType = ChessmanType.BISHOP;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeData();
        CreateMesh();
    }

    void MakeData()
    {
        points = new Vector3[] {
            new Vector3(0f, .45f, 0f), new Vector3(0f, .45f, 0f),

            new Vector3(0f, 0.42f, 0.02f), new Vector3(0.00563f, 0.42f, 0.01919f), new Vector3(0.01081f, 0.42f, 0.01683f), new Vector3(0.01511f, 0.42f, 0.0131f), new Vector3(0.01819f, 0.42f, 0.00831f), new Vector3(0.0198f, 0.42f, 0.00285f), new Vector3(0.0198f, 0.42f, -0.00285f), new Vector3(0.01819f, 0.42f, -0.00831f), new Vector3(0.01511f, 0.42f, -0.0131f), new Vector3(0.01081f, 0.42f, -0.01683f), new Vector3(0.00563f, 0.42f, -0.01919f), new Vector3(0f, 0.42f, -0.02f), /*<-14*/    new Vector3(0f, 0.42f, -0.02f), new Vector3(-0.00563f, 0.42f, -0.01919f), new Vector3(-0.01081f, 0.42f, -0.01683f), new Vector3(-0.01511f, 0.42f, -0.0131f), new Vector3(-0.01819f, 0.42f, -0.00831f), new Vector3(-0.0198f, 0.42f, -0.00285f), new Vector3(-0.0198f, 0.42f, 0.00285f), new Vector3(-0.01819f, 0.42f, 0.00831f), new Vector3(-0.01511f, 0.42f, 0.0131f), new Vector3(-0.01081f, 0.42f, 0.01683f), new Vector3(-0.00563f, 0.42f, 0.01919f), new Vector3(0f, 0.42f, 0.02f),
            new Vector3(0f, 0.38f, 0.06f), new Vector3(0.0169f, 0.38f, 0.05757f), new Vector3(0.03244f, 0.38f, 0.05048f), new Vector3(0.04534f, 0.38f, 0.03929f), new Vector3(0.05458f, 0.38f, 0.02492f), new Vector3(0.05939f, 0.38f, 0.00854f), new Vector3(0.05939f, 0.38f, -0.00854f), new Vector3(0.05458f, 0.38f, -0.02492f), new Vector3(0.04534f, 0.38f, -0.03929f), new Vector3(0.03244f, 0.38f, -0.05048f), new Vector3(0.0169f, 0.38f, -0.05757f), new Vector3(0f, 0.38f, -0.06f),   new Vector3(0f, 0.38f, -0.06f), new Vector3(-0.0169f, 0.38f, -0.05757f), new Vector3(-0.03244f, 0.38f, -0.05048f), new Vector3(-0.04534f, 0.38f, -0.03929f), new Vector3(-0.05458f, 0.38f, -0.02492f), new Vector3(-0.05939f, 0.38f, -0.00854f), new Vector3(-0.05939f, 0.38f, 0.00854f), new Vector3(-0.05458f, 0.38f, 0.02492f), new Vector3(-0.04534f, 0.38f, 0.03929f), new Vector3(-0.03244f, 0.38f, 0.05048f), new Vector3(-0.0169f, 0.38f, 0.05757f), new Vector3(0f, 0.38f, 0.06f),
            new Vector3(0f, 0.35f, 0.08f), new Vector3(0.02254f, 0.35f, 0.07676f), new Vector3(0.04325f, 0.35f, 0.0673f), new Vector3(0.06046f, 0.35f, 0.05239f), new Vector3(0.07277f, 0.35f, 0.03323f), new Vector3(0.07919f, 0.35f, 0.01139f), new Vector3(0.07919f, 0.35f, -0.01139f), new Vector3(0.07277f, 0.35f, -0.03323f), new Vector3(0.06046f, 0.35f, -0.05239f), new Vector3(0.04325f, 0.35f, -0.0673f), new Vector3(0.02254f, 0.35f, -0.07676f), new Vector3(0f, 0.35f, -0.08f),   new Vector3(0f, 0.35f, -0.08f), new Vector3(-0.02254f, 0.35f, -0.07676f), new Vector3(-0.04325f, 0.35f, -0.0673f), new Vector3(-0.06046f, 0.35f, -0.05239f), new Vector3(-0.07277f, 0.35f, -0.03323f), new Vector3(-0.07919f, 0.35f, -0.01139f), new Vector3(-0.07919f, 0.35f, 0.01139f), new Vector3(-0.07277f, 0.35f, 0.03323f), new Vector3(-0.06046f, 0.35f, 0.05239f), new Vector3(-0.04325f, 0.35f, 0.0673f), new Vector3(-0.02254f, 0.35f, 0.07676f), new Vector3(0f, 0.35f, 0.08f),
            new Vector3(0f, 0.325f, 0.06f), new Vector3(0.0169f, 0.325f, 0.05757f), new Vector3(0.03244f, 0.325f, 0.05048f), new Vector3(0.04534f, 0.325f, 0.03929f), new Vector3(0.05458f, 0.325f, 0.02492f), new Vector3(0.05939f, 0.325f, 0.00854f), new Vector3(0.05939f, 0.325f, -0.00854f), new Vector3(0.05458f, 0.325f, -0.02492f), new Vector3(0.04534f, 0.325f, -0.03929f), new Vector3(0.03244f, 0.325f, -0.05048f), new Vector3(0.0169f, 0.325f, -0.05757f), new Vector3(0f, 0.325f, -0.06f),   new Vector3(0f, 0.325f, -0.06f), new Vector3(-0.0169f, 0.325f, -0.05757f), new Vector3(-0.03244f, 0.325f, -0.05048f), new Vector3(-0.04534f, 0.325f, -0.03929f), new Vector3(-0.05458f, 0.325f, -0.02492f), new Vector3(-0.05939f, 0.325f, -0.00854f), new Vector3(-0.05939f, 0.325f, 0.00854f), new Vector3(-0.05458f, 0.325f, 0.02492f), new Vector3(-0.04534f, 0.325f, 0.03929f), new Vector3(-0.03244f, 0.325f, 0.05048f), new Vector3(-0.0169f, 0.325f, 0.05757f), new Vector3(0f, 0.325f, 0.06f),
            new Vector3(0f, .3f, 0f), new Vector3(0f, .3f, 0f),
        };

        triangleElements = new int[] {
            0,2,3, 0,3,4, 0,4,5, 0,5,6, 0,6,7, 0,7,8, 0,8,9, 0,9,10, 0,10,11, 0,11,12, 0,12,13, 0,13,14, 0,2,14,
            1,15,16, 1,16,17, 1,17,18, 1,18,19, 1,19,20, 1,20,21, 1,21,22, 1,22,23, 1,23,24, 1,24,25, 1,25,26,
            //2,3,26,25, 3,4,27,26, 4,5,28,27, 5,6,29,28, 6,7,30,29, 7,8,31,30, 8,9,32,31, 9,10,33,32, 10,11,34,33, 11,12,35,34,      13,14,
            //25,26,49,48, 26,27,50,49, 27,28,51,50, 28,29,52,51, 29,30,53,52, 30,31,54,53, 31,32,55,54, 32,33,56,55, 33,34,57,56, 34,35,58,57,
            //48,49,72,71, 49,50,73,72, 50,51,74,73, 51,52,75,74, 52,53,76,75, 53,54,77,76, 54,55,78,77, 55,56,79,78, 56,57,80,79, 57,58,81,80,
        };
    }

    /*void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = points;
        mesh.triangles = triangleElements;
    }*/
}
