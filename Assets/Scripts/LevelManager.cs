using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private InputManager inputManager;
    private CinemachineBlackscreen cameraBlackscreen;

    [Header("Debug")]
    [SerializeField] private CanvasBlackscreen canvasBlackscreen;
    [SerializeField] private bool hasCameraBlackscreen = false;
    [SerializeField] private bool hasCanvasBlackscreen = false;
    [SerializeField] private bool isLoadingScene = false;
    [SerializeField] private string sceneToLoad;
    public static LevelManager Instance { get; protected set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;


        inputManager = InputManager.Instance;

        if(cameraBlackscreen == null) cameraBlackscreen = CinemachineBlackscreen.Instance;
        if (canvasBlackscreen == null) canvasBlackscreen = CanvasBlackscreen.Instance;

        if (canvasBlackscreen != null) hasCanvasBlackscreen = true;
        if (cameraBlackscreen != null) hasCameraBlackscreen = true;

        isLoadingScene = false;

        // Listen To
        //Utils.cs
        UtilsEvent.fadeInEnded.AddListener(LoadNextScene);
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(hasCanvasBlackscreen || hasCameraBlackscreen) { UtilsEvent.startFadeOut.Invoke();  }

        isLoadingScene = false;
        // Listeners | PinchDetection.cs SlideOneFingerDetection.cs
        CustomGameEvents.sceneLoaded.Invoke();
        Debug.Log("Scene Loaded : " + scene.name);

    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OpenSceneByName(string name)
    {
        if (name == "")
        {
            Debug.Log("Scene : No Scene to load\nDid you forget to add a name ?"); 
            return;
        }

        if(hasCameraBlackscreen || hasCanvasBlackscreen)
        {
            UtilsEvent.startFadeIn.Invoke();
            isLoadingScene = true;
            sceneToLoad = name;
        }
        else
        {
            SceneManager.LoadScene(name);
        }
    }
    public void OpenNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (hasCameraBlackscreen || hasCanvasBlackscreen)
        {
            UtilsEvent.startFadeIn.Invoke();
            isLoadingScene = true;
            sceneToLoad = SceneManager.GetSceneByBuildIndex(nextSceneIndex).name;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void LoadNextScene()
    {
        if (!isLoadingScene) return;

        SceneManager.LoadScene(sceneToLoad);
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
