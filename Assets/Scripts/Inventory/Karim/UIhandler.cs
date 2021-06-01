using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhandler : MonoBehaviour
{
    public GameObject LevelDialog;
    public Text LesvelStatus;
    public Text scoreText;

    public static UIhandler instance;

     void Awake()
    {
        if (instance == null) instance = this ;
        

    }

    public void ShowLevelDialog(string status, string scores)
    {
        GetComponent<StarsScript>().starAcheived();
        LevelDialog.SetActive(true);
        LesvelStatus.text = status;
        scoreText.text = scores;
    }
}
