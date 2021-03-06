using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using TMPro;

public class ProcUI : MonoBehaviour
{
    new private Animation animation;
    public Chessboard chessboard;
    public TextMeshPro Info;

    private bool settiIcon = true;
    public delegate void Clicked(bool setti);
    public event Clicked clicked;

    const string SettiI = "\uE115";
    const string BackI = "\uE0D5";

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        chessboard.turnChange += TurnTo;
        chessboard.markedCubeCh += ShowChessman;
        Info.text = SettiI;
    }

    private void ShowChessman(ChequerPos chequerPos, string Icon)
    {
        if (Icon == null)
        {
            if (settiIcon)
                Info.text = SettiI;
            else
                Info.text = BackI;
        }
        else
            Info.text = Icon;
    }

    private void OnMouseUpAsButton()
    {
        settiIcon = !settiIcon;

        if (settiIcon)
            Info.text = SettiI;
        else
            Info.text = BackI;

        clicked?.Invoke(settiIcon);
    }

    void TurnTo(Assets.Color newColor)
    {
        if (newColor == Assets.Color.White)
            animation.Play("TurnToWhite");
        else
            animation.Play("TurnToBlack");
    }
}
