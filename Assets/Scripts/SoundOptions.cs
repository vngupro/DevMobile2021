using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider ambiantSlider;
    public Slider sfxSlider;
    public Slider uiSlider;
    public Button buttonMute;
    public Button buttonUnmute;

    private void Awake()
    {
        buttonMute.onClick.AddListener(Unmute);
        buttonUnmute.onClick.AddListener(Mute);
        buttonMute.gameObject.SetActive(false);
        buttonUnmute.gameObject.SetActive(true);

        masterSlider.onValueChanged.AddListener(SetVolumeMaster);
        musicSlider.onValueChanged.AddListener(SetVolumeMusic);
        ambiantSlider.onValueChanged.AddListener(SetVolumeAmbient);
        sfxSlider.onValueChanged.AddListener(SetVolumeSFX);
        uiSlider.onValueChanged.AddListener(SetVolumeUI);
    }

    // SET VOLUME 
    public void SetVolumeMaster(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.SetMasterVolume(value);
    }
    public void SetVolumeMusic(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.SetMusicVolume(value);
    }
    public void SetVolumeAmbient(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.SetAmbientVolume(value);
    }
    public void SetVolumeSFX(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.SetSFXVolume(value);
    }
    public void SetVolumeUI(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.SetUIVolume(value);
    }

    // SET MUTE
    private void Mute()
    {
        Debug.Log("Mute");
        buttonMute.gameObject.SetActive(true);
        buttonUnmute.gameObject.SetActive(false);
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.ChangeMute(true);
    }

    private void Unmute()
    {
        Debug.Log("Unmute");
        buttonMute.gameObject.SetActive(false);
        buttonUnmute.gameObject.SetActive(true);
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.ChangeMute(false);
    }

    // Load options
    public void LoadSoundsOptions(float master, float music, float sfx, float ui, float ambiant, bool mute)
    {
        if (mute == true) { Mute(); } else { Unmute(); }
        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;
        uiSlider.value = ui;
        ambiantSlider.value = ambiant;
    }
}
