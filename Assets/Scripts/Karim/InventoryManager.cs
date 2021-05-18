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

}
