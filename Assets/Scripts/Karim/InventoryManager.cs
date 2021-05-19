using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventoryy;
    [SerializeField] private InventoryItem thisItemm;

    #region Variable
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text descriptionText;

    [Header("Debug")]
    public InventoryItem currentItem;

    public bool isOpen { get; private set; }
    public static InventoryManager Instance { get; private set; }
    #endregion
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        MakeInventorySlots();
        SetText("", false);
        isOpen = false;

        // | Listeners
        // MenuManager.cs
        CustomGameEvents.openInventory.AddListener(OpenInventory);
        CustomGameEvents.closeInventory.AddListener(CloseInventory);
        CustomGameEvents.pickUpEvent.AddListener(AddItem);
        //CustomGameEvents.pickUpEvent.AddListener(DebugItem);
    }

    private void MakeInventorySlots()
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

    public void SetText(string description, bool buttonActive)
    {
        descriptionText.text = description;
    }

    public void SetupDescription(string newDescription, InventoryItem NewItem)
    {
        currentItem = NewItem;
        descriptionText.text = newDescription;
    }
    private void OpenInventory()
    {
        isOpen = true;
    }

    private void CloseInventory()
    {
        isOpen = false;
    }

    // Karim
    private void AddItem(GameObject item)
    {
        GameObject newItem = item;
        Debug.Log(item.name);
        thisItemm = item.GetComponent<Item>().data;
        Debug.Log(thisItemm);

        if (playerInventory != null && thisItemm != null)
        {
            if (playerInventory.myInventory.Contains(thisItemm))
            {
                thisItemm.numberHeld += 1;
                Debug.Log("+1 " + thisItemm.name);
            }
            else
            {
                playerInventory.myInventory.Add(thisItemm);
                MakeInventorySlots();
                Debug.Log("add item " + thisItemm.name);
            }
        }
        Debug.Log(item.name);
    }

    private void DebugItem(GameObject item)
    {
        Debug.Log(item.name);
    }


    //public void OpenClue(GameObject clue)
    //{
    //    clue.SetActive(true);
    //}

    //public void CloseClue(GameObject clue)
    //{
    //    clue.SetActive(false);
    //}

    //public void SeeBack(GameObject clue)
    //{
    //    foreach (UI_Clue image in images)
    //    {
    //        if (image.name == "Image_Back")
    //        {
    //            image.gameObject.SetActive(true);
    //            Debug.Log("Image back ");
    //        }
    //        else
    //        {
    //            image.gameObject.SetActive(false);
    //        }
    //    }

    //    Debug.Log("See Back of " + clue.name);
    //}

    //public void SeeFront(GameObject clue)
    //{

    //    foreach (UI_Clue image in images)
    //    {
    //        if (image.name == "Image_Front")
    //        {
    //            image.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            image.gameObject.SetActive(false);
    //        }
    //    }

    //    Debug.Log("See Front of " + clue.name);
    //}

    //public void ShowButton(GameObject button)
    //{
    //    button.SetActive(true);
    //}

    //public void HideButton(GameObject button)
    //{
    //    button.SetActive(false);
    //}
}
