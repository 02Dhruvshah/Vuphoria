using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game4automation;
using System;

public class ReadSubscribe : MonoBehaviour
{
    public OPCUA_Interface oPCUA_Interface;

    public string nodeID;
    public string animationName;
    public GameObject lookForCarrier;

    public string numberValue;

    // Start is called before the first frame update
    void Start()
    {
        oPCUA_Interface.EventOnConnected.AddListener(OnConnected);
        oPCUA_Interface.EventOnDisconnected.AddListener(OnDisconnected);

    }

    private void OnReconnecting()
    {
        Debug.Log("Reconnecting");
    }

    private void OnConnected()
    {
        Debug.Log("Connected");
        var subscription = oPCUA_Interface.Subscribe(nodeID, NodeChanged);
    }

    private void OnDisconnected()
    {
        Debug.Log("Disconnected");
    }
    public void NodeChanged(OPCUANodeSubscription sub, object value)
    {
        Debug.Log("hi " + value.ToString());
        Debug.Log("Carriers/" + value.ToString());
        numberValue = value.ToString();
        lookForCarrier = GameObject.Find("Carriers/" + numberValue);
        lookForCarrier.GetComponent<Animator>().Play(animationName);
        Debug.Log("Found and play");
    }

    private void OnDestroy()
    {
        oPCUA_Interface.EventOnConnected.RemoveAllListeners();

    }

}