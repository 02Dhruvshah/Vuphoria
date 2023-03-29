using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game4automation;
using TMPro;
using System;

public class AnimationTriggerSquare : MonoBehaviour
{
    public OPCUA_Interface[] oPCUA_Interface;
    public OPCUA_Node[] node;
    public OPCUA_Node[] IsItThere;

    //public List<OPCUA_Interface> oPCUA_Interface;
    //public OPCUA_Interface[] OPCUA_Interfaces;
    //public List<MonoBehaviour> Node;
    //public List<MonoBehaviour> IsItThere;

    //public Animator FactoryRailsPartTry1;
    public string carrierNumberRead;
    public GameObject lookForCarrier;
    public string[] animationNameInput;

    public string nodeID;

    public string carrierNumberFind;


    //public string isItThere;
    private float timer;
    private float pollTime;
    void Start()
    {
        // Debug.Log(lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead));
        // pollTime = Random.Range(1f, 2f);
    }

    private void Update()
    {

        // Debug.Log(oPCUA_Interface.ReadNodeValue(IsItThere).ToString());

        //isItThere = oPCUA_Interface[0].ReadNodeValue(IsItThere[0]).ToString();


        for (int i = 0; i < 5; i++)
        {
            ////Debug.Log(oPCUA_Interface[i].ReadNodeValue(IsItThere[i]).ToString());

            if (oPCUA_Interface[i].ReadNodeValue(IsItThere[i]).ToString() == "True") //carrier in sensor?
            {
                //Debug.Log( i + "  "+ oPCUA_Interface[i].ReadNodeValue(node[i]).ToString() + " it works");

                carrierNumberRead = oPCUA_Interface[i].ReadNodeValue(node[i]).ToString(); // Check number in sensor

                    lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead);//Look for carrier (n) When using Vfuoria AreaTarget/Carriers/
                    Animator theAnimations = lookForCarrier.GetComponent<Animator>();
                    if (theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[i]) == false)// Check if animation already playing 
                    {
                        lookForCarrier.GetComponent<Animator>().Play(animationNameInput[i]); // Get animator from Carrier & Play Animation of specific sensor
                        Debug.Log(carrierNumberRead + " it works");
                    }
                
               
                //        //Debug.Log("Carrier is true " + i);

                //        //    /*if(carrierNumberRead == carrierNumberInput)
                //        //    {
                //        //    Debug.Log("animation 1 triggreed");
                //        //     FactoryRailsPartTry1.Play(animationNameInput);
                //        //    }*/

                //    }

                //}


                //    //if (timer < pollTime)
                //    //{
                //    //    timer += Time.deltaTime;
                //    //}
                //    //else
                //    //{
                //    //    timer = 0;
                //    //    pollTime = 0.001f;//UnityEngine.Random.Range(0.001f, 0.002f);
                //    //                       //text.text = oPCUA_Interface.ReadNodeValue(node).ToString(); //allows the value to update as the carriers pass and gives real time value
                //    //                       //number = oPCUA_Interface.ReadNodeValue(node).ToString();
                //    //                       //isItThere = oPCUA_Interface.ReadNodeValue(IsItThere).ToString();
                //    //                       //Debug.Log(text.text);
                //    //    for (int i = 0; i < 5; i++)
                //    //    {
                //    //        //Debug.Log(oPCUA_Interface[i].ReadNodeValue(IsItThere[i]).ToString());

                //    //        if (oPCUA_Interface[i].ReadNodeValue(IsItThere[i]).ToString() == "True") //carrier in sensor?
                //    //        {

                //}

                //if (oPCUA_Interface[i].Subscribe(nodeID, NodeChanged).ToString() == "True") //carrier in sensor?
                //{
                //    //Debug.Log( i + "  "+ oPCUA_Interface[i].ReadNodeValue(node[i]).ToString() + " it works");

                //    carrierNumberRead = oPCUA_Interface[i].ReadNodeValue(node[i]).ToString(); // Check number in sensor
                //    lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead);//Look for carrier (n) When using Vfuoria AreaTarget/Carriers/
                //    Animator theAnimations = lookForCarrier.GetComponent<Animator>();
                //    if (theAnimations.GetCurrentAnimatorStateInfo(0).IsName(animationNameInput[i]) == false)// Check if animation already playing 
                //    {
                //        lookForCarrier.GetComponent<Animator>().Play(animationNameInput[i]); // Get animator from Carrier & Play Animation of specific sensor
                //        Debug.Log(carrierNumberRead + " it works");
                //    }

                //}

                //void NodeChanged(OPCUANodeSubscription sub, object value)
                //{
                //    Debug.Log("hi " + value.ToString());
                //    Debug.Log("Carriers/" + value.ToString());
                //    lookForCarrier = GameObject.Find("Carriers/" + value.ToString());
                //    lookForCarrier.GetComponent<Animator>().Play();
                //    Debug.Log("Found and play");
                //}
            }
        }
            

    }
}
