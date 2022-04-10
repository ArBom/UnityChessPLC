using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Chessman : MonoBehaviour
    {
        public Color color;
        protected ChessmanType chessmanType;
        protected Mesh mesh;

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
    }
}
