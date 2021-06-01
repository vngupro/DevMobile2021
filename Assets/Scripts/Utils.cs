using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class Utils
{
    public static Vector3 ScreenToWorld(Camera cam, Vector2 position)
    {
        Vector3 newPos = cam.ScreenToWorldPoint(position);
        newPos.z = cam.nearClipPlane;
        return newPos;
    }

    public static IEnumerator Fade(float alpha, float duration, float min, float max, bool isFadingOut, System.Action<float> callback)
    {
        if (isFadingOut)
        {
            UtilsEvent.fadeOutStarted.Invoke();
        }
        else{
            UtilsEvent.fadeInStarted.Invoke();
        }

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;
            alpha = Mathf.Lerp(min, max, ratio);
            callback(alpha);
            yield return null;
        }

        if (isFadingOut)
        {
            UtilsEvent.fadeOutEnded.Invoke();
        }
        else
        {
            UtilsEvent.fadeInEnded.Invoke();
        }
    }
}

public static class UtilsEvent
{
    public static UnityEvent startFadeIn = new UnityEvent();
    public static UnityEvent fadeInStarted = new UnityEvent();
    public static UnityEvent fadeInEnded = new UnityEvent();
    public static UnityEvent startFadeOut = new UnityEvent();
    public static UnityEvent fadeOutStarted = new UnityEvent();
    public static UnityEvent fadeOutEnded = new UnityEvent();

    public static UnityEvent blockMoveControls = new UnityEvent();
    public static UnityEvent unlockMoveControls = new UnityEvent();
}

