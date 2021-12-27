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
        //MeshFilter my_mf;
        //MeshRenderer my_mr;
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

        protected void Update()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit rh = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out rh, 200))
                {
                    if (rh.transform)
                    {
                        print("Clicked");
                    }
                }
            }
        }

        public void Clicked(GameObject go)
        {
            print(go.name);
        }

        protected void CreateMesh()
        {
            mesh.Clear();
            mesh.vertices = points;
            mesh.triangles = triangleElements;
        }
    }
}
