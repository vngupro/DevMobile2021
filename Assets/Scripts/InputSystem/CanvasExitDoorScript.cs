using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasExitDoorScript : MonoBehaviour
{
    public GameObject background;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    [HideInInspector]
    public string sceneToLoad;

    private void Start()
    {
        buttonNo.onClick.AddListener(NoGoBack);
        buttonYes.onClick.AddListener(YesChangeScene);
        background.SetActive(false);
    }

    public void YesChangeScene()
    {
        LevelManager.Instance.OpenSceneByName(sceneToLoad);
    }

    public void NoGoBack()
    {
       background.SetActive(false);
    }
}
