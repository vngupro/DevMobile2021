using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicsManager : MonoBehaviour
{
    public static GraphicsManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        string[] qualities = QualitySettings.names;
        foreach (string quality in qualities)
        {
            Debug.Log("Quality" + quality);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
