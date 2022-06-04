using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class CubeRS : MonoBehaviour
{
    public ChequerPos? chequerPos = null;
    public Chessman chessman = null;
    public static Chessboard chessboard;

    public Material PureMaterial;
    public Material MuddyMaterial;
    private Renderer rend;
    private UnityEngine.Color ColorofSelection = UnityEngine.Color.white;
    public string NameOfThis { get; protected set; }
    public Assets.Color Color { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        if (Color == Assets.Color.Black)
            rend.material = PureMaterial;
        else
            rend.material = MuddyMaterial;
    }

    private void OnMouseEnter()
    {
        ColorofSelection = rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", UnityEngine.Color.magenta);
    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_Color", ColorofSelection);

        print("exit");
    }

    private void OnMouseUpAsButton()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", UnityEngine.Color.blue);

        if(this.chessman != null && chequerPos.HasValue)
        {
            chessboard.CleanColor();
            chessboard.Moves = chessman.Moves();
            chessboard.GiveColors();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetChequerPos(ushort Row, ushort Column)
    {
        if (!chequerPos.HasValue && Row < 8 && Column < 8)
        {
            ChequerPos temp = new ChequerPos
            {
                column = (short)Column,
                row = (short)Row
            };
            chequerPos = temp;

            this.Color = (this.chequerPos.Value.column + this.chequerPos.Value.row) % 2 == 0 ? Assets.Color.Black : Assets.Color.White;
            char name1 = (char)(this.chequerPos.Value.column + 65);
            string name2 = (this.chequerPos.Value.row + 1).ToString();
            NameOfThis = name1 + name2;

            return true;
        }

        return false;
    }

    public void SetChessman(GameObject ChessmanGO)
    {
        chessman = ChessmanGO.GetComponent<Chessman>();
    }

    public void ResetColor()
    {
        ColorofSelection = UnityEngine.Color.white;
        rend.material.SetColor("_Color", UnityEngine.Color.white);
    }

    public void SetRedColor()
    {
        rend.material.SetColor("_Color", UnityEngine.Color.red);
    }

    public void SetGreenColor()
    {
        rend.material.SetColor("_Color", UnityEngine.Color.green);
    }

    public void SetBlueColor()
    {
        rend.material.SetColor("_Color", UnityEngine.Color.cyan);
        ColorofSelection = rend.material.GetColor("_Color");
    }
}
