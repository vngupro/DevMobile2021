using System.Collections;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerScript : MonoBehaviour
{
    public bool hasTimer = true;
    public GameObject cinematique;
    public float secondsBeforeStartCinematic = 10.0f;
    public float fadeDuration = 2.0f;

    private VideoPlayer videoPlayer;
    private CanvasGroup canvasGroup;
    private float timer;
    private bool isTimerOn = true;
    private bool isWaitingEndOfVideo = false;
    private bool hasPressAnyButton = false;
    [Header("Debug")]
    [SerializeField] private bool hasBlackScreen = false;
    [SerializeField] private float debugTimer;

    public static VideoPlayerScript Instance {get; protected set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetTexture.Release();
        canvasGroup = cinematique.GetComponent<CanvasGroup>();
        cinematique.SetActive(false);
        isTimerOn = true;
        timer = secondsBeforeStartCinematic;
        hasPressAnyButton = false;

        BlackScreenScript blackScreen = FindObjectOfType<BlackScreenScript>();
        if (blackScreen != null)
        {
            hasBlackScreen = true;
        }

        // | Listeners
        // BlackScreenScript.cs
        CustomGameEvents.fadeInFinished.AddListener(PlayVideo);
        CustomGameEvents.fadeOutFinished.AddListener(RestartTimer);
        // MenuManager.cs
        CustomGameEvents.hasTapScreen.AddListener(ChangeHasPressAnyButton);
    }

    private void Update()
    {

        debugTimer = timer;
        //Stop Timer if player is playing
        if (hasPressAnyButton)
        {
            timer = secondsBeforeStartCinematic;
            return;
        }

        //-----Start Timer-------
        if (hasTimer && isTimerOn)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0f)
        {
            StartVideoCinematic();
        }

        if (videoPlayer.isPlaying)
        {
            if (!isWaitingEndOfVideo)
            {
                FinishVideo();
            }
        }

    }

    private void ChangeHasPressAnyButton()
    {

        if (isWaitingEndOfVideo)
        {
            InteruptCinematic();
        }
        else
        {
            hasPressAnyButton = true;

            // | Invoke
            // MenuManager.cs
            CustomGameEvents.hasNotInteruptVideo.Invoke();
        }
    }

    private void InteruptCinematic()
    {
        StartCoroutine(FadeVideo());
    }

    private void StartVideoCinematic()
    {
        if (hasBlackScreen)
        {
            // | Invoke
            // BlackScreenScripts.cs
            CustomGameEvents.cinematicStart.Invoke();
        }
        else
        {
            PlayVideo();
        }
        isTimerOn = false;
        timer = secondsBeforeStartCinematic;
    }
    public void PlayVideo()
    {
        Debug.Log("Play Video");
        cinematique.SetActive(true);
        videoPlayer.Play();
    }
    private void FinishVideo()
    {
        Debug.Log("Delegate Finish Video");
        isWaitingEndOfVideo = true;
        videoPlayer.loopPointReached += EndReached;
    }
    private void EndReached(VideoPlayer videoPlayer)
    {
        ResetVideo();
    }

    private void ResetVideo()
    {
        Debug.Log("Reset Video");
        videoPlayer.Stop();
        videoPlayer.frame = 0;
        videoPlayer.targetTexture.Release();
        videoPlayer.SetDirectAudioVolume(0, 1);
        canvasGroup.alpha = 1;
        cinematique.SetActive(false);
        isWaitingEndOfVideo = false;

        if (hasBlackScreen)
        {
            CustomGameEvents.cinematicFinished.Invoke();
        }
        else
        {
            RestartTimer();
        }

        videoPlayer.loopPointReached -= EndReached;
    }

    private void RestartTimer()
    {
        isTimerOn = true;
    }

    IEnumerator FadeVideo()
    {
        float timer = 0f;
        float min = 1f;
        float max = 0f;

        //Fade
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(min, max, ratio);
            videoPlayer.SetDirectAudioVolume(0, canvasGroup.alpha);
            yield return null;
        }

        ResetVideo();
    }

    public bool IsPlaying()
    {
        return videoPlayer.isPlaying;
    }
}
