using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TapScreenScript : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    private VideoPlayerScript video;
    private InputManager inputManager;
    private CanvasGroup canvasGroup;
    private Coroutine coroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        if(inputManager != null)
        inputManager.OnStartTouch += CloseTapScreen;
        coroutine = StartCoroutine(BlinkingText());
    }
    private void Start()
    {
        video = VideoPlayerScript.Instance;
    }

    private void CloseTapScreen(Vector2 position, float time)
    {
        if (video != null)
        {
            if (video.IsPlaying())
            {
                CustomGameEvents.hasTapScreen.Invoke();
                return;
            }
        }
        
        this.gameObject.SetActive(false);
        inputManager.OnStartTouch -= CloseTapScreen;
        CustomGameEvents.enteredMenu.Invoke();
        StopCoroutine(coroutine);
        
    }

    IEnumerator BlinkingText()
    {
        float timer = 0f;
        bool isFadingIn = true;
        float min = 0f, max = 1f;

        //Fade
        while (timer < fadeDuration)
        {
            if (isFadingIn)
            {
                min = 0f;
                max = 1f;
                timer += Time.deltaTime;
                float ratio = timer / fadeDuration;
                canvasGroup.alpha = Mathf.Lerp(min, max, ratio);

                if (canvasGroup.alpha >= 1)
                {
                    isFadingIn = false;
                    timer = 0f;
                }
            }
            else
            {
                min = 1f;
                max = 0f;
                timer += Time.deltaTime;
                float ratio = timer / fadeDuration;
                canvasGroup.alpha = Mathf.Lerp(min, max, ratio);

                if (canvasGroup.alpha <= 0)
                {
                    isFadingIn = true;
                    timer = 0f;
                }
            }
            yield return null;
        }
    }
}
