using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScreenScript : MonoBehaviour
{
    private VideoPlayerScript video;
    private InputManager inputManager;
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += CloseTapScreen;
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        video = VideoPlayerScript.Instance;
    }

    private void CloseTapScreen(Vector2 position, float time)
    {
        if (video.IsPlaying())
        {
            CustomGameEvents.hasTapScreen.Invoke();
        }
        else
        {
            this.gameObject.SetActive(false);
            inputManager.OnStartTouch -= CloseTapScreen;
            CustomGameEvents.enterMenu.Invoke();
        }
    }
}
