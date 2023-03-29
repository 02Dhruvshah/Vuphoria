using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game4automation;
using System;

public class ANimationTriggerSubscribeIndividual : MonoBehaviour
{
    public OPCUA_Interface oPCUA_Interface;
    public OPCUA_Node node;
    public OPCUA_Node IsItThere;

    public string carrierNumberRead;
    public GameObject lookForCarrier;
    public string animationNameInput;



    //private void Start()
    //    {
    //      OPCUA_Node oPCUA_Node = GetComponent<OPCUA_Node>();

    //      string smth = oPCUA_Node.Value;
    //    }

    private void Update()
    {
            if (oPCUA_Interface.ReadNodeValue(IsItThere).ToString() == "True") //carrier in sensor? By READVALUE
            {
                //node = GetComponent<OPCUA_Node>();
                carrierNumberRead = node.Value; // Look for carrier number in SUBSCRIBE

                lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead);//Look for carrier (n) When using Vfuoria AreaTarget/Carriers/

             if(carrierNumberRead =="1"|| carrierNumberRead == "2" || carrierNumberRead == "3" || carrierNumberRead == "4" || carrierNumberRead == "5" || carrierNumberRead == "6")
             {
                Animator theAnimations = lookForCarrier.GetComponent<Animator>();
                if (theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput) == false)// Check if animation already playing 
                {
                    lookForCarrier.GetComponent<Animator>().Play(animationNameInput); // Get animator from Carrier & Play Animation of specific sensor
                    Debug.Log(carrierNumberRead + " it works");

                }
            }
            else
            {

            }
                

            }



        
    }
}
