using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuGraphics : MonoBehaviour
{
    [Header("   Graphics Options")]
    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private Button buttonArrowLeft;
    [SerializeField] private Button buttonArrowRight;



    [Header("   Debug")]
    [SerializeField] private string[] qualities;
    [SerializeField] private int currentQualityIndex;
    private void Awake()
    {
        // Init Quality
        qualities = QualitySettings.names;
        currentQualityIndex = qualities.Length / 2;
        UpdateQuality();

        // Buttons
        buttonArrowLeft.onClick.AddListener(PreviousQuality);
        buttonArrowRight.onClick.AddListener(NextQuality);
    }

    private void NextQuality()
    {
        if (currentQualityIndex + 1 < qualities.Length)
        {
            currentQualityIndex++;
            UpdateQuality();
        }
    }

    private void PreviousQuality()
    {
        if (currentQualityIndex - 1 >= 0)
        {
            currentQualityIndex--;
            UpdateQuality();
        }
    }

    private void UpdateQuality()
    {
        QualitySettings.SetQualityLevel(currentQualityIndex);
        qualityText.text = qualities[currentQualityIndex];
    }
}
