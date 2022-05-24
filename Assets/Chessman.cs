using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public abstract class Chessman : MonoBehaviour
    {
        public Chessboard chessboard;
        public Color color { get; private set; }
        protected ChessmanType chessmanType;
        protected Mesh mesh;
        public ChequerPos? position = null;
        const float chSize = 1;

        protected Vector3[] points;
        protected int[] triangleElements;

        public int s7ChType()
        {
            return (int)chessmanType + (int)color;
        }

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        private void Update()
        {
           
        }

        protected void CreateMesh()
        {
            mesh.Clear();
            mesh.vertices = points;
            mesh.triangles = triangleElements;
        }

        public bool SetValues(ChequerPos newPos, Assets.Color newColor)
        {
            if (!position.HasValue && newPos.row < 8 && newPos.column < 8 && newPos.row >=0 && newPos.column >= 0)
            {
                position = newPos;
                color = newColor;

                if (color == Color.Black)
                {
                    this.transform.Rotate(new Vector3(0, 1, 0), 90);
                }
                else
                {
                    this.transform.Rotate(new Vector3(0, 1, 0), -90);
                }

                this.transform.Translate(chSize * position.Value.row, 0, chSize * -position.Value.column);

                return true;
            }

            return false;
        }

        public abstract (List<ChequerPos> possible, List<ChequerPos> confuting) Moves();
    }
}
