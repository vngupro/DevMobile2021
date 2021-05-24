/* if using with button
 * put black screen at end of canvas to block button input
 * good for anti bug where black screen is call to many time
 */

using System.Collections;
using UnityEngine;
public class BlackScreenScript : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    CanvasGroup canvasGroup;

    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private bool hasFinishedFadingOut = false;
    private InputManager inputManager;
    private void Awake()
    {
        inputManager = InputManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        // | Listeners
        // VideoPlayerScripts.cs
        CustomGameEvents.activateFadeIn.AddListener(FadeIn);
        CustomGameEvents.activateFadeOut.AddListener(FadeOut);
        CustomGameEvents.cinematicStart.AddListener(FadeIn);
        CustomGameEvents.cinematicFinished.AddListener(FadeOut);
    }

    private void Start()
    {
        // VideoPlayerScript.cs verify if black screen is active in awake
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isFadingIn)
        {
            //When fadeIn is finished
            StartVideo();
        }
        else if (hasFinishedFadingOut)
        {
            hasFinishedFadingOut = false;
            // | Invoke
            // VideoPlayerScripts.cs
            CustomGameEvents.fadeOutFinished.Invoke();
            gameObject.SetActive(false);
        }
    }

    //Use with button
    public void StartCinematic()
    {
        Debug.Log("Cinematic Start");
        FadeIn();
    }
    public void FadeIn()
    {
        Debug.Log("Fade In");
        gameObject.SetActive(true);
        StartCoroutine(Fade(true));
    }

    public void FadeOut()
    {
        Debug.Log("Fade Out");
        StartCoroutine(Fade(false));
    }

    private void StartVideo()
    {
        //Play video
        if (canvasGroup.alpha >= 1f)
        {
            // | Invoke
            // VideoPlayerScript.cs
            CustomGameEvents.fadeInFinished.Invoke();
        }
    }

    IEnumerator Fade(bool isFadeIn)
    {
        inputManager.DisableControls();
        float timer = 0f;
        float min = 0f;
        float max = 1f;

        //Swap min max if fade out
        if (!isFadeIn)
        {
            float temp = min;
            min = max;
            max = temp;

            isFadingOut = true;

        }
        else
        {
            isFadingIn = true;
        }

        //Fade
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(min, max, ratio);
            yield return null;
        }

        //Finish Fading out
        if (isFadingOut)
        {
            hasFinishedFadingOut = true;
        }

        isFadingIn = false;
        isFadingOut = false;
        inputManager.EnableControls();
    }
}