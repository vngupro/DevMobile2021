using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PopUpTest : MonoBehaviour, ISerializationCallbackReceiver
{
    public static List<string> TMPList;
    public List<string> popupList;
    [List2Popup(typeof(PopUpTest), "TMPList")]
    public string currentScene;
    [List2Popup(typeof(PopUpTest), "TMPList")]
    public string nextScene;
    [List2Popup(typeof(PopUpTest), "TMPList")]
    public string previousScene;
    public List<string> GetAllScenesInBuild()
    {
        List<string> allScenes = new List<string>();

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            allScenes.Add(sceneName);
        }

        return allScenes;
    }
    public void OnBeforeSerialize()
    {
        TMPList = popupList;
    }

    public void OnAfterDeserialize()
    {

    }
}
