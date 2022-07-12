using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using TMPro;

public class CubeRS : MonoBehaviour
{
    public ChequerPos chequerPos = null;
    public Chessman chessman = null;
    public static Chessboard chessboard;
    public Light lightOfCube;

    public Material PureMaterial;
    public Material MuddyMaterial;
    public TextMeshPro TMP;

    private Renderer rend;
    private UnityEngine.Color ColorofSelection = UnityEngine.Color.white;
    protected UnityEngine.Color StartColor;
    public string NameOfThis { get; protected set; }
    public Assets.Color Color { get; protected set; }

    private Animation l_Animator;

    private bool _isKingCheckedHere = false;
    public bool isKingCheckedHere
    {
        get { return _isKingCheckedHere; }
        set
        { 
            _isKingCheckedHere = value;
            if (_isKingCheckedHere)
            {
                l_Animator.Stop();
                l_Animator.Play("KingInDanger");
            }
            else
            {
                l_Animator.Stop();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        if (Color == Assets.Color.Black)
        {
            rend.material = PureMaterial;
        }
        else
        {
            rend.material = MuddyMaterial;
        }

        StartColor = rend.material.GetColor("_Color");
        l_Animator = lightOfCube.GetComponent<Animation>();
    }

    private void OnMouseEnter()
    {
        if (chequerPos.column < 0 || chequerPos.column > 7 || chequerPos.row < 0 || chequerPos.row > 7)
            return;

        ColorofSelection = rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", UnityEngine.Color.magenta);
    }

    private void OnMouseExit()
    {
        if (chequerPos.column < 0 || chequerPos.column > 7 || chequerPos.row < 0 || chequerPos.row > 7)
            return;

        rend.material.SetColor("_Color", ColorofSelection);
    }

    private void OnMouseUpAsButton()
    {
        if (chequerPos == null)
            return;

        if (chequerPos.column < 0 || chequerPos.column > 7 || chequerPos.row < 0 || chequerPos.row > 7)
            return;

        if (this.chessman == null)
        {
            if (chessboard.TryMoveInto(chequerPos))
                return;
        }

        if (this.chessman != null && chessboard.actualTurn == this.chessman.color)
        {
            chessboard.UnmarkAndSwitchoffLights();
            chessboard.Moves = chessman.Moves();
            chessboard.GiveColors();
        }
        else if (this.chessman == null)
        {
            chessboard.TryMoveInto(chequerPos);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetChequerPos(short Row, short Column)
    {
        ChequerPos temp = new ChequerPos
        {
            column = Column,
            row = Row
        };

        if (chequerPos != null)
            return false;

        chequerPos = temp;
        this.Color = (this.chequerPos.column + this.chequerPos.row) % 2 == 0 ? Assets.Color.White : Assets.Color.Black;

        if (Row > -1 && Column > -1 && Row < 8 && Column < 8)
        {
            char name1 = (char)(this.chequerPos.column + 65);
            string name2 = (this.chequerPos.row + 1).ToString();
            NameOfThis = name1 + name2;
            Destroy(TMP);
            //TMP.text = ChequerPosHelper.ChequerPos2ushort(this.chequerPos.Value).ToString();

            return true;
        }
        else
        {
            float newZ = 1;
            float newX = 1;

            if (Row == -1)
            {
                newZ = 0.4f;
                this.transform.Translate(new Vector3(0f, 0f, 0.3f));
                NameOfThis = ((char)(this.chequerPos.column + 65)).ToString();
                TMP.text = NameOfThis;
            }

            if (Row == 8)
            {
                newZ = 0.4f;
                TMP.transform.Rotate(new Vector3(0, 0, 180f));
                TMP.transform.Translate(new Vector3(-0.7f, 0.08f, 0));
                this.transform.Translate(new Vector3(0f, 0f, -0.3f));
                NameOfThis = ((char)(this.chequerPos.column + 65)).ToString();
                TMP.text = NameOfThis;              
            }

            if (Column == -1)
            {
                newX = 0.4f;
                this.transform.Translate(new Vector3(0.3f, 0f, 0));
                NameOfThis = (this.chequerPos.row + 1).ToString();
                TMP.text = NameOfThis;
            }

            if (Column == 8)
            {
                newX = 0.4f;
                TMP.transform.Rotate(new Vector3(0, 0, 180f));
                TMP.transform.Translate(new Vector3(-0.7f, 0.08f, 0));
                this.transform.Translate(new Vector3(-0.3f, 0f, 0));
                NameOfThis = (this.chequerPos.row + 1).ToString();
                TMP.text = NameOfThis;
            }

            if (TMP.text == "0" || TMP.text == "9")
                TMP.text = "■";

            this.transform.localScale = new Vector3(newX, 0.075f, newZ);
            Destroy(lightOfCube);
        }

        return false;
    }

    public void SetChessman(GameObject ChessmanGO)
    {
        chessman = ChessmanGO.GetComponent<Chessman>();
    }

    public void ResetColor()
    {
        ColorofSelection = StartColor;
        rend.material.SetColor("_Color", StartColor);

        lightOfCube.spotAngle = 1;
        lightOfCube.range = 0;

        l_Animator.Stop();

        if(isKingCheckedHere)
            l_Animator.Play("KingInDanger");
    }

    public void SetRedColor()
    {
        if(!isKingCheckedHere)
            l_Animator.Play("SwitchRedLightOn");
    }

    public void SetGreenColor()
    {
        if (!isKingCheckedHere)
            l_Animator.Play("SwitchGreenLightOn");
    }

    public void SetBlueColor()
    {
        l_Animator.Stop();
        l_Animator.Play("SwitchBlueLightOn");
    }
}
