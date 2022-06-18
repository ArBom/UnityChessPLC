using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Assets;
using System.Threading;
using System;

public class ProcPromotionWin : MonoBehaviour
{
    Animation animation;

    public delegate void ColorChange(Assets.Color newColor);
    public event ColorChange colorChange;
    private ChessmanType chosen;

    // Start is called before the first frame update
    void Start()
    {
        animation = this.GetComponent<Animation>();        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ChessmanType ChooseNewAsync(Assets.Color actualColor)
    {
        colorChange?.Invoke(actualColor);
        animation.Play("ShowPromotionWin");
        //TODO wait until Chosen()
        return chosen;
    }

    public void Chosen(ChessmanType newChessmanType)
    {
        chosen = newChessmanType;
        animation.Play("HidePromotionWin");

    }
}
