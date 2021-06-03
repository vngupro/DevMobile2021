using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class ChangeFontText : MonoBehaviour
{
    public TMP_FontAsset font;
    private void Start()
    {
        TMP_Text[] textBoxes = FindObjectsOfType<TMP_Text>();
        foreach(TMP_Text textBox in textBoxes)
        {
            textBox.font = font;
        }
    }
}
