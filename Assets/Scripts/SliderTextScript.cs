using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderTextScript : MonoBehaviour
{
    public TMP_Text textBox;

    public void SetText(float value)
    {
        textBox.text = value.ToString();
    }

    public void SetTextPercent(float value)
    {
        float newValue = Mathf.Round(value * 100);
        textBox.text = newValue.ToString();
    }
}
