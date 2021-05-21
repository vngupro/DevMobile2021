using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class SuspectManager : MonoBehaviour
{
    [Tooltip("Add Suspect Scriptable Object")]
    public Suspect[] suspects;

    [Header("Canvas Elements")]
    [SerializeField]
    private GameObject boxSuspectRow;
    [SerializeField]
    private GameObject boxSuspectPrefab;
    private UI_Suspect[] uiSuspects = new UI_Suspect[10];

    private void Awake()
    {
        CreateUISuspect();
    }

    private void CreateUISuspect()
    {
        int index = 0;
        uiSuspects = boxSuspectRow.GetComponentsInChildren<UI_Suspect>();
        if(uiSuspects != null)
        {
            int i, j;
            for(i = 0; i < uiSuspects.Length; i++)
            {
                uiSuspects[i].image.sprite = suspects[i].sprite;
            }

            if(suspects.Length > uiSuspects.Length)
            {
                for(j = i; j < suspects.Length - i; j++)
                {
                    GameObject newSuspect = Instantiate(boxSuspectPrefab, boxSuspectRow.transform) as GameObject;
                    UI_Suspect newUISuspect = newSuspect.GetComponent<UI_Suspect>();
                    newUISuspect.image.sprite = suspects[j+i].sprite;
                    Debug.Log(newUISuspect.gameObject.name);
                    uiSuspects[i+j -1] = newUISuspect;
                }
            }
        }
        else
        {
            foreach (Suspect suspect in suspects)
            {
                Debug.Log("Suspect : " + suspect.name);
                GameObject newSuspect = Instantiate(boxSuspectPrefab, boxSuspectRow.transform) as GameObject;
                UI_Suspect newUISuspect = newSuspect.GetComponent<UI_Suspect>();
                newUISuspect.image.sprite = suspect.sprite;
                Debug.Log(newUISuspect.gameObject.name);
                uiSuspects[index] = newUISuspect;

                index++;
            }
        }
    }
}
