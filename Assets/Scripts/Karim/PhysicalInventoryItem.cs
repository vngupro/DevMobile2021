using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Player")) ;



    }

    void AddItemToInventory()
    {
        if(playerInventory && thisItem)
        {
            if (playerInventory.itemList.Contains(thisItem))
            {
                thisItem.number += 1;
            }
            else
            {
                playerInventory.itemList.Add(thisItem);
            }
        }
    }
}
