using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    #region Variable
    [Tooltip(("Add Scriptable Object Inventory"))]
    public PlayerInventory inventory;
    public int inventoryIndex = 0;

    [Header("UI")]
    [Tooltip("Prefabs for inventory slot")]
    [SerializeField] private GameObject prefabSlot;
    [SerializeField] private GameObject panelInventory;
    [SerializeField] private int maxNumberPerRow = 9;

    public GameObject panelSecond;
    public Button buttonBack;
    public Button buttonNext;
    public Button buttonPrevious;
    public Image imageClue;
    public TMP_Text descriptionClue;

    [Header("Debug")]
    [SerializeField] private InventoryItem currentItem;
    [SerializeField] private int countSlot = 0;
    [SerializeField] private int row = 0;

    [SerializeField]
    private float slotWidth;
    public bool isOpen { get; private set; }

    public static InventoryManager Instance { get; private set; }
    #endregion
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        isOpen = false;

        slotWidth = prefabSlot.GetComponent<RectTransform>().rect.width;

        //UI
        buttonBack.onClick.AddListener(ClosePanelSecond);
        buttonNext.onClick.AddListener(GetNextItem);
        buttonPrevious.onClick.AddListener(GetPreviousItem);
        panelSecond.SetActive(false);

        MakeInventorySlots();

        // | Listeners
        // MenuManager.cs
        CustomGameEvents.openInventory.AddListener(OpenInventory);
        CustomGameEvents.closeInventory.AddListener(CloseInventory);
        CustomGameEvents.pickUpEvent.AddListener(AddItem);
    }

    private void OnDisable()
    {
        inventory.itemList.Clear();
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
        GameObject temp = Instantiate(prefabSlot, panelInventory.transform);
        InventorySlot newSlot = temp.GetComponent<InventorySlot>();
        if (newSlot)
        {
            Debug.Log(item.name);
            newSlot.AddItemToSlot(item);
        }

        if (countSlot > 0 && (countSlot % maxNumberPerRow) == 0)
        {
            row++;
        }

        RectTransform rectTransform = temp.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x + (slotWidth * 2) * (countSlot % maxNumberPerRow), 
            rectTransform.anchoredPosition.y - (slotWidth * 2) * row);
        countSlot++;

    }

    public void SetupDescription(string newDescription, InventoryItem NewItem)
    {
        currentItem = NewItem;
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
    {;
        this.currentItem = item.GetComponent<Item>().data;

        if (inventory != null && this.currentItem != null)
        {
            if (inventory.itemList.Contains(this.currentItem))
            {
                this.currentItem.number += 1;
            }
            else
            {
                inventory.itemList.Add(this.currentItem);
                CreateSlot(this.currentItem);
            }
        }
    }

    //HUD
    public void ClosePanelSecond()
    {
        panelSecond.SetActive(false);
    }

    public void OnInventorySlotSelected(InventorySlot slot)
    {
        panelSecond.SetActive(true);
        string description = slot.item.description;

        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            if (inventory.itemList[i] == slot.item)
            {
                inventoryIndex = i;
                break;
            }
        }

        //Add information on corresponding panel
        ShowItemData(slot.item);
    }

    public void GetNextItem() {
        int indexOfLastItem = inventory.itemList.Count - 1;
        if (inventoryIndex < indexOfLastItem) { 
            inventoryIndex++;
        }
        else
        {
            inventoryIndex = 0;
        }

        ShowItemData(inventory.itemList[inventoryIndex]);
    }

    public void GetPreviousItem() {
        if (inventoryIndex > 0)
        {
            inventoryIndex--;
        }
        else
        {
            inventoryIndex = inventory.itemList.Count - 1;
        }

        ShowItemData(inventory.itemList[inventoryIndex]);
    }

    public void ShowItemData(InventoryItem item)
    {
        imageClue.sprite = item.spriteInInventory;
        descriptionClue.text = item.description;
    }
}
