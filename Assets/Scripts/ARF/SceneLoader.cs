using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private String sceneToLoadName = "1-2.2";
    private void Awake()
    {
        if(!SceneManager.GetSceneByName(sceneToLoadName).IsValid())
            SceneManager.LoadScene(sceneToLoadName, LoadSceneMode.Additive);
    }
}
