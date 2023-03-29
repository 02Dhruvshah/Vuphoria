using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game4automation;
using System;
public class AnimationTriggerSquareSubscription : MonoBehaviour
{
    public OPCUA_Interface[] oPCUA_Interface;
    public OPCUA_Node[] node;
    public OPCUA_Node[] IsItThere;

    public string carrierNumberRead;
    public GameObject lookForCarrier;
    public string[] animationNameInput;

    public string carrierNumberFind;


    //private void Start()
    //    {
    //      OPCUA_Node oPCUA_Node = GetComponent<OPCUA_Node>();

    //      string smth = oPCUA_Node.Value;
    //    }

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (oPCUA_Interface[i].ReadNodeValue(IsItThere[i]).ToString() == "True") //carrier in sensor? By READVALUE
            {
                //node[i] = GetComponent<OPCUA_Node>();
                carrierNumberRead = node[i].Value; // Look for carrier number in SUBSCRIBE

                lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead);//Look for carrier (n) When using Vfuoria AreaTarget/Carriers/
                if (carrierNumberRead == "1" || carrierNumberRead == "2" || carrierNumberRead == "3" || carrierNumberRead == "4" || carrierNumberRead == "5" || carrierNumberRead == "6")
                {

                    Animator theAnimations = lookForCarrier.GetComponent<Animator>();
                    if (theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[0]) == false && theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[1]) == false && theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[2]) == false && theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[3]) == false && theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[4]) == false)// Check if animation already playing 
                    {
                        lookForCarrier.GetComponent<Animator>().Play(animationNameInput[i]); // Get animator from Carrier & Play Animation of specific sensor
                        Debug.Log(carrierNumberRead + " it works");

                    }
                }

            }



        }
    }
}

