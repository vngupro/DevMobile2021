using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    #region Variable
    [Header("Inventory")]
    [Tooltip(("Add Scriptable Object Player Inventory"))]
    public PlayerInventory inventory;

    [Header("UI")]
    [Tooltip("Prefabs for inventory slot")]
    [SerializeField] private GameObject prefabSlot;
    [SerializeField] private GameObject panelInventory;
    [SerializeField] private TMP_Text descriptionText;

    [Header("Debug")]
    [SerializeField] private InventoryItem currentItem;

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
        if (inventory)
        {
            foreach(InventoryItem item in inventory.itemList)
            {
                CreateSlot(item);
            }
        }
    }

    public void CreateSlot(InventoryItem item)
    {
        GameObject temp = Instantiate(
                    prefabSlot,
                    panelInventory.transform.position,
                    Quaternion.identity
                    );
        temp.transform.SetParent(panelInventory.transform);

        InventorySlot newSlot = temp.GetComponent<InventorySlot>();
        if (newSlot)
        {
            Debug.Log(item.name);
            newSlot.AddItemToSlot(item);
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

    private void AddItem(GameObject item)
    {
        GameObject newItem = item;
        Debug.Log(item.name);
        this.currentItem = item.GetComponent<Item>().data;
        Debug.Log(this.currentItem);

        if (inventory != null && this.currentItem != null)
        {
            if (inventory.itemList.Contains(this.currentItem))
            {
                this.currentItem.numberHeld += 1;
                Debug.Log("+1 " + this.currentItem.name);
            }
            else
            {
                inventory.itemList.Add(this.currentItem);
                MakeInventorySlots();
                Debug.Log("add item " + this.currentItem.name);
            }
        }
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
