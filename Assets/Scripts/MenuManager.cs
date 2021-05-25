using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<UI_Layer> layers = new List<UI_Layer>();

    private void Awake()
    {
        // Listeners 
        // TapScreenScript.cs
        CustomGameEvents.enterMenu.AddListener(EnterMenu);
    }
    public void OpenLayer(UI_Layer layer)
    {
        layer.gameObject.SetActive(true);

        if(layer.name == "Layer_Inventory")
        {
            // | Invoke
            // InventoryManager.cs
            CustomGameEvents.openInventory.Invoke();
        }
    }

    public void CloseLayer(UI_Layer layer)
    {
        layer.gameObject.SetActive(false);

        if (layer.name == "Layer_Inventory")
        {
            // | Invoke
            // InventoryManager.cs
            CustomGameEvents.closeInventory.Invoke();
        }
    }
    public void OpenLayerByName(string name)
    {
        foreach(UI_Layer layer in layers)
        {
            if(layer.name == name)
            {
                layer.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void CloseLayerByName(string name)
    {
        foreach (UI_Layer layer in layers)
        {
            if (layer.name == name)
            {
                layer.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void EnterMenu()
    {
        OpenLayerByName("Layer_Menu");
    }
}
