using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<UI_Layer> layers = new List<UI_Layer>();
    public void OpenLayer(UI_Layer layer)
    {
        //Debug.Log("Open Layer");
        layer.gameObject.SetActive(true);
    }

    public void CloseLayer(UI_Layer layer)
    {
        //Debug.Log("Close Layer");
        layer.gameObject.SetActive(false);
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
}
