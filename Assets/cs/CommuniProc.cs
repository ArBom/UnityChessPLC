using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharp7;

public class CommuniProc : MonoBehaviour
{
    const string IPaddr = "192.168.0.100";
    private S7Client s7Client;

    public static CommuniProc Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
