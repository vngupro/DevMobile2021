using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData gameData;
    public LevelData levelData;
    public GameObject canvasResult;

    [SerializeField] private CanvasResultScript canvasResultScript;

    private bool timerStop = false;

    [Header ("Debug")]
    [SerializeField] private float time;
    [SerializeField] private float timeInCrimeScene;
    [SerializeField] private float timeInSuspectScene;
    


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

        canvasResultScript.UpdateInfo(levelData.caseTitle, levelData.totalClues.ToString(), timeInCrimeScene.ToString(), timeInSuspectScene.ToString(), isGuilty.ToString(), 2);
    }

}

[System.Serializable]
public struct LevelData
{
    public int level;
    public int totalClues;
    public string caseTitle;

}
