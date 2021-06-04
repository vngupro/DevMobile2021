using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptionScript : MonoBehaviour
{
    public void SetVolumeMaster(float value)
    {
        SoundManager.Instance.SetMasterVolume(value);
    }

    public void SetVolumeMusic(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }

    public void SetVolumeAmbient(float value)
    {
        SoundManager.Instance.SetAmbientVolume(value);
    }

    public void SetVolumeSFX(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }

    public void SetVolumeUI(float value)
    {
        SoundManager.Instance.SetUIVolume(value);
    }
}
