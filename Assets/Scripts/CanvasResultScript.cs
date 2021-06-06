using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasResultScript : MonoBehaviour
{
    public GameObject backgroundGeneral;
    public GameObject background;
    public CanvasGroup backgroundCanvasGroup;
    public float fadeBackgroundDuration;
    public AnimationCurve curve;

    public TMP_Text textMurderer;

    public TMP_Text caseTitle;

    public TMP_Text clueText; 
    public TMP_Text timeInCrimeScene; 
    public TMP_Text timeInSuspectScene; 
    public TMP_Text caseNotes;

    public GameObject[] stars;
    public Sprite spriteFullStar;
    public Sprite spriteEmptyStar;

    public Button buttonClose;

    private void Awake()
    {
        backgroundGeneral.SetActive(true);
        background.SetActive(false);
        backgroundCanvasGroup.alpha = 0; 

        buttonClose.onClick.AddListener(ReturnToMenu);
    }


    public void UpdateInfo(string _caseTitle, string _clueText, string _timeCrime, string _timeSuspect, string _caseNotes, int _nbStars, int totalClues)
    {
        caseTitle.text = _caseTitle;
        clueText.text = "Clue Founds : " + _clueText +  " / " + totalClues;
        timeInCrimeScene.text = "Time in Crime Scene : " + _timeCrime;
        timeInSuspectScene.text = "Time in Suspect Scene : " + _timeSuspect;
        caseNotes.text = _caseNotes;

        int currentStar = 0;
        foreach (GameObject star in stars)
        {
            if (currentStar < _nbStars)
            {
                star.GetComponent<Image>().sprite = spriteFullStar;
            }
            ++currentStar;
        }
    }

    private void ReturnToMenu()
    {
        LevelManager.Instance.OpenSceneByName("Menu");
    }
}
