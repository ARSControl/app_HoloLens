using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;

public class ChangeSceneButton : MonoBehaviour
{
    public async void LoadScene(string sceneName) 
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();

        await sceneSystem.LoadContent(sceneName, LoadSceneMode.Single);
    }   
}
