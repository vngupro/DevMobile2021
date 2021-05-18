using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text descriptionText;
    public InventoryItem currentItem;

    public void SetText(string description, bool buttonActive)
    {
        descriptionText.text = description;
        
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                GameObject temp =
                    Instantiate(blankInventorySlot,
                    inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.myInventory[i], this);
                }
            }
        }
    }

   

    // Start is called before the first frame update
    void Start()
    {
        MakeInventorySlots();
        SetText("", false);
    }
    public void SetupDescriptionn(string newDescription, InventoryItem NewItem)
    {
        currentItem = NewItem;
        descriptionText.text = newDescription;
    }

    [SerializeField] private List<GameObject> clues = new List<GameObject>();

    private UI_Clue[] images;
    public bool isOpen { get; private set; }

    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        images = clues[0].GetComponentsInChildren<UI_Clue>();
        isOpen = false;

        // | Listeners
        // MenuManager.cs
        CustomGameEvents.openInventory.AddListener(OpenInventory);
        CustomGameEvents.closeInventory.AddListener(CloseInventory);
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


        foreach (UI_Clue image in images)
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

    private void OpenInventory()
    {
        isOpen = true;
    }

    private void CloseInventory()
    {
        isOpen = false;
    }

}
