using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public new string name;

    public bool hasDescription = true;
    [TextArea(5,5)]
    public string description;
    public bool hasDefaultImage = true;
    public Sprite sprite;
    public Color spriteColor;
    public int number = 1;

    public bool isClue;
    public bool isPickable;
    public bool isDragable;

    //Esteban Part 
    public LensEnum filter;
    public Sprite spriteOnLens;
    public Color colorOnLens;

    public Sprite spriteIcon;
    public Sprite photoInInventory;
}
