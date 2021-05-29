using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<UI_Layer> layers = new List<UI_Layer>();

    [Header("Animation")]
    public float fadeDuration = 2.0f;

    private InputManager inputManager;
    private void Awake()
    {
        inputManager = InputManager.Instance;

        // Listeners 
        // TapScreenScript.cs
        CustomGameEvents.enteredMenu.AddListener(EnterMenu);
        UtilsEvent.fadeInEnded.AddListener(inputManager.EnableControls);
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
                inputManager.DisableControls();
                CanvasGroup canvasGroup = layer.GetComponent<CanvasGroup>();
                StartCoroutine(Utils.Fade(canvasGroup.alpha, fadeDuration, 0f, 1f, false,
                    returnValue => {
                        canvasGroup.alpha = returnValue;
                    }));
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
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(min, max, ratio);
            yield return null;
        }

        inputManager.EnableControls();
    }
}
