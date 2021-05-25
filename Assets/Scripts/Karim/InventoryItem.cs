using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    [TextArea(5,5)]
    public string itemDescription;
    public bool hasDefaultImage = true;
    public Sprite itemImage;
    public int numberHeld;

    public bool isPickable;
    public bool isDragable;

    //Esteban Part 
    public LensEnum filter;
    public Sprite itemImageOnLens;
}
