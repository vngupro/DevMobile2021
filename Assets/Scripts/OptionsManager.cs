using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] GraphicsOptions graphics;
    [SerializeField] SoundOptions soundScript;
    [SerializeField] GameplayOptions gameplay;

    private string qualityKey = "Quality";
    private string masterKey = "Master";
    private string musicKey = "Music";
    private string sfxKey = "SFX";
    private string uiKey = "UI";
    private string ambiantKey = "Ambient";
    private string muteKey = "Mute";
    private string panKey = "Pan";
    private string zoomKey = "Zoom";

    private int qualityValue = 0;
    private float masterValue = 0;
    private float musicValue = 0;
    private float sfxValue = 0;
    private float uiValue = 0;
    private float ambiantValue = 0;
    private bool muteValue = false;
    private float panValue = 0;
    private float zoomValue = 0;

    public static OptionsManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    private void OnDisable()
    {
        SaveOptions();
    }
    private void Start()
    {
        LoadOptions();
    }

    public void SaveOptions()
    {
        Debug.Log("Save Options");

        //Get all options value
        PlayerPrefs.SetInt(qualityKey, graphics.currentQualityIndex);
        PlayerPrefs.SetFloat(masterKey, soundScript.masterSlider.value);
        PlayerPrefs.SetFloat(musicKey, soundScript.musicSlider.value);
        PlayerPrefs.SetFloat(sfxKey, soundScript.sfxSlider.value);
        PlayerPrefs.SetFloat(uiKey, soundScript.uiSlider.value);
        PlayerPrefs.SetFloat(ambiantKey, soundScript.ambiantSlider.value);
        PlayerPrefs.SetInt(muteKey, soundScript.buttonMute.IsActive() ? 1 : 0);
        PlayerPrefs.SetFloat(panKey, gameplay.panSlider.value);
        PlayerPrefs.SetFloat(zoomKey, gameplay.zoomSlider.value);
    }

    public void LoadOptions()
    {
        Debug.Log("Load Options");

        if (!PlayerPrefs.HasKey(qualityKey)) return;
        //Set all options value
        qualityValue = PlayerPrefs.GetInt(qualityKey);
        masterValue = PlayerPrefs.GetFloat(masterKey);
        musicValue = PlayerPrefs.GetFloat(musicKey);
        sfxValue = PlayerPrefs.GetFloat(sfxKey);
        uiValue = PlayerPrefs.GetFloat(uiKey);
        ambiantValue = PlayerPrefs.GetFloat(ambiantKey); ;
        muteValue = (PlayerPrefs.GetInt(muteKey) == 0) ? false : true;
        panValue = PlayerPrefs.GetFloat(panKey);
        zoomValue = PlayerPrefs.GetFloat(zoomKey);

        //Update UI Visual
        graphics.LoadQuality(qualityValue);
        soundScript.LoadSoundsOptions(masterValue, musicValue, sfxValue, uiValue, ambiantValue, muteValue);
        gameplay.LoadGameplayOptions(panValue, zoomValue);
    }
}
