using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-1)]
[ExecuteInEditMode]
public class ChangeFontText : MonoBehaviour
{
    public TMP_FontAsset font;


    private void Awake()
    {
        TMP_Text[] textBoxes = Resources.FindObjectsOfTypeAll<TMP_Text>();
        //TMP_Text[] textBoxes = FindObjectsOfType<TMP_Text>();
        foreach(TMP_Text textBox in textBoxes)
        {
            textBox.font = font;
        }
    }
}
