using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PinchDetection : MonoBehaviour
{

    [SerializeField] private float zoomSpeed = 0.2f;
    [SerializeField] private float zoomInMax = 2f;
    [SerializeField] private float zoomOutMax = 5f;
    [SerializeField] private float distanceTolerance = 15.0f;
    private InputManager inputManager;
    private Vector2 startPositionPrimary;
    private Vector2 startPositionSecondary;
    private Coroutine zoomCoroutine;

    private CinemachineVirtualCamera virtualCamera;

    public bool isInventoryOpen = false;
    private void Awake()
    {
        inputManager = InputManager.Instance;
        virtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        //Debug.Log(virtualCamera.name);
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
        if (isInventoryOpen) return;
        //Debug.Log("Start Zoom ");
        startPositionPrimary = positionPrimary;
        startPositionSecondary = positionSecondary;

        zoomCoroutine = StartCoroutine(DetectionZoom());
    }

    private void EndZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (isInventoryOpen) return;
        //Debug.Log("End Zoom ");

        StopCoroutine(zoomCoroutine);
    }

    IEnumerator DetectionZoom()
    {
        float previousDistance = Vector2.Distance(startPositionPrimary, startPositionSecondary), 
              distance = 0f;
        while (true)
        {
            Vector2 positionPrimary = inputManager.mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>();
            Vector2 positionSecondary = inputManager.mobileControls.Mobile.SecondaryPosition.ReadValue<Vector2>();
            distance = Vector2.Distance(positionPrimary, positionSecondary);
            //Debug.Log("Previous Distance = " + previousDistance);
            //Debug.Log("Distance " + distance);

            // Detection
            // Zoom Out
            if(distance > previousDistance + distanceTolerance)
            {
                //Debug.Log("Zoom Out");
                float fov = virtualCamera.m_Lens.OrthographicSize;
                float target = Mathf.Clamp(fov - zoomSpeed, zoomInMax, zoomOutMax);
                float ratio = target / zoomOutMax;
                virtualCamera.m_Lens.OrthographicSize = target;
            }
            // Zoom In
            else if(distance < previousDistance - distanceTolerance)
            {
                //Debug.Log("Zoom In");
                float fov = virtualCamera.m_Lens.OrthographicSize;
                float target = Mathf.Clamp(fov + zoomSpeed, zoomInMax, zoomOutMax);
                float ratio = target / zoomInMax;
                virtualCamera.m_Lens.OrthographicSize = target;
            }

            //Keep Track of Previous Distance
            previousDistance = distance;
            yield return null;
        }

    }

    public void InventoryTrue()
    {
        isInventoryOpen = true;
        Debug.Log("inventory open  " + isInventoryOpen);
    }

    public void InventoryFalse()
    {
        isInventoryOpen = false;
        Debug.Log("inventory open  " + isInventoryOpen);
    }
    public void ChangeIsInventoryOpen()
    {
        isInventoryOpen = !isInventoryOpen;
    }
}
