using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScreenScript : MonoBehaviour
{
    private VideoPlayerScript video;

    private void Start()
    {
        video = VideoPlayerScript.Instance;
    }

    private void CloseTapScreen()
    {
        if (video.IsPlaying())
        {
            CustomGameEvents.hasTapScreen.Invoke();
        }
    }
}
