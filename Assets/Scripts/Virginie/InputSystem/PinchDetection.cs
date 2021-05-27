using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PinchDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float defaultZoom = 5.0f;
    [SerializeField] private float zoomSpeed = 0.2f;
    [SerializeField] private float zoomInMax = 2f;
    [SerializeField] private float zoomOutMax = 5f;
    [SerializeField] private float distanceTolerance = 15.0f;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Coroutine coroutine;
    #endregion
    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouchSecondary += StartZoom;
        inputManager.OnEndTouchSecondary += EndZoom;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouchSecondary -= StartZoom;
        inputManager.OnEndTouchSecondary -= EndZoom;
    }

    private void StartZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }

        coroutine = StartCoroutine(DetectionZoom());
    }

    private void EndZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }

        StopCoroutine(coroutine);
    }

    IEnumerator DetectionZoom()
    {
        float previousDistance = Vector2.Distance(inputManager.GetPrimaryScreenPosition(), inputManager.GetSecondaryScreenPosition()), 
              distance = 0f;

        while (true)
        {
            Vector2 positionPrimary = inputManager.GetPrimaryScreenPosition();
            Vector2 positionSecondary = inputManager.GetSecondaryScreenPosition();

            distance = Vector2.Distance(positionPrimary, positionSecondary);

            // Detection
            // Zoom Out
            if(distance > previousDistance + distanceTolerance)
            {
                float orthographicSize = Camera.main.orthographicSize;
                float target = Mathf.Clamp(orthographicSize - zoomSpeed, zoomInMax, zoomOutMax);
                Camera.main.orthographicSize = target;
            }
            // Zoom In
            else if(distance < previousDistance - distanceTolerance)
            {
                float orthographicSize = Camera.main.orthographicSize;
                float target = Mathf.Clamp(orthographicSize + zoomSpeed, zoomInMax, zoomOutMax);
                Camera.main.orthographicSize = target;
            }

            //Keep Track of Previous Distance
            previousDistance = distance;

            yield return null;
        }
    }
}
