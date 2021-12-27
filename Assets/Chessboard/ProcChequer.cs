using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class ProcChequer : MonoBehaviour
{
    [SerializeField]
    ChequerPos chequerPos;

    new string name;
    public Assets.Color color;
    public Chessman chessman;

    protected void Awake()
    {
        SetData();
    }

    void Start()
    {

    }

    private void Update()
    {

    }

    private void SetData()
    {
        color = (this.chequerPos.column + this.chequerPos.row) % 2 == 0 ? Assets.Color.Black : Assets.Color.White;

        char name1 = (char)(this.chequerPos.column + 65);
        name = name1 + this.chequerPos.row.ToString();
    }
}
