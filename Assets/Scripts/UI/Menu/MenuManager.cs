using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    #region Variable
    [Header("   Layer")]
    [SerializeField] private GameObject layerMenu;
    [SerializeField] private GameObject layerOptions;
    [SerializeField] private GameObject layerTapscreen;

    [Header("   Menu")]
    [SerializeField] private Button achievementButton;

    [Header("   Animation")]
    [SerializeField] private float fadeDuration = 2.0f;

    [Header("   Debug")]
    [SerializeField] private InputManager inputManager;
    #endregion
    private void Awake()
    {
        // Init
        inputManager = InputManager.Instance;

        // Layer
        layerMenu.SetActive(false);
        layerOptions.SetActive(false);
        layerTapscreen.SetActive(true);

        // Achievement
        achievementButton.onClick.AddListener(ShowAchievement);

        // Listen To 
        // TapScreenScript.cs
        CustomGameEvents.enteredMenu.AddListener(EnterMenu);
        if (inputManager != null) UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
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
    }
    public void ShowAchievement()
    {
        if(PlayService.Instance == null) { Debug.LogWarning("No PlayService in Scene"); return; }

        Debug.Log("Play Service Show Achievement");
        PlayService.Instance.ShowAchievements();
    }
}
