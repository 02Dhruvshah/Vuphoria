using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneFESTO : MonoBehaviour
{

    public string sceneName;
    public void OnClickEvent()
    {

        SceneManager.LoadScene(sceneName);

    }


    
}
