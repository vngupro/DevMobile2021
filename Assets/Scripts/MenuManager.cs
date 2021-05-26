using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<UI_Layer> layers = new List<UI_Layer>();

    [Header("Animation")]
    public float fadeInMenuDuration = 2.0f;

    private InputManager inputManager;
    private void Awake()
    {
        inputManager = InputManager.Instance;

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
        string name = "Layer_Menu";
        OpenLayerByName(name);
        foreach (UI_Layer layer in layers)
        {
            if (layer.name == name)
            {
                CanvasGroup canvasGroup = layer.GetComponent<CanvasGroup>();
                StartCoroutine(FadeInLayer(canvasGroup));
                return;
            }
        }
    }

    IEnumerator FadeInLayer(CanvasGroup canvasGroup)
    {
        inputManager.DisableControls();
        float timer = 0f;
        float min = 0f;
        float max = 1f;

        //Fade
        while (timer < fadeInMenuDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeInMenuDuration;
            canvasGroup.alpha = Mathf.Lerp(min, max, ratio);
            yield return null;
        }

        inputManager.EnableControls();
    }
}
