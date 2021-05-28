using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    #region Variable
    [Header("UI")]
    [SerializeField] private Image image;

    [Header("Debug")]
    public InventoryItem item;
    public InventoryManager inventoryManager;
    #endregion

    public void AddItemToSlot(InventoryItem _item)
    {
        this.item = _item;
        if(_item.sprite != null) this.image.sprite = _item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click On Inventory Slot");
    }
}
