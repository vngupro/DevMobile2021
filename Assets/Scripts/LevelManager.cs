using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Add a Blackscreen from the canvas with the script CanvasBlackscreen")]
    [SerializeField] private CanvasBlackscreen canvasBlackscreen;

    private InputManager inputManager;
    private CinemachineBlackscreen cameraBlackscreen;

    [Header("Debug")]
    [SerializeField] private bool hasCameraBlackscreen;
    [SerializeField] private bool hasCanvasBlackscreen;
    public static LevelManager Instance { get; protected set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        inputManager = InputManager.Instance;

        if(cameraBlackscreen == null) cameraBlackscreen = CinemachineBlackscreen.Instance;
        if (canvasBlackscreen == null) canvasBlackscreen = CanvasBlackscreen.Instance;

        if (canvasBlackscreen != null) hasCanvasBlackscreen = true;
        if (cameraBlackscreen != null) hasCameraBlackscreen = true;
        
        // Listen To
        // SwitchLocationDetection.cs
        CustomGameEvents.exitScene.AddListener(OpenSceneByName);
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(hasCanvasBlackscreen || cameraBlackscreen) { UtilsEvent.startFadeOut.Invoke();  }
        
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

        //blackScreen.FadeIn();
        SceneManager.LoadScene(name);
    }
    public void OpenNextScene()
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
