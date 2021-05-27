using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    #region Variable
    [Header("UI")]
    [SerializeField] private TMP_Text textNumber;
    [SerializeField] private Image image;

    [Header("Variables from the item")]
    public InventoryItem item;
    public InventoryManager inventoryManager;
    #endregion

    public void AddItemToSlot(InventoryItem _item)
    {
        this.item = _item;

        if(_item.itemImage != null) this.image.sprite = _item.itemImage;
        this.textNumber.text = _item.numberHeld.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click On Inventory Slot");
    }
}
