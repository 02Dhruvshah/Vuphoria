using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class DataBaseName : MonoBehaviour
{
    public string dataBaseURL = "http://172.21.0.90/SQLData.php?Command=cart";// URL of the database we are extracting the information from
    public DataBaseNameJSON[] currentOrderObjectArray; // Attach first script: "DataBaseNameJSON"
    public GameObject orderButtonPrefab;// The prefab for each information tab 
    public GameObject listOfOrderOptionsPanel;// Parent of all prefabs 
    public bool Test;//if TRUE --> Data used from jSOnTestData   ||    if FALSE --> Data used from dataBaseURL
    public string jSOnTestData;// Where we will input test data

    public void Start()
    {
        OnClickUpdateButton();
    }

    public void OnClickUpdateButton()
    {
        if (listOfOrderOptionsPanel.transform.childCount < 1)
        {
            if (Test)  //Data used from jSOnTestData
            {
                ConvertOrderDataToArray(jSOnTestData);
            }
            else      // Data used from dataBaseURL
            {
                StartCoroutine(GetRequest(dataBaseURL));
            }
        }
    }

    IEnumerator GetRequest(string uri)//For more information: https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
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

    string fixJson(string value)// To Read the data we are extracting from the URL correctly
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public void ConvertOrderDataToArray(string OrderStringPHPMany)
    {
        string severCodeFixOrderStringPHPMany = fixJson(OrderStringPHPMany);
        currentOrderObjectArray = JsonHelper.FromJson<DataBaseNameJSON>(severCodeFixOrderStringPHPMany);
        UpdateOrderData();
    }

    public void UpdateOrderData()
    {
        for (int i = 0; i < currentOrderObjectArray.Length; i++)
        {
            GameObject InstantiatedOrderButton = Instantiate(orderButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity); // Instantiate order prefab
            InstantiatedOrderButton.transform.SetParent(listOfOrderOptionsPanel.transform, false); // List prefab as child of parent 
            DataBaseNameDisplay DataBaseScript = InstantiatedOrderButton.GetComponent(typeof(DataBaseNameDisplay)) as DataBaseNameDisplay;// Get Dislpayers(TextMeshProUGUI variables) from DataBaseNameDisplay.cs
            DataBaseScript.nameOfOneVariable.text = "Name of The Variable: " + currentOrderObjectArray[i].nameOfOneVariable.ToString();// Get what we want to be displayed
            DataBaseScript.nameOfAnotherVariable.text = "Name of Another Variable: " + currentOrderObjectArray[i].nameOfAnotherVariable;
        }
    }
}
