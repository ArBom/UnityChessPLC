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

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        chessboard.turnChange += TurnTo;
        Info.text = "\uE115";
    }

    private void OnMouseUpAsButton()
    {
        settiIcon = !settiIcon;
        if (settiIcon)
        {
            Info.text = "\uE115";
        }
        else
            Info.text = "\uE0D5";

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
