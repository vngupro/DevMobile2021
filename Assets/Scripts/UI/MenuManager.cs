using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<UI_Layer> layers = new List<UI_Layer>();
    [SerializeField] private Button playButton;
    [SerializeField] private Button achievementButton;
    [SerializeField] private Button buttonMute;
    [SerializeField] private Button buttonUnmute;

    [Header("Animation")]
    public float fadeDuration = 2.0f;

    private InputManager inputManager;
    private SoundManager soundManager;
    private void Awake()
    {
        inputManager = InputManager.Instance;

        playButton.onClick.AddListener(PlayGame);
        achievementButton.onClick.AddListener(ShowAchievement);

        buttonMute.onClick.AddListener(Unmute);
        buttonUnmute.onClick.AddListener(Mute);
        buttonMute.gameObject.SetActive(false);
        buttonUnmute.gameObject.SetActive(true);

        // Listeners 
        // TapScreenScript.cs
        CustomGameEvents.enteredMenu.AddListener(EnterMenu);
        if (inputManager != null)
        UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
        soundManager.ChangeMute(false);
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

    public void CloseLayer(UI_Layer layer)
    {
        layer.gameObject.SetActive(false);

        if (layer.name == "Layer_Inventory")
        {
            // | Invoke
            // InventoryManager.cs
            CustomGameEvents.closeInventory.Invoke();
        }
    }
    public void OpenLayerByName(string name)
    {
        foreach(UI_Layer layer in layers)
        {
            if(layer.name == name)
            {
                layer.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void CloseLayerByName(string name)
    {
        foreach (UI_Layer layer in layers)
        {
            if (layer.name == name)
            {
                layer.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void EnterMenu()
    {
        string name = "Layer_Menu";
        OpenLayerByName(name);
        foreach (UI_Layer layer in layers)
        {
            if (layer.name == name)
            {
                inputManager.DisableControls();
                CanvasGroup canvasGroup = layer.GetComponent<CanvasGroup>();
                StartCoroutine(Utils.Fade(canvasGroup.alpha, fadeDuration, 0f, 1f, false,
                    returnValue => {
                        canvasGroup.alpha = returnValue;
                    }));
                return;
            }
        }
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
}
