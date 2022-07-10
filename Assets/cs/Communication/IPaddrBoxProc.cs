using Assets.cs.Communication;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class IPaddrBoxProc : MonoBehaviour
{
    private InputField inputField;
    protected IPAddress address;

    private void Awake()
    {
        this.inputField = this.GetComponent<InputField>();
        inputField.onValueChanged.AddListener(delegate { OnValueChange(); });
    }

    private void OnValueChange()
    {
        if (IPAddress.TryParse(inputField.text, out address) && inputField.text.Split('.').Length == 4)
        {
            this.GetComponent<Image>().color = new Color(1, 1f, 1f);
            switch (address.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:
                    print("IPv4");
                    break;
                case System.Net.Sockets.AddressFamily.InterNetworkV6:
                    print("IPv6");
                    break;
                default:
                    print("Wrong");
                    break;
            }

            long PingRes = CommHelper.GetPing(address).Result;
            if (PingRes > 0)
            {
                this.GetComponent<Image>().color = new Color(0, .95f, .1f);
                if (CommHelper.TryConnect(address))
                    print("OK");
            }
            else
                this.GetComponent<Image>().color = new Color(.95f, .95f, 0);
        }
        else
        {
            this.GetComponent<Image>().color = new Color(1, .5f, .5f);
            print("Not IP");
        }
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
