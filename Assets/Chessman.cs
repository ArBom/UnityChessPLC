using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.cs;

namespace Assets
{
    public abstract class Chessman : MonoBehaviour
    {
        public Chessboard chessboard;
        public Color color { get; private set; }
        public Material PureMaterial;
        public Material MuddyMaterial;

        public delegate void ConfutedHandled(ChequerPos position);
        public event ConfutedHandled ConfutedHandler;

        private Renderer rend;

        protected ChessmanType chessmanType = ChessmanType.EMPTY;
        protected Mesh mesh;

        public ChequerPos? position = null;
        private ChequerPos newPosition;
        private float ratioOfMove = 1f;
        private const float speed = 0.4f;

        private Animation chman_Animator;

        //TODO posprzątać poniższe
        const float chSize = 1;
        protected bool nieDrgnal = true;

        protected Vector3[] pointsOfPier;
        protected Vector2[] uvOfPier;
        protected int[] triangleElementsOfpier;
        protected Mesh meshOfPier;

        protected Vector3[] pointsOfCoping;
        protected int[] triangleElementsOfCoping;
        protected Mesh meshOfCoping;

        protected Vector3[] points;
        protected int[] triangleElements;

        // Update is called once per frame
        void Update()
        {
            if (ratioOfMove < 1)
            {
                ratioOfMove += speed * Time.deltaTime;

                if (ratioOfMove >= 1)
                {
                    ratioOfMove = 1;
                    chessboard.chequers[newPosition.column, newPosition.row].chessman = chessboard.chequers[position.Value.column, position.Value.row].chessman;
                    chessboard.chequers[position.Value.column, position.Value.row].chessman = null;
                    position = newPosition;
                }

                this.transform.localPosition = new Vector3(position.Value.column * (1 - ratioOfMove) + newPosition.column * ratioOfMove, 
                                                           0, 
                                                           position.Value.row * (1 - ratioOfMove) + newPosition.row * ratioOfMove);
            }
        }

        public int s7ChType()
        {
            return (int)chessmanType + (int)color;
        }

        protected void CreateMesh()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = points;
            mesh.triangles = triangleElements;
        }

        void Start()
        {
            chman_Animator = this.GetComponent<Animation>();
        }

        protected void Marge()
        {
            var combine = new CombineInstance[2];

            combine[0].mesh = meshOfPier;
            combine[0].transform = transform.localToWorldMatrix;

            combine[1].mesh = meshOfCoping;
            combine[1].transform = transform.localToWorldMatrix;

            mesh = new Mesh();

            mesh.CombineMeshes(combine);
            mesh.RecalculateNormals();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        public bool SetValues(ChequerPos newPos, Assets.Color? newColor)
        {
            if (newPos.row < 8 && newPos.column < 8 && newPos.row >=0 && newPos.column >= 0)
            {
                if (this.position.HasValue)
                {
                    ratioOfMove = 0;
                    newPosition = newPos;
                }
                else
                {
                    position = newPos;
                    this.transform.localPosition = new Vector3(position.Value.column, 0, position.Value.row);
                }

                if (newColor.HasValue)
                {
                    color = newColor.Value;

                    if (color == Color.Black)
                    {
                        this.transform.Rotate(new Vector3(0, 1, 0), 90);
                        rend.material = MuddyMaterial;
                    }
                    else
                    {
                        this.transform.Rotate(new Vector3(0, 1, 0), -90);
                        rend.material = PureMaterial;
                    }
                }
                else
                    nieDrgnal = false;

                return true;
            }

            return false;
        }

        protected void AddPier(bool ripple, bool jabot, float high, float radius)
        {
            Vector3[] PierPoints = new Vector3[99];

            PierPoints[0] = new Vector3(0f, 0.93f * high, 0f);

            if (jabot)
            {
                for (int a = 1; a < 25; a++) //1st up cone
                {
                    if (ripple)
                        PierPoints[a] = new Vector3((float)(0.45f * radius * Math.Sin(2 * Math.PI * a / 24)), (float)(1.05 * high + 0.1 * high * Math.Cos(2 * Math.PI * a / 12)), (float)(0.8f * radius * Math.Cos(2 * Math.PI * a / 24)));
                    else
                        PierPoints[a] = new Vector3((float)(0.8f * radius * Math.Sin(2 * Math.PI * a / 24)), high, (float)(0.8f * radius * Math.Cos(2 * Math.PI * a / 24)));
                }

                PierPoints[25] = new Vector3(0f, high * .7f, 0f); //"top" of up cone /its up side down

                for (int a = 26; a < 50; a++) //2nd up cone
                {
                    if (ripple)
                        PierPoints[a] = new Vector3((float)(.5 * radius * Math.Sin(2 * Math.PI * a / 24)), (float)(0.94f * high + 0.06 * high * Math.Cos(2 * Math.PI * a / 12)), (float)(radius * Math.Cos(2 * Math.PI * a / 24)));
                    else
                        PierPoints[a] = new Vector3((float)(radius * Math.Sin(2 * Math.PI * a / 24)), 0.94f * high, (float)(radius * Math.Cos(2 * Math.PI * a / 24)));

                }
            }

            for (int a = 50; a < 74; a++) //1st down cone
            {
                PierPoints[a] = new Vector3((float)(0.6f * radius * Math.Sin(2 * Math.PI * a / 24)), 0, (float)(0.6f * radius * Math.Cos(2 * Math.PI * a / 24)));
            }

            PierPoints[74] = new Vector3(0f, high * .3f, 0f); //top of 2nd down cone

            for (int a = 75; a < 99; a++) //2nd down cone
            {
                if (a%2 == 0 || !ripple) 
                    PierPoints[a] = new Vector3((float)(radius * Math.Sin(2 * Math.PI * a / 24)), 0, (float)(radius * Math.Cos(2 * Math.PI * a / 24)));
                else
                    PierPoints[a] = new Vector3((float)(0.85 * radius * Math.Sin(2 * Math.PI * a / 24)), 0, (float)(0.85 * radius * Math.Cos(2 * Math.PI * a / 24)));
            }

            int[] PierTriangleElements;

            if (jabot)
            {
                PierTriangleElements = new int[]
                {
                0,1,2, 0,2,3, 0,3,4, 0,4,5, 0,5,6, 0,6,7, 0,7,8, 0,8,9, 0,9,10, 0,10,11, 0,11,12, 0,12,13, 0,13,14, 0,14,15, 0,15,16, 0,16,17, 0,17,18, 0,18,19, 0,19,20, 0,20,21, 0,21,22, 0,22,23, 0,23,24, 0,24,1,
                25,2,1, 25,3,2, 25,4,3, 25,5,4, 25,6,5, 25,7,6, 25,8,7, 25,9,8, 25,10,9, 25,11,10, 25,12,11, 25,13,12, 25,14,13, 25,15,14, 25,16,15, 25,17,16, 25,18,17, 25,19,18, 25,20,19, 25,21,20, 25,22,21, 25,23,22, 25,24,23, 25,1,24,
                //
                0,26,27, 0,27,28, 0,28,29, 0,29,30, 0,30,31, 0,31,32, 0,32,33, 0,33,34, 0,34,35, 0,35,36, 0,36,37, 0,37,38, 0,38,39, 0,39,40, 0,40,41, 0,41,42, 0,42,43, 0,43,44, 0,44,45, 0,45,46, 0,46,47, 0,47,48, 0,48,49, 0,49,26,
                25,27,26, 25,28,27, 25,29,28, 25,30,29, 25,31,30, 25,32,31, 25,33,32, 25,34,33, 25,35,34, 25,36,35, 25,37,36, 25,38,37, 25,39,38, 25,40,39, 25,41,40, 25,42,41, 25,43,42, 25,44,43, 25,45,44, 25,46,45, 25,47,46, 25,48,47, 25,49,48, 25,26,49,
                //
                0,50,51, 0,51,52, 0,52,53, 0,53,54, 0,54,55, 0,55,56, 0,56,57, 0,57,58, 0,58,59, 0,59,60, 0,60,61, 0,61,62, 0,62,63, 0,63,64, 0,64,65, 0,65,66, 0,66,67, 0,67,68, 0,68,69, 0,69,70, 0,70,71, 0,71,72, 0,72,73, 0,73,50,
                74,75,76, 74,76,77, 74,77,78, 74,78,79, 74,79,80, 74,80,81, 74,81,82, 74,82,83, 74,83,84, 74,84,85, 74,85,86, 74,86,87, 74,87,88, 74,88,89, 74,89,90, 74,90,91, 74,91,92, 74,92,93, 74,93,94, 74,94,95, 74,95,96, 74,96,97, 74,97,98, 74,98,75,
                };
            }
            else
            {
                PierTriangleElements = new int[]
                {
                    0,50,51, 0,51,52, 0,52,53, 0,53,54, 0,54,55, 0,55,56, 0,56,57, 0,57,58, 0,58,59, 0,59,60, 0,60,61, 0,61,62, 0,62,63, 0,63,64, 0,64,65, 0,65,66, 0,66,67, 0,67,68, 0,68,69, 0,69,70, 0,70,71, 0,71,72, 0,72,73, 0,73,50,
                    74,75,76, 74,76,77, 74,77,78, 74,78,79, 74,79,80, 74,80,81, 74,81,82, 74,82,83, 74,83,84, 74,84,85, 74,85,86, 74,86,87, 74,87,88, 74,88,89, 74,89,90, 74,90,91, 74,91,92, 74,92,93, 74,93,94, 74,94,95, 74,95,96, 74,96,97, 74,97,98, 74,98,75,
                };
            }

            ///uv below
            float maxHihgt = PierPoints.Max(point => point.y);
            float minHight = PierPoints.Min(point => point.y);
            float maxWide = PierPoints.Max(point => point.z);
            float minWide = PierPoints.Min(point => point.z);
            float maxDeph = PierPoints.Max(point => point.x);
            float minDeph = PierPoints.Min(point => point.x);

            float scaleOfTexture = 1.5f;
            uvOfPier = new Vector2[PierPoints.Length];
            for(int index = 0; index < uvOfPier.Length; index++)
            {
                if (PierPoints[index] != null)
                {
                    float PointHigh = (PierPoints[index].y - minHight) / (maxHihgt - minHight);
                    float PointWide = (PierPoints[index].z - minWide) / (maxWide - minWide);
                    float PointDeph = (PierPoints[index].x - minDeph) / (maxDeph - minDeph);

                    uvOfPier[index] = new Vector2((float)Math.Pow(scaleOfTexture * (PointDeph+1), PointHigh), 2*scaleOfTexture * PointWide);
                }
            }
            ///uv up

            pointsOfPier = PierPoints;
            triangleElementsOfpier = PierTriangleElements;

            meshOfPier = new Mesh();
            meshOfPier.Clear();
            meshOfPier.vertices = PierPoints;
            meshOfPier.triangles = PierTriangleElements;
            meshOfPier.uv = uvOfPier;

            rend = GetComponent<Renderer>();
        }

        protected void MakeData2()
        {
            meshOfCoping = new Mesh();
            meshOfCoping.vertices = pointsOfCoping;
            meshOfCoping.triangles = triangleElementsOfCoping;

            ///uv below
            float maxHihgt = pointsOfCoping.Max(point => point.y);
            float minHight = pointsOfCoping.Min(point => point.y);
            float maxWide = pointsOfCoping.Max(point => point.z);
            float minWide = pointsOfCoping.Min(point => point.z);
            float maxDeph = pointsOfCoping.Max(point => point.x);
            float minDeph = pointsOfCoping.Min(point => point.x);

            float scaleOfTexture = .75f;
            var uvOfCoping = new Vector2[pointsOfCoping.Length];
            for (int index = 0; index < uvOfCoping.Length; index++)
            {
                if (pointsOfCoping[index] != null)
                {
                    float PointHigh = (pointsOfCoping[index].y - minHight) / (maxHihgt - minHight);
                    float PointWide = (pointsOfCoping[index].z - minWide) / (maxWide - minWide);
                    float PointDeph = (pointsOfCoping[index].x - minDeph) / (maxDeph - minDeph);

                    uvOfCoping[index] = new Vector2(1-(float)Math.Pow(scaleOfTexture * (PointDeph + 1), PointHigh), 1- (2 * scaleOfTexture * PointWide));
                }
            }
            ///uv up

            meshOfCoping.uv = uvOfCoping;
        }

        public void Confution()
        {
            chman_Animator.Play("ChessmanConfution");
            //IMPORTANT: Animation starts ConfutionEvent() below
        }

        public void ConfutionEvent()
        {
            ConfutedHandler?.Invoke(position.Value);
            Destroy(this.gameObject, 0.5f);
        }

        public abstract (ChequerPos marked, List<ChequerPos> possible, List<ChequerPos> confuting) Moves();
    }
}
