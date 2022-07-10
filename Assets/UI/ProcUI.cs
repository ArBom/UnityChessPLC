using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class ProcUI : MonoBehaviour
{
    new private Animation animation;
    public Chessboard chessboard;

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
    }

    private void OnMouseUpAsButton()
    {
        settiIcon = !settiIcon;
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
