using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject layerPause;
    [SerializeField] private float soundDiminution = 4f;
    private void Awake()
    {
        layerPause.SetActive(false);
    }
    public void PauseGame()
    {
        Debug.Log("Pause Game");
        Time.timeScale = 0;

        if(SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }

        foreach(Sound s in SoundManager.Instance.sounds)
        {
            if(s.type == SoundType.MUSIC)
            {
                s.source.volume /= soundDiminution;
            }

            if(s.type == SoundType.AMBIENT)
            {
                s.source.volume /= soundDiminution;
            }
        }

    }

    public void UnpauseGame()
    {
        Debug.Log("Unpause Game");
        Time.timeScale = 1;

        if (SoundManager.Instance == null) { Debug.LogWarning("No Sound Manager in Scene"); return; }

        foreach (Sound s in SoundManager.Instance.sounds)
        {
            if (s.type == SoundType.MUSIC)
            {
                s.source.volume *= soundDiminution;
            }

            if (s.type == SoundType.AMBIENT)
            {
                s.source.volume *= soundDiminution;
            }
        }
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
