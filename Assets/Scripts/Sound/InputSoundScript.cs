using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSoundScript : MonoBehaviour
{
    [Header("  PlaySound ?")]
    [SerializeField] private bool playTouchSound = true;
    [SerializeField] private bool playPickUpSound = true;
    [SerializeField] private bool playZoomSound = true;
    [SerializeField] private bool playSlideSound = false;
    [SerializeField] private bool playSwitchLocationSound = true;

    [Header ("  Sound")]
    [SerializeField] private string touchSound;
    [SerializeField] private string pickUpSound;
    [SerializeField] private string zoomSound;
    [SerializeField] private string slideSound;
    [SerializeField] private string switchLocationSound;

    private void Start()
    {
        TouchDetection.Instance.OnTouch += SoundTouch;
        PickUpDetection.Instance.OnPickUp += SoundPickUp;
        PinchDetection.Instance.OnPinch += SoundZoom;
        SlideOneFingerDetection.Instance.OnSlide += SoundSlide;
        SwitchLocationDetection.Instance.OnSwitchLocation += SoundSwitchLocation;
    }
    private void OnDisable()
    {
        TouchDetection.Instance.OnTouch -= SoundTouch;
        PickUpDetection.Instance.OnPickUp -= SoundPickUp;
        PinchDetection.Instance.OnPinch -= SoundZoom;
        SlideOneFingerDetection.Instance.OnSlide -= SoundSlide;
        SwitchLocationDetection.Instance.OnSwitchLocation -= SoundSwitchLocation;
    }

    private void SoundTouch()
    {
        if (!playTouchSound) return;
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager on scene"); return; }
        SoundManager.Instance.PlaySound(touchSound);
    }
    private void SoundPickUp()
    {
        if (!playPickUpSound) return;
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager on scene"); return; }
        SoundManager.Instance.PlaySound(pickUpSound);
    }
    private void SoundZoom()
    {
        if (!playZoomSound) return;
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager on scene"); return; }
        Debug.Log("Sound Zoom");
        SoundManager.Instance.PlaySound(zoomSound);
    }
    private void SoundSlide()
    {
        if (!playSlideSound) return;
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager on scene"); return; }
        SoundManager.Instance.PlaySound(slideSound);
    }
    private void SoundSwitchLocation()
    {
        if (!playSwitchLocationSound) return;
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager on scene"); return; }
        SoundManager.Instance.PlaySound(switchLocationSound);
    }
}