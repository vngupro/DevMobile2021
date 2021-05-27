using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class Item : MonoBehaviour
{
  
    public InventoryItem data;
    private SpriteRenderer spriteRenderer;
   
    [ExecuteInEditMode]
   
   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (data == null)
        {
            Debug.Log("Item : " + this.name + " has no data !\nPlease add an Inventory Item (Scriptable Object) in \"Data\"");
        }
        else
        {
            if (data.hasDefaultImage)
            {
                if (data.itemImage == null)
                {
                   Debug.Log("Item : " + this.name + " has no Sprite in scriptable object !");
                }
                else
                {
                    spriteRenderer.sprite = data.itemImage;
                }
            }
            else
            {
                spriteRenderer.sprite = null;
            }
        }
    }
   
}
