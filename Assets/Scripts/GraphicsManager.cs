using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicsManager : MonoBehaviour
{
    public TMP_Dropdown dropdownQuality;

    private void Awake()
    {
        string[] qualities = QualitySettings.names;
        List<TMP_Dropdown.OptionData> graphicOptions = new List<TMP_Dropdown.OptionData>();
        dropdownQuality.ClearOptions();
        foreach (string quality in qualities)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = quality;
            graphicOptions.Add(data);
        }

        dropdownQuality.AddOptions(graphicOptions);
        SetQuality(graphicOptions.Count - 2);
        dropdownQuality.value = graphicOptions.Count - 2;
        dropdownQuality.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
