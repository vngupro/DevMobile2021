using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    #region Variable
    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    [Header("Debug")]
    public InventoryItem item;
    public InventoryManager inventoryManager;
    #endregion

    public void AddItemToSlot(InventoryItem _item)
    {
        this.item = _item;
        if(_item.spriteInInventory != null) this.image.sprite = _item.spriteInInventory;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click On Inventory Slot");
        inventoryManager.OnInventorySlotSelected(this);
        
    }

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        button.onClick.AddListener(PlayButtonSound);
    }

    public void PlayButtonSound()
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound("button_item");
        }
    }
}
