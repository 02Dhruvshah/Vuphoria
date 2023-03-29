using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class FinishedOrders : MonoBehaviour
{
 public string finishedOrdersURL = "http://172.21.0.90/SQLData.php?Command=finishedOrders";
    public FinishedOrderJSON[] currentOrderObjectArray;
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
       if (listOfOrderOptionsPanel.transform.childCount < 1)
       {
            if (Test)
            {
                ConvertOrderDataToArray(jSOnTestData);
                Debug.Log("This module is testing don't test live");
            }
            else
            {
                StartCoroutine(GetRequest(finishedOrdersURL));
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
        currentOrderObjectArray = JsonHelper.FromJson<FinishedOrderJSON>(severCodeFixOrderStringPHPMany);
       // Debug.Log("Converting FinONo:" + currentOrderObjectArray[0].FinONo + ", Company:" + currentOrderObjectArray[0].Company + ", Start:" + currentOrderObjectArray[0].Start + ", End:" + currentOrderObjectArray[0].End + ", State:" + currentOrderObjectArray[0].State);
       // for (int i = 0; i < currentOrderObjectArray.Length; i++)
       // {
        //   Debug.Log("FinONo:" + currentOrderObjectArray[i].FinONo + ", Company:" + currentOrderObjectArray[i].Company + ", Start:" + currentOrderObjectArray[i].Start + ", End:" + currentOrderObjectArray[i].End + ", State:" + currentOrderObjectArray[i].State);
        //}

        UpdateOrderData();

    }
    public void UpdateOrderData()
    {
        Debug.Log("updatingOrders : " + currentOrderObjectArray.Length);
        for (int i = 0; i < currentOrderObjectArray.Length; i++)
        {
            Debug.Log("FinONo:" + currentOrderObjectArray[i].FinONo + ", Company:" + currentOrderObjectArray[i].Company +", Start:" + currentOrderObjectArray[i].Start + ", End:" + currentOrderObjectArray[i].End + ", State:" + currentOrderObjectArray[i].State);
            GameObject InstantiatedOrderButton = Instantiate(orderButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            InstantiatedOrderButton.transform.SetParent(listOfOrderOptionsPanel.transform, false);
            FinishedOrdersDisplay OrderButtonScript = InstantiatedOrderButton.GetComponent(typeof(FinishedOrdersDisplay)) as FinishedOrdersDisplay;
            OrderButtonScript.FinONo.text =  "FinONo: " + currentOrderObjectArray[i  ].FinONo.ToString();
            OrderButtonScript.Company.text =  "Company: " + currentOrderObjectArray[i].Company;
            OrderButtonScript.Start.text = "Start: " + currentOrderObjectArray[i].Start;
            OrderButtonScript.End.text ="End: " + currentOrderObjectArray[i].End;
            OrderButtonScript.State.text = "State: " + currentOrderObjectArray[i].State;
            //OrderButtonScript.CurentOrdersText.text = "ONo:" + currentOrderObjectArray[i].ONo.ToString() + ", Company:" + currentOrderObjectArray[i].Company + ", PlannedStart:" + currentOrderObjectArray[i].PlannedStart + ", PlannedEnd:" + currentOrderObjectArray[i].PlannedEnd;
        }
    }
}
