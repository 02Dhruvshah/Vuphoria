using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class Cart : MonoBehaviour
{
  public string cartURL = "http://172.21.0.90/SQLData.php?Command=cart";// Here we will put the URL of the database we are extracting the information from
    public CartsJSON[] currentOrderObjectArray; // Here we will attach the first script: "DataBaseNameJSON" that we cretaed 
    public GameObject orderButtonPrefab;
    public GameObject listOfOrderOptionsPanel;
    public string ImageIPAddress = "http://172.21.0.90/I4.0/mes4/EN/";
    public bool Test;
    public string jSOnTestData;
    public TCP_Message tCP;


    public void Start()
    {
        OnClickUpdateButton();
    }

    public void OnClickUpdateButton()
    {
        Debug.Log("it is updating the cart");
       if (listOfOrderOptionsPanel.transform.childCount < 1)
        {
            if (Test) // This will work as 
            {
                ConvertOrderDataToArray(jSOnTestData);
                //StartCoroutine(GetRequest(dataTestURL));
                Debug.Log("This module is testing don't test live");
            }
            else
            {
                StartCoroutine(GetRequest(cartURL));
            }
        }
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    ConvertOrderDataToArray(webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public void ConvertOrderDataToArray(string OrderStringPHPMany)
    {

        string severCodeFixOrderStringPHPMany = fixJson(OrderStringPHPMany);
        currentOrderObjectArray = JsonHelper.FromJson<CartsJSON>(severCodeFixOrderStringPHPMany);
        Debug.Log("Converting CarrierID:" + currentOrderObjectArray[0].CarrierID + ", CarrierTypeID:" + currentOrderObjectArray[0].CarrierTypeID + ", ONo:" + currentOrderObjectArray[0].ONo + ", OPos:" + currentOrderObjectArray[0].OPos + ", PNo:" + currentOrderObjectArray[0].PNo + ", PNoGroup:" + currentOrderObjectArray[0].PNoGroup);
        for (int i = 0; i < currentOrderObjectArray.Length; i++)
        {
            Debug.Log("CarrierID:" + currentOrderObjectArray[i].CarrierID + ", CarrierTypeID:" + currentOrderObjectArray[i].CarrierTypeID + ", ONo:" + currentOrderObjectArray[i].ONo + ", OPos:" + currentOrderObjectArray[i].OPos + ", PNo:" + currentOrderObjectArray[i].PNo + ", PNoGroup:" + currentOrderObjectArray[0].PNoGroup);
        }

        UpdateOrderData();
    }

    public void UpdateOrderData()
    {
        Debug.Log("updatingOrders : " + currentOrderObjectArray.Length);
        for (int i = 0; i < currentOrderObjectArray.Length; i++)
        {
            Debug.Log("CarrierID:" + currentOrderObjectArray[i].CarrierID + ", CarrierTypeID:" + currentOrderObjectArray[i].CarrierTypeID +", ONo:" + currentOrderObjectArray[i].ONo + ", OPos:" + currentOrderObjectArray[i].OPos + ", PNo:" + currentOrderObjectArray[i].PNo + ", PNoGroup:" + currentOrderObjectArray[0].PNoGroup);
            GameObject InstantiatedOrderButton = Instantiate(orderButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            InstantiatedOrderButton.transform.SetParent(listOfOrderOptionsPanel.transform, false);
            CartDisplay OrderButtonScript = InstantiatedOrderButton.GetComponent(typeof(CartDisplay)) as CartDisplay;
            OrderButtonScript.CarrierID.text =  "CarrierID: " + currentOrderObjectArray[i].CarrierID.ToString();
            OrderButtonScript.CarrierTypeID.text =  "CarrierTypeID: " + currentOrderObjectArray[i].CarrierTypeID;
            OrderButtonScript.ONo.text = "ONo: " + currentOrderObjectArray[i].ONo;
            OrderButtonScript.OPos.text ="OPos: " + currentOrderObjectArray[i].OPos;
            OrderButtonScript.PNo.text = "PNo: "+currentOrderObjectArray[i].PNo;
            OrderButtonScript.PNoGroup.text = "PNoGroup: "+currentOrderObjectArray[i].PNoGroup;
            //OrderButtonScript.CurentOrdersText.text = "ONo:" + currentOrderObjectArray[i].ONo.ToString() + ", Company:" + currentOrderObjectArray[i].Company + ", PlannedStart:" + currentOrderObjectArray[i].PlannedStart + ", PlannedEnd:" + currentOrderObjectArray[i].PlannedEnd;
        }
    }


}
