using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public JSONManager json;
    public LevelData levelData;
    public GameObject canvasResult;
    public PlayerInventory inventory;
    private bool timerStop = false;
    public float timerBeforeSecondResult = 8.0f;

    [Header ("Debug")]
    [SerializeField] private CanvasResultScript canvasResultScript;
    [SerializeField] private float time;
    [SerializeField] private float timeInCrimeScene;
    [SerializeField] private float timeInSuspectScene;
    [SerializeField] private int score = 0;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        time = 0.0f;
        
        canvasResultScript = canvasResult.GetComponent<CanvasResultScript>();
        canvasResult.SetActive(false);
    }

    void Update()
    {
        if (!timerStop)
        {
            time += Time.deltaTime;
        }   
    }

    public void ExitCrimeScene()
    {
        timeInCrimeScene = time;
    }

    public void Accuse(bool isGuilty, Sprite sprite)
    {
        timeInSuspectScene = time - timeInCrimeScene;
        timerStop = true;
        canvasResult.SetActive(true);
        CanvasResultScript canvasResultScript = canvasResult.GetComponent<CanvasResultScript>();

        string endText = "";
        if (isGuilty)
        {
            endText = levelData.journalTextCulprit;
        }
        else
        {
            endText = levelData.journalTextInnocent;
        }

        string newsText = "";
        if (isGuilty)
        {
            newsText = levelData.journalTextNewsPaperCulprit;
        }
        else
        {
            newsText = levelData.journalTextNewsPaperInnocent;
        }

        //Timer Conversion
        TimeSpan timeInCrimeSceneMinute = TimeSpan.FromSeconds((int)timeInCrimeScene);
        string textCrime = timeInCrimeSceneMinute.ToString();        
        TimeSpan timeInSuspectSceneMinute = TimeSpan.FromSeconds((int)timeInSuspectScene);
        string textSuspect = timeInSuspectSceneMinute.ToString();

        //Total Clues Founds
        int cluesFound = inventory.itemList.Count;
        string textClues = cluesFound.ToString();

        //Is Guilty Text
        string caseNotesText = isGuilty ? levelData.caseNotesIsGuilty : levelData.caseNotesIsNotGuilty;

        //Score calcule
        if (isGuilty) score++;
        if (timeInCrimeScene < 5 * 60) score++;
        if (timeInSuspectScene < 5 * 60) score++;
        if (cluesFound >= levelData.totalClues / 2) score++;
        if (cluesFound >= levelData.totalClues) score++;
        if (cluesFound < levelData.totalClues / 2) score--;
        if (cluesFound == 0) score--;

        canvasResultScript.UpdateInfo(levelData.caseTitle, textClues, textCrime, textSuspect, caseNotesText, score, levelData.totalClues, endText, newsText, sprite);

        //StartCoroutine(FadeInBackground());
    }


}


[System.Serializable]
public struct LevelData
{
    public int level;
    public int totalClues;
    public string caseTitle;
    [TextArea(3,3)]
    public string caseNotesIsGuilty;
    [TextArea(3, 3)]
    public string caseNotesIsNotGuilty;
    [TextArea(3, 3)]
    public string journalTextCulprit;
    [TextArea(3, 3)]
    public string journalTextInnocent;
    [TextArea(3, 3)]
    public string journalTextNewsPaperCulprit;
    [TextArea(3, 3)]
    public string journalTextNewsPaperInnocent;
}
