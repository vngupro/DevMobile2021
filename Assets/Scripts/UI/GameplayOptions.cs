using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayOptions : MonoBehaviour
{
    public  Slider panSlider;
    public  Slider zoomSlider;

    private void Awake()
    {
        panSlider.onValueChanged.AddListener(SetPan);
        zoomSlider.onValueChanged.AddListener(SetZoom);
    }
    public void SetPan(float value)
    {
        SlideOneFingerDetection.Instance.ChangeDistanceTolerance(value, zoomSlider.maxValue);
    }

    public void SetZoom(float value)
    {
        PinchDetection.Instance.ChangeZoomSpeed(value, panSlider.maxValue);
    }

    public void LoadGameplayOptions(float pan, float zoom)
    {
        panSlider.value = pan;
        zoomSlider.value = zoom;
    }
}
