using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Item : MonoBehaviour
{
    public InventoryItem data;
    private SpriteRenderer spriteRenderer;
    public bool isHidden = true;

    [ExecuteInEditMode]
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Make sprite appear in scene in edit mode
        if (data == null)
        {
            Debug.Log("Item : " + this.name + " has no data !\nPlease add an Inventory Item (Scriptable Object) in \"Data\"");
        }
        else
        {
            if (data.hasDefaultImage)
            {
                if (data.sprite == null)
                {
                   Debug.Log("Item : " + this.name + " has no Sprite in scriptable object !");
                }
                else
                {
                    spriteRenderer.sprite = data.sprite;
                }
            }
            else
            {
                spriteRenderer.sprite = null;
            }
        }
    }
}
