using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class SuspectManager : MonoBehaviour
{
    #region Event 
    public delegate void SuspectEvent();
    public event SuspectEvent AccuseSuspect;
    public event SuspectEvent OpenSuspectInfo;
    #endregion
    #region Variable
    [SerializeField] private CinemachineVirtualCamera vcamResult;

    public List<Suspect> suspects;

    [Header("Canvas Elements")]
    [SerializeField] private GameObject panelSuspect;
    [SerializeField] private GameObject panelInfo;
    [SerializeField] private GameObject boxSuspectPrefab;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private Image imagePhoto;
    [SerializeField] private Image imageFingerprint;
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonPrevious;
    [SerializeField] private GameObject panelAccuse;
    [SerializeField] private TMP_Text textAccuse;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;

    [Header("Sound")]
    [SerializeField] private string openPanelSound;

    private Suspect currentSuspect;
    private UI_Suspect currentAccuse;

    public static SuspectManager Instance { get; private set; }
    #endregion
    private void Awake()
    {
        Instance = this;

        panelInfo.SetActive(false);
        panelAccuse.SetActive(false);
        InitSuspects();

        // Listen To
        buttonBack.onClick.AddListener(ClosePanelInfo);
        buttonNext.onClick.AddListener(GetNextSuspect);
        buttonPrevious.onClick.AddListener(GetPreviousSuspect);
        buttonYes.onClick.AddListener(YesAccuse);
        buttonNo.onClick.AddListener(NoAccuse);
    }

    private void InitSuspects()
    {
        foreach(Suspect suspect in suspects)
        {
            CreateSuspect(suspect);
        }
    }
    private void CreateSuspect(Suspect suspect)
    {
        GameObject boxSuspect = GameObject.Instantiate(boxSuspectPrefab, panelSuspect.transform);
        UI_Suspect uiSuspect = boxSuspect.GetComponent<UI_Suspect>();
        uiSuspect.data = suspect;
        uiSuspect.suspectManager = this;
    }

    public void OpenPanelInfo(UI_Suspect suspect)
    {
        OpenSuspectInfo?.Invoke();

        panelInfo.SetActive(true);
        UpdateUISuspectData(suspect.data);
    }

    private void ClosePanelInfo()
    {
        panelInfo.SetActive(false);
    }

    private void GetNextSuspect()
    {
        for(int i = 0; i < suspects.Count; i++)
        {
            if(currentSuspect == suspects[i])
            {
                if(i + 1 < suspects.Count)
                {
                    currentSuspect = suspects[i + 1];
                }
                else
                {
                    currentSuspect = suspects[0];
                }

                UpdateUISuspectData(currentSuspect);
                return;
            }
        }
    }

    private void GetPreviousSuspect()
    {
        for (int i = 0; i < suspects.Count; i++)
        {
            if (currentSuspect == suspects[i])
            {
                if (i - 1 >= 0)
                {
                    currentSuspect = suspects[i - 1];
                }
                else
                {
                    currentSuspect = suspects[suspects.Count - 1];
                }

                UpdateUISuspectData(currentSuspect);
                return;
            }
        }
    }

    private void  UpdateUISuspectData(Suspect suspect)
    {
        textBox.text = " NAME : " + suspect.name + "\n\n" +
                " AGE : " + suspect.age + "\n\n" +
                " HEIGHT : " + suspect.height + "\n\n" +
                " SEXE : " + suspect.sexe + "\n\n" +
                " BLOOD TYPE : " + suspect.bloodType + "\n\n" +
                " JOB : " + suspect.job + "\n\n" +
                " NATIONALITY : " + suspect.nationality + "\n\n" +
                " LOCATION DURING MURDER : " + "\n" + suspect.placeOfResidence + "\n\n" +
                " ALIBI : " + "\n" + suspect.alibi + "\n\n" +
                " POTENTIAL MOTIVE : " + "\n" + suspect.suspectedMotive + "\n\n" +
                " DESCRIPTION : " + "\n" + suspect.description;
        imagePhoto.sprite = suspect.sprite;
        imageFingerprint.sprite = suspect.fingerprint;

        currentSuspect = suspect;
    }

    public void OnAccuse(UI_Suspect suspect)
    {
        AccuseSuspect?.Invoke();

        currentAccuse = suspect;
        textAccuse.text = "Accuse\n" + suspect.data.name;
        panelAccuse.SetActive(true);
    }
    
    public void YesAccuse()
    {
        if (CanvasBlackscreen.Instance != null)
        {
            StartCoroutine(SwitchToResult());
        }
        else
        {
            GameManager.Instance.Accuse(currentAccuse.data.isGuilty, currentAccuse.data.sprite);
        }
    }

    public void NoAccuse()
    {
        panelAccuse.SetActive(false);
    }

    IEnumerator SwitchToResult()
    {
        CanvasBlackscreen.Instance?.FadeIn();
        yield return new WaitForSeconds(CanvasBlackscreen.Instance.fadeDuration);
        CustomGameEvents.switchToResult.Invoke(vcamResult);
        GameManager.Instance.Accuse(currentAccuse.data.isGuilty, currentAccuse.data.sprite);
        yield return null;
        CanvasBlackscreen.Instance.FadeOut();
    }
}
