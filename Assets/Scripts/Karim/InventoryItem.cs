using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public new string name;
    [TextArea(5,5)]
    public string description;
    public bool hasDefaultImage = true;
    public Sprite sprite;
    public int number;

    public bool isPickable;
    public bool isDragable;

    //Esteban Part 
    public LensEnum filter;
    public Sprite spriteOnLens;
}
