using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class CanvasExitDoorScript : MonoBehaviour
{
    public GameObject background;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    [SerializeField] private CinemachineVirtualCamera vcamSuspect;

    public GameObject canvasSuspect;
    public GameObject groupsLens;
    public GameObject buttonLens;

    [Header("   Sound")]
    [SerializeField] private string yesExitSound;
    [SerializeField] private string noExitSound;

    [Header("   Debug")]
    public bool isExiting = false;
    private CanvasBlackscreen blackscreen;

    
    private void Start()
    {
        blackscreen = CanvasBlackscreen.Instance;

        buttonNo.onClick.AddListener(NoGoBack);
        buttonYes.onClick.AddListener(YesChangeScene);
        buttonNo.onClick.AddListener(SoundNo);
        buttonYes.onClick.AddListener(SoundYes);

        background.SetActive(false);
        canvasSuspect.SetActive(false);
    }

    public void YesChangeScene()
    {
        isExiting = true;
        GameManager.Instance.ExitCrimeScene();

        if(blackscreen != null)
        {
            StartCoroutine(SwitchToSuspect());
        }
        else
        {
            CustomGameEvents.switchToSuspect.Invoke(vcamSuspect);
        }
    }

    public void NoGoBack()
    {
       background.SetActive(false);
    }

    private void SoundNo()
    {
        SoundManager.Instance?.PlaySound(noExitSound);
    }

    private void SoundYes()
    {
        SoundManager.Instance?.PlaySound(yesExitSound);
    }

    IEnumerator SwitchToSuspect()
    {
        blackscreen.FadeIn();
        yield return new WaitForSeconds(blackscreen.fadeDuration);
        CustomGameEvents.switchToSuspect.Invoke(vcamSuspect);
        yield return null;
        background.SetActive(false);
        canvasSuspect.SetActive(true);
        groupsLens.SetActive(false);
        buttonLens.SetActive(false);
        blackscreen.FadeOut();
    }
}
