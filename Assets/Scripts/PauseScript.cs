using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject layerPause;
    private void Awake()
    {
        layerPause.SetActive(false);
    }
    public void PauseGame()
    {
        Debug.Log("Pause Game");
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Debug.Log("Unpause Game");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        LevelManager.Instance.QuitGame();
    }

    public void BackToMenu()
    {
        LevelManager.Instance.OpenSceneByName("Menu");
    }
}
