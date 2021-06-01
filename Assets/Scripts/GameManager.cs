using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData gameData;
    public LevelData levelData;
    public GameObject canvasResult;
    public PlayerInventory inventory;



    private bool timerStop = false;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    public void Accuse(bool isGuilty)
    {
        timeInSuspectScene = time - timeInCrimeScene;
        timerStop = true;
        canvasResult.SetActive(true);
        Debug.Log("Il est gulty ? " + isGuilty);

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

        canvasResultScript.UpdateInfo(levelData.caseTitle, textClues, textCrime, textSuspect, caseNotesText, score, levelData.totalClues) ;
    }
}

[System.Serializable]
public struct LevelData
{
    public int level;
    public int totalClues;
    public string caseTitle;
    public string caseNotesIsGuilty;
    public string caseNotesIsNotGuilty;

}
