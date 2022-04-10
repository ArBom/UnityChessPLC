using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class cubeS : MonoBehaviour
{
    public ChequerPos? chequerPos = null;

    public static Chessboard chessboard;

    public Material PureMaterial;
    public Material MuddyMaterial;
    private Renderer rend;
    public string NameOfThis { get; protected set; }
    public Assets.Color color { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        if (color == Assets.Color.Black)
            rend.material = PureMaterial;
        else
            rend.material = MuddyMaterial;
    }

    private void OnMouseEnter()
    {
        //rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", rend.material.GetColor("_Color"));
        rend.material.SetColor("_Color", UnityEngine.Color.red);

        print("entered");
    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_Color", rend.material.GetColor("_SpecColor"));
        rend.material.SetColor("_Color", UnityEngine.Color.white);

        print("exit");
    }

    private void OnMouseUpAsButton()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", UnityEngine.Color.blue);

        if (this.chequerPos.HasValue)
        {
            print("Row: " + this.chequerPos.Value.row + ", Column: " + this.chequerPos.Value.column + " " + NameOfThis);

            var foundCubeR = FindObjectOfType<Chessboard>();

            chessboard.Clicked(this.chequerPos.Value.row);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetChequerPos(ushort Row, ushort Column)
    {
        if (!chequerPos.HasValue && Row<8 && Column<8)
        {
            ChequerPos temp = new ChequerPos();
            temp.column = (ushort)Column;
            temp.row = (ushort)Row;
            chequerPos = temp;

            this.color = (this.chequerPos.Value.column + this.chequerPos.Value.row) % 2 == 0 ? Assets.Color.Black : Assets.Color.White;
            char name1 = (char)(this.chequerPos.Value.column + 65);
            string name2 = (this.chequerPos.Value.row + 1).ToString();
            NameOfThis = name1 + name2;

            return true;
        }

        return false;
    }

    public void SetColor()
    {
        rend.material.SetColor("_Color", UnityEngine.Color.yellow);
    }
}
