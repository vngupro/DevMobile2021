using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Cinemachine;

/* 
 * Cinematic = Screen Fade In > Video Start Play > Video Stop > Screen Fade Out
 */

public class VideoPlayerScript : MonoBehaviour
{
    #region Variables
    public VideoPlayer videoPlayer;
    [Tooltip("Canvas Raw Image with target render texture use in video player")]
    public GameObject videoScreen;
    public float secondsBeforeStartCinematic = 10.0f;
    [Tooltip("Fade of the video (separate from the blackscreen fade)")]
    public float fadeDuration = 2.0f;

    [Header("Debug")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CinemachineBlackscreen cameraBlackscreen;
    [SerializeField] private bool hasCameraBlackscreen = false;
    [SerializeField] private CanvasBlackscreen canvasBlackscreen;
    [SerializeField] private bool hasCanvasBlackscreen = false;
    [SerializeField] private float timer;
    [SerializeField] private bool isTimerOn = true;                         //Stop timer during cinematic playing
    [SerializeField] private bool isWaitingEndOfVideo = false;
    [SerializeField] private bool hasEnterMenu = false;
    [SerializeField] private bool isInterrupting = false;
    #endregion
    #region Property
    public static VideoPlayerScript Instance { get; protected set; }
    #endregion
    #region Initialization
    private void Awake()
    {
        if(Instance != null && Instance != this) { Destroy(this.gameObject); }
        Instance = this;

        InitBlackscreen();
        InitVideoPlayer();
        InitVideoScreen();

        timer = secondsBeforeStartCinematic;
        isTimerOn = true;
        isWaitingEndOfVideo = false;
        hasEnterMenu = false;

        // Listen To
        // Utils.cs
        UtilsEvent.fadeInEnded.AddListener(PlayVideo);
        UtilsEvent.fadeOutEnded.AddListener(RestartTimer);
        // MenuManager.cs
        CustomGameEvents.hasTapScreen.AddListener(InterruptVideo);
        CustomGameEvents.enteredMenu.AddListener(ChangeHasEnterMenu);

    }

    private void InitBlackscreen()
    {
        cameraBlackscreen = CinemachineBlackscreen.Instance;
        hasCameraBlackscreen = (cameraBlackscreen != null) ? true : false;

        canvasBlackscreen = CanvasBlackscreen.Instance;
        hasCanvasBlackscreen = (canvasBlackscreen != null) ? true : false;
    }
    private void InitVideoPlayer()
    {
        if (videoPlayer == null) videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetTexture.Release();
    }
    private void InitVideoScreen()
    {
        if (videoScreen == null) videoScreen = GameObject.Find("VideoScreen");
        canvasGroup = videoScreen.GetComponent<CanvasGroup>();
        videoScreen.SetActive(false);
    }

    #endregion
    private void Update()
    {
        // Stop everything if player has enter menu
        if (hasEnterMenu) { return; }

        // Start Timer
        if (isTimerOn) { timer -= Time.deltaTime; }

        // Start Cinematic
        if (timer <= 0f) { VideoScreenOn(); }

        // Prepare to Interrupt Video
        if (!isWaitingEndOfVideo && videoPlayer.isPlaying)
        {
            videoPlayer.loopPointReached += ResetVideo;
            isWaitingEndOfVideo = true;
        }
    }
    #region Functions
    private void VideoScreenOn()
    {
        isTimerOn = false;
        timer = secondsBeforeStartCinematic;

        if (hasCameraBlackscreen || hasCanvasBlackscreen) {
            UtilsEvent.startFadeIn.Invoke();
        }else { 
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        if (hasEnterMenu) return;
        videoScreen.SetActive(true);
        videoPlayer.Play();
        SoundManager.Instance.StopSoundWithFade("Background");
    }

    private void ResetVideo(VideoPlayer videoPlayer)
    {
        Debug.Log("Reset Video");
        videoPlayer.loopPointReached -= ResetVideo;
        videoPlayer.Stop();
        videoPlayer.frame = 0;
        videoPlayer.targetTexture.Release();
        videoPlayer.SetDirectAudioVolume(0, 1);
        canvasGroup.alpha = 1;
        videoScreen.SetActive(false);
        isWaitingEndOfVideo = false;

        if (hasCameraBlackscreen || hasCanvasBlackscreen)
        {
            UtilsEvent.startFadeOut.Invoke();
        } else{
            RestartTimer();
        }
    }

    private void RestartTimer()
    {
        if (hasEnterMenu) return;
        isTimerOn = true;
        isInterrupting = false;
        SoundManager.Instance.PlaySound("Background");
    }

    IEnumerator StopVideo()
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

        ResetVideo(videoPlayer);
    }
    private void InterruptVideo()
    {
        if (isInterrupting) return;
        isInterrupting = true;
        StartCoroutine(StopVideo());
    }

    private void ChangeHasEnterMenu()
    {
        if (isWaitingEndOfVideo)
        {
            InterruptVideo();
        }
        else
        {
            hasEnterMenu = true;
            isTimerOn = false;
        }

        timer = secondsBeforeStartCinematic;
    }

    public bool IsPlaying()
    {
        return videoPlayer.isPlaying;
    }


    #endregion
}
