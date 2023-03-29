using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game4automation;
using TMPro;
using UnityEngine.Networking;


public class CarrierNumberDisplay : MonoBehaviour
{
    public TMP_Text textNodeIn;
    public TMP_Text textNodeOut;
    public TMP_Text machineName;
    public string machineNameString;

    //No Lagging 
    public string isItThere;
    private float timer;
    private float pollTime;


    public string numbersIn;
    public string numbersOut;
    public OPCUA_Interface oPCUA_Interface;

    public string numberGuide;
    public string numberGuide2;

    
    public OPCUA_Node nodeIn;
    public OPCUA_Node nodeOut;

    public string nodeID;

    // Start is called before the first frame update
    void Start()
    {
        oPCUA_Interface.EventOnConnected.AddListener(OnConnected);
        oPCUA_Interface.EventOnDisconnected.AddListener(OnDisconnected);
        machineName.text = machineNameString;// Name of machine
        //StartCoroutine(DownloadImage(urlImage));
    }

    private void OnReconnecting()
    {
        Debug.Log("Reconnecting");
    }

    private void Update()
    {
        if (timer < pollTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            pollTime = 1.5f;// Updates every second and a half
            textNodeIn.text = numberGuide + oPCUA_Interface.ReadNodeValue(nodeIn).ToString();
            numbersIn = oPCUA_Interface.ReadNodeValue(nodeIn).ToString();

            textNodeOut.text = numberGuide2 + oPCUA_Interface.ReadNodeValue(nodeOut).ToString();// Displayed in the text mesh pro
            numbersOut = oPCUA_Interface.ReadNodeValue(nodeOut).ToString();
        }

    }
    private void OnConnected()
    {
        //Debug.Log("Connected");
       //var subscription = oPCUA_Interface.Subscribe(nodeID, NodeChanged);
    }

    private void OnDisconnected()
    {
        Debug.Log("Disconnected");
    }

    private void NodeChanged(OPCUANodeSubscription sub, object value)
    {
        Debug.Log("hi");
        textNodeIn.text = value.ToString();
    }
    private void OnDestroy()
    {
        oPCUA_Interface.Disconnect();
    }
}
