using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;


public class OrderButton : MonoBehaviour
{
    public TextMeshProUGUI PNo;
    public TextMeshProUGUI DescriptionText;

    public TextMeshProUGUI CurentOrdersText;

    public Image OrderImage;
    public int productNumber;

    
    public OrderOptions orderOptions;

    private void Start()
    {
        orderOptions = GameObject.FindObjectOfType<OrderOptions>();
    }

    public void ImageDownloader(string url)
    {
        StartCoroutine(setImage(url)); //balanced parens CAS
    }

    public void ProductOrder()
    {
        orderOptions.OrderPNoOnClick(productNumber);
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
                    OrderImage.sprite = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0, 0));
                    break;
            }
        }
    }
}