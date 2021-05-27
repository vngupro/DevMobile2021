using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private BlackScreenScript blackScreen;
    private InputManager inputManager;
    public static LevelManager Instance { get; protected set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("Destroy" + this.gameObject.name);
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        inputManager = InputManager.Instance;
        blackScreen = FindObjectOfType<BlackScreenScript>();
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (blackScreen != null)
        {
            if(scene.name == "Menu")
                blackScreen.FadeOut();
        }

        inputManager.EnableControls();
        CustomGameEvents.changeScene.Invoke();
        Debug.Log("Scene Loaded = " + scene.name);

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
