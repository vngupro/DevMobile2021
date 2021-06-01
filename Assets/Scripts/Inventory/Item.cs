using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class Item : MonoBehaviour
{
  
    public InventoryItem data;
    public bool isBlocked = false;

    [Header("Debug")]
    public bool isHidden = true;
    private SpriteRenderer spriteRenderer;

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
                spriteRenderer.sprite = data.sprite;
            }
            else
            {
                spriteRenderer.sprite = null;
            }
        }

        if(data.filter != LensEnum.NONE)
        {
            isHidden = true;
        }
        else
        {
            isHidden = false;
        }
    }
   
}
