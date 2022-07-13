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

    public delegate void Choose(ChessmanType chosen);
    public event Choose choose;

    private bool IsShown = false;

    // Start is called before the first frame update
    void Start()
    {
        animation = this.GetComponent<Animation>();
        SetUnactive();
    }

    public void ShowYourself(Assets.Color actualColor)
    {
        if (!IsShown)
        {
            SetActive();
            colorChange?.Invoke(actualColor);
            IsShown = true;
            animation.Play("ShowPromotionWin");
        }
    }

    public void HideYourself()
    {
        if (IsShown)
        {
            IsShown = false;
            animation.Play("HidePromotionWin");
        }
    }

    public void Chosen(ChessmanType newChessmanType)
    {
        if (IsShown)
        {
            IsShown = false;
            animation.Play("HidePromotionWin"); //In the end of animation call SetUnactive()
            choose?.Invoke(newChessmanType);
        }
    }

    private void SetActive()
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);
    }

    private void SetUnactive() //Its called in the end of "HidePromotionWin" animation
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
