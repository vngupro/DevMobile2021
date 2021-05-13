using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> clues = new List<GameObject>();

    UI_Clue[] images;

    private void Awake()
    {
        images = clues[0].GetComponentsInChildren<UI_Clue>();
    }
    public void OpenClue(GameObject clue)
    {
        clue.SetActive(true);
    }

    public void CloseClue(GameObject clue)
    {
        clue.SetActive(false);
    }

    public void SeeBack(GameObject clue)
    {


        foreach(UI_Clue image in images)
        {
            if (image.name == "Image_Back")
            {
                image.gameObject.SetActive(true);
                Debug.Log("Image back ");
            }
            else
            {
                image.gameObject.SetActive(false);
            }
        }

        Debug.Log("See Back of " + clue.name);
    }

    public void SeeFront(GameObject clue)
    {

        foreach (UI_Clue image in images)
        {
            if (image.name == "Image_Front")
            {
                image.gameObject.SetActive(true);
            }
            else
            {
                image.gameObject.SetActive(false);
            }
        }

        Debug.Log("See Front of " + clue.name);
    }

    public void HideButton(GameObject button)
    {
        button.SetActive(false);
    }

    public void ShowButton(GameObject button)
    {
        button.SetActive(true);
    }
}

