using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SliderTextScript : MonoBehaviour
{
    public TMP_Text textBox;
    public float multiplier = 100f;
    public Slider slider;

    private void Start()
    {
        if(multiplier == 0)
        {
            SetText(slider.value);
        }
        else
        {
            SetTextPercent(slider.value);
        }
    }

    public void SetText(float value)
    {
        textBox.text = value.ToString();
    }

    public void SetTextPercent(float value)
    {
        float newValue = Mathf.Round(value * multiplier);
        textBox.text = newValue.ToString();
    }
}
