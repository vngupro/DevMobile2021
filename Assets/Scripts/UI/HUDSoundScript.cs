using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSoundScript : MonoBehaviour
{
    [Header("   Buttons")]
    [SerializeField] private Button buttonNotes;
    [SerializeField] private Button buttonLens;
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonBack;
    [SerializeField] private List<Button> buttonsLensChoices;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonPrevious;
    [SerializeField] private List<Button> buttonsClose;
    [SerializeField] private GameObject groupLens;

    [Header("   Sound")]
    [SerializeField] private string nextSound;
    [SerializeField] private string preivousSound;
    [SerializeField] private string openNotesSound;
    [SerializeField] private string openLensSound;
    [SerializeField] private string closeLensSound;
    [SerializeField] private string selectLensSound;
    [SerializeField] private string openPauseSound;
    [SerializeField] private string buttonsCloseSound;
    [SerializeField] private string openPanelInventorySound;
    [SerializeField] private string buttonBackSound;

    private void Awake()
    {
        buttonNotes.onClick.AddListener(SoundOpenNotes);
        buttonLens.onClick.AddListener(SoundOpenCloseLens);
        buttonPause.onClick.AddListener(SoundOpenPause);
        buttonBack.onClick.AddListener(SoundButtonBack);
        buttonNext.onClick.AddListener(SoundButtonNext);
        buttonPrevious.onClick.AddListener(SoundButtonPrevious);

        foreach (Button button in buttonsLensChoices)
        {
            button.onClick.AddListener(SoundSelectLens);
        }

        foreach (Button button in buttonsClose)
        {
            button.onClick.AddListener(SoundButtonsClose);
        }
    }

    private void Start()
    {
        InventoryManager.Instance.OnOpenPanelInfo += SoundOpenPanelInventory;
    }

    private void SoundButtonNext()
    {
        SoundManager.Instance?.PlaySound(nextSound);
    }

    private void SoundButtonPrevious()
    {
        SoundManager.Instance.PlaySound(preivousSound);
    }

    private void SoundOpenNotes()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }
        SoundManager.Instance.PlaySound(openNotesSound);
    }

    private void SoundOpenCloseLens()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }

        if (HUDManager.Instance.IsGroupLensOpen)
        {
            SoundManager.Instance.PlaySound(openLensSound);
        }
        else
        {
            SoundManager.Instance.PlaySound(closeLensSound);
        }
    }

    private void SoundOpenPause()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }
        SoundManager.Instance.PlaySound(openPauseSound);
    }

    private void SoundSelectLens()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }
        SoundManager.Instance.PlaySound(selectLensSound);
    }

    private void SoundButtonsClose()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }
        SoundManager.Instance.PlaySound(buttonsCloseSound);
    }

    private void SoundOpenPanelInventory()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }
        SoundManager.Instance.PlaySound(openPanelInventorySound);
    }

    private void SoundButtonBack()
    {
        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }
        SoundManager.Instance.PlaySound(buttonBackSound);
    }
}
