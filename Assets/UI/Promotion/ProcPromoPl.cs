using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class ProcPromoPl : MonoBehaviour
{
    public GameObject chessman;
    public Light light;
    private ChessmanType thisChessmanType;
    private Animation animation;
    private GameObject promoChessman;

    public ProcPromotionWin procPromotionWin;

    // Start is called before the first frame update
    void Start()
    {
        procPromotionWin.colorChange += UpdateMaterial;

        UnityEngine.Component ch;

        if (chessman.TryGetComponent(typeof(ProcQueen), out ch))
            thisChessmanType = ChessmanType.QUEEN;
        else if ((chessman.TryGetComponent(typeof(ProcKnight), out ch)))
            thisChessmanType = ChessmanType.KNIGHT;
        else if ((chessman.TryGetComponent(typeof(ProcRook), out ch)))
            thisChessmanType = ChessmanType.ROOK;
        else if ((chessman.TryGetComponent(typeof(ProcBishop), out ch)))
            thisChessmanType = ChessmanType.BISHOP;

        ch = null;

        animation = light.GetComponent<Animation>();

        promoChessman = Instantiate(chessman);

        Vector3 chessLocalPosition = new Vector3();

        switch (thisChessmanType)
        {
            case ChessmanType.QUEEN:
                chessLocalPosition = new Vector3(-0.3f, -0.66f, -1.65f);
                break;

            case ChessmanType.KNIGHT:
                chessLocalPosition = new Vector3(-0.1f, -0.66f, -1.65f);
                break;

            case ChessmanType.ROOK:
                chessLocalPosition = new Vector3(0.1f, -0.66f, -1.65f);
                break;

            case ChessmanType.BISHOP:
                chessLocalPosition = new Vector3(0.3f, -0.66f, -1.65f);
                break;
        }

        print(thisChessmanType);

        var trParent = this.transform.parent;
        promoChessman.transform.SetParent(trParent, false);
        promoChessman.transform.localScale = new Vector3(.25f, 1f, .25f);
        var ChessmanComp = promoChessman.GetComponent<Chessman>();

        promoChessman.GetComponent<Renderer>().material = ChessmanComp.PureMaterial;
        promoChessman.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        Animation chessAnim = promoChessman.GetComponent<Animation>();
        chessAnim.wrapMode = WrapMode.Loop;
        chessAnim.Play("ChessmanRotate");

        ChessmanComp.transform.localPosition = chessLocalPosition;
    }

    void UpdateMaterial(Assets.Color newColor)
    {
        var ChessmanComp = promoChessman.GetComponent<Chessman>();

        if (newColor == Assets.Color.White)
            promoChessman.GetComponent<Renderer>().material = ChessmanComp.PureMaterial;
        else
            promoChessman.GetComponent<Renderer>().material = ChessmanComp.MuddyMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        animation.Play("SwitchBlueLightOn");
    }

    private void OnMouseExit()
    {
        animation.Stop();
        light.range = 0f;
    }

    private void OnMouseUpAsButton()
    {
        procPromotionWin.Chosen(thisChessmanType);
    }
}
