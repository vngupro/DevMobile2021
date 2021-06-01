using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasResultScript : MonoBehaviour
{
    public TMP_Text caseTitle;

    public TMP_Text clueText; 
    public TMP_Text timeInCrimeScene; 
    public TMP_Text timeInSuspectScene; 
    public TMP_Text caseNotes;

    public GameObject[] stars;
    public Sprite spriteFullStar;
    public Sprite spriteEmptyStar;



    public void UpdateInfo(string _caseTitle, string _clueText, string _timeCrime, string _timeSuspect, string _caseNotes, int _nbStars)
    {
        caseTitle.text = _caseTitle;
        clueText.text = _clueText;
        timeInCrimeScene.text = _timeCrime;
        timeInSuspectScene.text = _timeSuspect;
        caseNotes.text = _caseNotes;

        int currentStar = 0;
        foreach (GameObject star in stars)
        {
            if (currentStar <= _nbStars)
            {
                star.GetComponent<Image>().sprite = spriteFullStar;
            }
            ++currentStar;
        }
    }
}
