using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    #region Variable
    [SerializeField] private Canvas canvas;

    [Header("   Layer")]
    [SerializeField] private GameObject layerMenu;
    [SerializeField] private GameObject layerOptions;
    [SerializeField] private GameObject layerTapscreen;

    [Header("   Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button achievementButton;

    [Header("   Sound Options")]
    [SerializeField] private Button buttonMute;
    [SerializeField] private Button buttonUnmute;

    [Header("   Graphics Options")]
    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private Button buttonArrowLeft;
    [SerializeField] private Button buttonArrowRight;

    private string[] qualities;
    private int currentQualityIndex = 0;

    [Header("   Animation")]
    public float fadeDuration = 2.0f;

    private InputManager inputManager;
    private SoundManager soundManager;
    private GraphicsManager graphicsManager;

    #endregion
    private void Awake()
    {
        inputManager = InputManager.Instance;

        // Layer
        layerMenu.SetActive(false);
        layerOptions.SetActive(false);
        layerTapscreen.SetActive(true);

        // Menu
        playButton.onClick.AddListener(PlayGame);
        achievementButton.onClick.AddListener(ShowAchievement);

        // Sound
        buttonMute.onClick.AddListener(Unmute);
        buttonUnmute.onClick.AddListener(Mute);
        buttonMute.gameObject.SetActive(false);
        buttonUnmute.gameObject.SetActive(true);

        //Graphics
        qualities = QualitySettings.names;
        buttonArrowLeft.onClick.AddListener(PreviousQuality);
        buttonArrowRight.onClick.AddListener(NextQuality);

        // Listeners 
        // TapScreenScript.cs
        CustomGameEvents.enteredMenu.AddListener(EnterMenu);
        if (inputManager != null)
        UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
        if(soundManager != null)
        soundManager.ChangeMute(false);

        graphicsManager = GraphicsManager.Instance;
        currentQualityIndex = qualities.Length / 2;
        SetQualityMenu();
    }

    public void OpenLayer(UI_Layer layer)
    {
        layer.gameObject.SetActive(true);

        if(layer.name == "Layer_Inventory")
        {
            // | Invoke
            // InventoryManager.cs
            CustomGameEvents.openInventory.Invoke();
        }
    }


    private void EnterMenu()
    {
        GetComponent<MenuSoundScript>().SoundEnterMenu();
        inputManager.DisableControls();
        layerMenu.SetActive(true);
        CanvasGroup canvasGroup = layerMenu.GetComponent<CanvasGroup>();
        StartCoroutine(Utils.Fade(canvasGroup.alpha, fadeDuration, 0f, 1f, false,
            returnValue => {
                canvasGroup.alpha = returnValue;
            }));
        return;
    }

    private void PlayGame()
    {
        LevelManager.Instance.OpenSceneByName("Tutorial Safe");
    }

    public void ShowAchievement()
    {
        PlayService.Instance.ShowAchievements();
    }

    private void Mute()
    {
        Debug.Log("Mute");
        buttonMute.gameObject.SetActive(true);
        buttonUnmute.gameObject.SetActive(false);
        soundManager.ChangeMute(true);
    }

    private void Unmute()
    {
        Debug.Log("Unmute");
        buttonMute.gameObject.SetActive(false);
        buttonUnmute.gameObject.SetActive(true);
        soundManager.ChangeMute(false);
    }

    private void NextQuality()
    {
        if(currentQualityIndex + 1 < qualities.Length)
        {
            currentQualityIndex++;
            SetQualityMenu();
        }
    }

    private void PreviousQuality()
    {
        if(currentQualityIndex - 1 >= 0)
        {
            currentQualityIndex--;
            SetQualityMenu();
        }
    }

    private void SetQualityMenu()
    {
        if(graphicsManager != null)
        graphicsManager.SetQuality(currentQualityIndex);
        qualityText.text = qualities[currentQualityIndex];
    }
}
