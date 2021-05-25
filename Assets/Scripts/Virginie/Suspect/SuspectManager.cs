using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//box suspect row 
// width : 200
// height : 400

//save after play the arrengment
[ExecuteInEditMode]
public class SuspectManager : MonoBehaviour
{
    [Tooltip("Add Suspect Scriptable Object")]
    public Suspect[] suspects;

    [Header("Auto Organize")]
    public int width = 200;
    public int space = 0;
    [Header("Canvas Elements")]
    [SerializeField]
    private GameObject boxSuspectRow;
    [SerializeField]
    private GameObject boxSuspectPrefab;
    [SerializeField]
    private MaxSuspectPerLevel maxSuspectPerLevel;


    [Header("Debug")]
    [SerializeField]
    private int level = 0;
    private List<UI_Suspect> uiSuspects = new List<UI_Suspect>();


    private void Awake()
    {
        UI_Suspect[] tempUISuspects = boxSuspectRow.GetComponentsInChildren<UI_Suspect>();

        if(tempUISuspects.Length != 0)
        {
            foreach (UI_Suspect uiSuspect in tempUISuspects)
            {
                uiSuspects.Add(uiSuspect);
            }
        }

        if(suspects.Length != 0)
        {
            InitSuspects();
        }
    }
    private void InitSuspects()
    {
        int index = 0;
        Debug.Log("Init Suspect");
        foreach(Suspect suspect in suspects)
        {
            //Protection against null value
            if (maxSuspectPerLevel.maxSuspectList.Count < level) {
                Debug.Log("You forgot to add a max suspect number for that level");
                maxSuspectPerLevel.maxSuspectList.Add(3); 
            }

            //Do not add more suspect than necessary
            if (index > maxSuspectPerLevel.maxSuspectList[level]) return;

            //Add suspect in scene
            if (uiSuspects.Count != 0 && index < uiSuspects.Count)
            {
                uiSuspects[index].image.sprite = suspect.sprite;
                uiSuspects[index].textDescription.text = suspect.description;
                uiSuspects[index].isGuilty = suspect.isGuilty;
                uiSuspects[index].layerDescription.gameObject.SetActive(false);
            }
            else
            {
                GameObject newBoxSuspect = Instantiate(boxSuspectPrefab, boxSuspectRow.transform) as GameObject;
                UI_Suspect newUISuspect = newBoxSuspect.GetComponent<UI_Suspect>();
                newUISuspect.image.sprite = suspect.sprite;
                uiSuspects.Add(newUISuspect);
                RectTransform rectTransform = newBoxSuspect.GetComponent<RectTransform>();

                //pair
                if (index != 0)
                {
                    if (index % 2 == 0)
                    {
                        rectTransform.position = new Vector3(
                            rectTransform.position.x +  (width + space),
                            rectTransform.position.y,
                            rectTransform.position.z
                            );
                    }
                    else
                    {
                        rectTransform.position = new Vector3(
                            rectTransform.position.x - (width + space),
                            rectTransform.position.y,
                            rectTransform.position.z
                            );
                    }
                }

                uiSuspects[index].image.sprite = suspect.sprite;
                uiSuspects[index].textDescription.text = suspect.description;
                uiSuspects[index].isGuilty = suspect.isGuilty;
                uiSuspects[index].layerDescription.gameObject.SetActive(false);
            }
            index++;
        }
    }

    public void Accuse()
    {
        UI_Suspect data = EventSystem.current.currentSelectedGameObject.GetComponentInParent<UI_Suspect>();
        if (data.isGuilty)
        {
            Debug.Log("You Find The Culprit !");
        }
        else
        {
            Debug.Log("You Failure ! ");
        }
    }
}
