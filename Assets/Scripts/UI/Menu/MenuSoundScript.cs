using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSoundScript : MonoBehaviour
{
    [Header("   Buttons to add sound")]
    [SerializeField] private List<Button> buttonsOpen;
    [SerializeField] private List<Button> buttonsClose;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private List<Scrollbar> scrolls;
    [SerializeField] private List<Slider> sliders;

    public Button arrowLeft;
    public Button arrowRight;

    [Header("   Sound Name")]
    [SerializeField] private string soundEnterMenu;
    [SerializeField] private string soundButtonOpen;
    [SerializeField] private string soundButtonClose;
    [SerializeField] private string soundPlay;
    [SerializeField] private string soundScroll;
    [SerializeField] private string soundSlider;
    [SerializeField] private string soundArrowLeft;
    [SerializeField] private string soundArrowRight;

    private void Awake()
    {
        foreach (Button buttonOpen in buttonsOpen)
        {
            buttonOpen.onClick.AddListener(SoundButtonOpen);
        }

        foreach(Button buttonClose in buttonsClose)
        {
            buttonClose.onClick.AddListener(SoundButtonClose);
        }

        buttonPlay.onClick.AddListener(SoundPlay);

        foreach(Scrollbar scrollbar in scrolls)
        {
            scrollbar.onValueChanged.AddListener(SoundScroll);
        }

        foreach(Slider slider in sliders)
        {
            slider.onValueChanged.AddListener(SoundSlider);
        }

        arrowLeft.onClick.AddListener(SoundArrowLeft);
        arrowRight.onClick.AddListener(SoundArrowRight);
    }

    // SET SOUND
    public void SoundEnterMenu()
    {   
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.PlaySound(soundEnterMenu);
    }
    public void SoundButtonOpen()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.PlaySound(soundButtonOpen);
    }
    public void SoundButtonClose()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.PlaySound(soundButtonClose);
    }
    public void SoundPlay()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.PlaySound(soundPlay);
    }
    public void SoundScroll(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }

        if (!SoundManager.Instance.IsSoundPlaying(soundScroll) && 
            value > 0 &&
            value < 1)
        {
            //StartCoroutine(SoundScrollCoroutine);
            //SoundManager.Instance.PlaySound(soundScroll);
        }
    }
    public void SoundSlider(float value)
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }

        if (!SoundManager.Instance.IsSoundPlaying(soundSlider) &&
            value > 0 &&
            value < 1)
        {
            SoundManager.Instance.PlaySound(soundSlider);
        }
    }
    public void SoundArrowLeft()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.PlaySound(soundArrowLeft);
    }
    public void SoundArrowRight()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in scene"); return; }
        SoundManager.Instance.PlaySound(soundArrowRight);
    }
}
