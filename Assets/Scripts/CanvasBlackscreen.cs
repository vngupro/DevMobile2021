using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CanvasBlackscreen : MonoBehaviour
{
    public CanvasGroup blackscreen;

    [Header("Animation")]
    public float fadeDuration = 2.0f;

    private InputManager inputManager;
    public static CanvasBlackscreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); }

        Instance = this;


        inputManager = InputManager.Instance;

        if (blackscreen == null) blackscreen = GetComponentInChildren<CanvasGroup>();
        // Listen To
        UtilsEvent.startFadeIn.AddListener(FadeIn);
        UtilsEvent.startFadeOut.AddListener(FadeOut);
        UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
        UtilsEvent.fadeOutEnded.AddListener(EndFadeOut);
    }

    public void FadeOut()
    {

        BlockInput();
        StartCoroutine(Utils.Fade(blackscreen.alpha, fadeDuration, 1f, 0f, true,
            returnValue => {
                blackscreen.alpha = returnValue;
            }));
    }

    public void FadeIn()
    {
        BlockInput();
        StartCoroutine(Utils.Fade(blackscreen.alpha, fadeDuration, 0f, 1f, false,
            returnValue => {
                blackscreen.alpha = returnValue;
            }));
    }

    private void EndFadeOut()
    {
        blackscreen.blocksRaycasts = false;
        inputManager.EnableControls();
    }

    private void BlockInput()
    {
        inputManager.DisableControls();
        blackscreen.blocksRaycasts = true;
    }
}

// Video Player (overlay) > Blackscreen (overlay) > Canvas (camera mode with Main Camera) 