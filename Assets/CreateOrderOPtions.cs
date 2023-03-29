using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
public class CreateOrderOPtions : MonoBehaviour
{
    public string exampleURL = "http://172.21.0.90/SQLData.php?Command=ordersOptions";
    public OrderOptionJson[] orderOptionObjectArray;
    public GameObject orderButtonPrefab;
    public GameObject listOfOrderOptionsPanel;
    public string ImageIPAddress = "http://172.21.0.90/I4.0/mes4/EN/";
    public bool Test;
    public string jSOnTestData;
    public TCP_Message tCP;
    public void RequestOrderOptions()
    {
        if (listOfOrderOptionsPanel.transform.childCount < 1)
        {
            if (Test)
            {
                ConvertOrderDataToArray(jSOnTestData);
                Debug.LogError("This module is testing don't test live");
            }
            else
            {
                StartCoroutine(GetRequest(exampleURL));
            }
        }
    }


    // Web resquest is coming fromt he uri, 
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
        orderOptionObjectArray = JsonHelper.FromJson<OrderOptionJson>(severCodeFixOrderStringPHPMany);
        Debug.Log("Converting PNo:" + orderOptionObjectArray[0].PNo + ", Description:" + orderOptionObjectArray[0].Description);
        for (int i = 0; i < orderOptionObjectArray.Length; i++)
        {
            Debug.Log("PNo:" + orderOptionObjectArray[i].PNo + ", Description:" + orderOptionObjectArray[i].Description);
        }

        UpdateOrderData();

    }
    // This script will simply instantiate the Prefab when the game starts.
    public void UpdateOrderData()
    {
        Debug.Log("updatingOrders : " + orderOptionObjectArray.Length);
        for (int i = 0; i < orderOptionObjectArray.Length; i++)
        {
            Debug.Log("PNo:" + orderOptionObjectArray[i].PNo + ", picture:" + orderOptionObjectArray[i].Picture);
            GameObject InstantiatedOrderButton = Instantiate(orderButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            InstantiatedOrderButton.transform.SetParent(listOfOrderOptionsPanel.transform, false);
            OrderButton OrderButtonScript = InstantiatedOrderButton.GetComponent(typeof(OrderButton)) as OrderButton;
            OrderButtonScript.PNo.text = orderOptionObjectArray[i].PNo.ToString();
            OrderButtonScript.productNumber = orderOptionObjectArray[i].PNo;
            /* Button button = InstantiatedOrderButton.GetComponent<Button>() as Button ;
              int temp = orderOptionObjectArray[i].PNo;
              button.onClick.AddListener(()=> OrderPNoOnClick(temp)); */
            if (Test)
            {

            }
            else
            {
                OrderButtonScript.ImageDownloader(ImageIPAddress + orderOptionObjectArray[i].Picture);
            }



        }

        // Instantiate at position (0, 0, 0) and zero rotation.
        //Instantiate(orderButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void OrderPNoOnClick(int PNo)
    {
        tCP.CustomOrder(PNo);
        CloseOrderOptions();
    }

    private void CloseOrderOptions()
    {
        foreach (Transform child in listOfOrderOptionsPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
