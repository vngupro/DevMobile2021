using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CanvasBlackscreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    [Header("Animation")]
    public float fadeDuration = 2.0f;

    private InputManager inputManager;
    public static CanvasBlackscreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        inputManager = InputManager.Instance;

        // Listen To
        UtilsEvent.startFadeIn.AddListener(FadeIn);
        UtilsEvent.startFadeOut.AddListener(FadeOut);
        UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
        UtilsEvent.fadeOutEnded.AddListener(EndFadeOut);
    }

    public void FadeOut()
    {

        BlockInput();
        StartCoroutine(Utils.Fade(canvasGroup.alpha, fadeDuration, 1f, 0f, true,
            returnValue => {
                canvasGroup.alpha = returnValue;
            }));
    }

    public void FadeIn()
    {
        BlockInput();
        StartCoroutine(Utils.Fade(canvasGroup.alpha, fadeDuration, 0f, 1f, false,
            returnValue => {
                canvasGroup.alpha = returnValue;
            }));
    }

    private void EndFadeOut()
    {
        canvasGroup.blocksRaycasts = false;
        inputManager.EnableControls();
    }

    private void BlockInput()
    {
        inputManager.DisableControls();
        canvasGroup.blocksRaycasts = true;
    }
}

// Video Player (overlay) > Blackscreen (overlay) > Canvas (camera mode with Main Camera) 