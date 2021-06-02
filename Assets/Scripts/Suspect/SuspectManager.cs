using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuspectManager : MonoBehaviour
{
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
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    [SerializeField] private Image suspectImage;

    private Suspect currentSuspect;
    private UI_Suspect currentAccuse;
    private void Awake()
    {
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
        suspectImage.sprite = suspect.sprite;
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
        Debug.Log("On Accuse");

        currentAccuse = suspect;
        panelAccuse.SetActive(true);
    }
    
    public void YesAccuse()
    {
        GameManager.Instance.Accuse(currentAccuse.data.isGuilty);
    }

    public void NoAccuse()
    {
        panelAccuse.SetActive(false);
    }
}
