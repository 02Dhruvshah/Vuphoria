using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class CurrentOrderDisplay : MonoBehaviour
{
    public TextMeshProUGUI ONo;
    public TextMeshProUGUI Company;

    public TextMeshProUGUI PlannedStart;
    public TextMeshProUGUI PlannedEnd;
    //public TextMeshProUGUI CurentOrdersText;
    public TextMeshProUGUI State;  


    public void ImageDownloader(string url)
    {
        StartCoroutine(setImage(url)); //balanced parens CAS
    }

    IEnumerator setImage(string uri)
    {
        /// https://docs.unity3d.com/Manual/UnityWebRequest-RetrievingTexture.html

        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    Texture myTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                    //https://answers.unity.com/questions/1175862/loading-a-sprite-from-url-c.html
                    break;
            }
        }
    }
}
