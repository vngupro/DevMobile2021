using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayOptions : MonoBehaviour
{
    public Slider panSlider;
    public Slider zoomSlider;
    public Button buttonVibration;
    public Button buttonNoVibration;

    private void Awake()
    {
        panSlider.onValueChanged.AddListener(SetPan);
        zoomSlider.onValueChanged.AddListener(SetZoom);
        buttonVibration.onClick.AddListener(NoVibration);
        buttonNoVibration.onClick.AddListener(Vibration);

        buttonNoVibration.gameObject.SetActive(true);
        buttonVibration.gameObject.SetActive(false);
    }
    public void SetPan(float value)
    {
        SlideOneFingerDetection.Instance.ChangeDistanceTolerance(value, zoomSlider.maxValue);
    }
    public void SetZoom(float value)
    {
        PinchDetection.Instance.ChangeZoomSpeed(value, panSlider.maxValue);
    }
    public void NoVibration()
    {
        buttonVibration.gameObject.SetActive(false);
        buttonNoVibration.gameObject.SetActive(true);
        OptionsManager.Instance.vibrateValue = false;
    }
    public void Vibration()
    {
        buttonVibration.gameObject.SetActive(true);
        buttonNoVibration.gameObject.SetActive(false);
        OptionsManager.Instance.vibrateValue = true;
    }
    public void LoadGameplayOptions(float pan, float zoom, bool vibrate)
    {
        panSlider.value = pan;
        zoomSlider.value = zoom;

        if (vibrate) { Vibration(); }
        else { NoVibration(); }
    }
}
