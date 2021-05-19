using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InventoryItem data;

    private void Awake()
    {
        if (data == null)
        {
            Debug.Log("Item : " + this.name + " has no data !\nPlease add an Inventory Item (Scriptable Object) in \"Data\"");
        }
    }
}
