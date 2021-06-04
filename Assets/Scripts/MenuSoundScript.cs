using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSoundScript : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private List<Button> buttonOpen;
    [SerializeField] private List<Button> buttonClose;
    [SerializeField] private List<TabButtonScript> buttonTab;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private List<Scrollbar> scroll;
    [SerializeField] private List<Slider> slider;


    [Header("Menu Sound")]
    [SerializeField] private string soundEnterMenu;
    [SerializeField] private string soundButtonOpen;
    [SerializeField] private string soundButtonClose;
    [SerializeField] private string soundPlay;
    [SerializeField] private string soundButtonTab;
    [SerializeField] private string soundScroll;
    [SerializeField] private string soundSlider;

    private void Awake()
    {
        
    }

    public void SoundEnterMenu()
    {
        SoundManager.Instance.PlaySound(soundEnterMenu);
    }

    public void SoundButtonOpen()
    {
        SoundManager.Instance.PlaySound(soundButtonOpen);
    }
    
    public void SoundButtonClose()
    {
        SoundManager.Instance.PlaySound(soundButtonClose);
    }
    public void SoundPlay()
    {
        SoundManager.Instance.PlaySound(soundPlay);
    }

    public void SoundButtonTab()
    {
        SoundManager.Instance.PlaySound(soundButtonTab);
    }
    public void SoundScroll()
    {
        SoundManager.Instance.PlaySound(soundScroll);
    }
    public void SoundSlide()
    {
        SoundManager.Instance.PlaySound(soundSlider);
    }

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
