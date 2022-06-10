using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class ProcUI : MonoBehaviour
{
    private Animation animation;
    public Chessboard chessboard;

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        chessboard.turnChange += TurnTo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnTo(Assets.Color newColor)
    {
        if (newColor == Assets.Color.White)
            animation.Play("TurnToWhite");
        else
            animation.Play("TurnToBlack");
    }
}
