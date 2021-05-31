using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class CanvasExitDoorScript : MonoBehaviour
{
    public GameObject background;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    public CinemachineVirtualCamera vcam;
    [HideInInspector]
    public string sceneToLoad;
    public bool isExiting = false;
    private CanvasBlackscreen blackscreen;
    public GameObject canvasSuspect;
    
    private void Start()
    {
        blackscreen = CanvasBlackscreen.Instance;
        buttonNo.onClick.AddListener(NoGoBack);
        buttonYes.onClick.AddListener(YesChangeScene);
        background.SetActive(false);
        canvasSuspect.SetActive(false);
    }

    public void YesChangeScene()
    {
        Debug.Log("Go To Suspect Scene");
        isExiting = true;

        if(blackscreen != null)
        {
            StartCoroutine(SwitchToSuspect());
        }
        else
        {
            CustomGameEvents.switchToSuspect.Invoke(vcam);
        }
        //LevelManager.Instance.OpenSceneByName(sceneToLoad);
    }

    public void NoGoBack()
    {
       background.SetActive(false);
    }

    IEnumerator SwitchToSuspect()
    {
        blackscreen.FadeIn();
        yield return new WaitForSeconds(blackscreen.fadeDuration);
        CustomGameEvents.switchToSuspect.Invoke(vcam);
        yield return new WaitForSeconds(0);
        background.SetActive(false);
        canvasSuspect.SetActive(true);
        blackscreen.FadeOut();
    }
}