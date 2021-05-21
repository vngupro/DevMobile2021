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
        uiSuspects = boxSuspectRow.GetComponentsInChildren<UI_Suspect>();

        InitSuspects();
    }
    private void InitSuspects()
    {
        Debug.Log("Create Suspect");
        //GameObject newSuspect = Instantiate(boxSuspectPrefab, boxSuspectRow.transform) as GameObject;
        //UI_Suspect newUISuspect = newSuspect.GetComponent<UI_Suspect>();
        //newUISuspect.image.sprite = suspect.sprite;
        //Debug.Log(newUISuspect.gameObject.name);
        //uiSuspects[index] = newUISuspect;
        //if(uiSuspects != null)
        //{
        //    int i, j;
        //    for(i = 0; i < suspects.L; i++)
        //    {
        //        uiSuspects[i].image.sprite = suspects[i].sprite;
        //    }

        //    if(suspects.Length > uiSuspects.Length)
        //    {

        //    }
        //}
        //else
        //{
        //    foreach (Suspect suspect in suspects)
        //    {
        //        Debug.Log("Suspect : " + suspect.name);
        //        GameObject newSuspect = Instantiate(boxSuspectPrefab, boxSuspectRow.transform) as GameObject;
        //        UI_Suspect newUISuspect = newSuspect.GetComponent<UI_Suspect>();
        //        newUISuspect.image.sprite = suspect.sprite;
        //        Debug.Log(newUISuspect.gameObject.name);
        //        uiSuspects[index] = newUISuspect;

        //        index++;
        //    }
        //}
    }
}
