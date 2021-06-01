using UnityEngine;
using Cinemachine;

[DefaultExecutionOrder(-1)]
public class CinemachineBlackscreen : MonoBehaviour
{
    [SerializeField] private CinemachineStoryboard storyboard;

    [Header("Animation")]
    public float fadeDuration = 2.0f;

    private InputManager inputManager;
    public static CinemachineBlackscreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); }

        Instance = this;

        inputManager = InputManager.Instance;

        // Listen To
        UtilsEvent.startFadeIn.AddListener(FadeIn);
        UtilsEvent.startFadeOut.AddListener(FadeOut);
        UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
        UtilsEvent.fadeOutEnded.AddListener(inputManager.EnableControls);
    }

    public void FadeOut()
    {
        inputManager.DisableControls();
        StartCoroutine(Utils.Fade(storyboard.m_Alpha, fadeDuration, 1f, 0f, true, 
            returnValue => { 
                storyboard.m_Alpha = returnValue;
            }));
    }

    public void FadeIn()
    {
        inputManager.DisableControls();
        StartCoroutine(Utils.Fade(storyboard.m_Alpha, fadeDuration, 0f, 1f, false,
            returnValue => {
                storyboard.m_Alpha = returnValue;
            }));
    }
}
