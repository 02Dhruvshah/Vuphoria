using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game4automation;
using TMPro;

public class AnimationTriggerIndividual : MonoBehaviour
{
    public OPCUA_Interface oPCUA_Interface;
    public OPCUA_Node node;
    public OPCUA_Node IsItThere;

    //public List<OPCUA_Interface> oPCUA_Interface;
    //public OPCUA_Interface[] OPCUA_Interfaces;
    //public List<MonoBehaviour> Node;
    //public List<MonoBehaviour> IsItThere;

    //public Animator FactoryRailsPartTry1;
    private string carrierNumberRead;
    private GameObject lookForCarrier;
    public string animationNameInput;


    public string isItThere;
    //private float timer;
    //private float pollTime;
    void Start()
    {
        // Debug.Log(lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead));
        // pollTime = Random.Range(1f, 2f);
    }

    private void Update()
    {
        // Debug.Log(oPCUA_Interface.ReadNodeValue(IsItThere).ToString());


            //Debug.Log(oPCUA_Interface[i].ReadNodeValue(IsItThere[i]).ToString());

            if (oPCUA_Interface.ReadNodeValue(IsItThere).ToString() == "True") //carrier in sensor?
            {
                //Debug.Log("Carrier is true " + i);
                carrierNumberRead = oPCUA_Interface.ReadNodeValue(node).ToString(); // Check number in sensor
                lookForCarrier = GameObject.Find("Carriers/" + carrierNumberRead);//Look for carrier (n)
                lookForCarrier.GetComponent<Animator>().Play(animationNameInput);// Get animator from Carrier & Play Animation of specific sensor
                //Debug.Log(carrierNumberRead + " it works");


                //    /*if(carrierNumberRead == carrierNumberInput)
                //    {
                //    Debug.Log("animation 1 triggreed");
                //     FactoryRailsPartTry1.Play(animationNameInput);
                //    }*/


            }

        //isItThere = oPCUA_Interface[0].ReadNodeValue(IsItThere[0]).ToString();


        //if(timer < pollTime)
        //{
        //    timer += Time.deltaTime;
        //}
        //else
        //{
        //    timer = 0;
        //    pollTime = 1f;// Updates every second 
        //    carrierNumberRead = oPCUA_Interface.ReadNodeValue(node).ToString();// Update the Number of carrier value
        //    isItThere = oPCUA_Interface.ReadNodeValue(IsItThere).ToString();
        //}
    }

}

