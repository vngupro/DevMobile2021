using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PinchDetection : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float zoomInMax = 1.0f;
    [SerializeField] private float zoomOutMax = 10.0f;
    private InputManager inputManager;
    private Vector2 startPositionPrimary;
    private Vector2 endPositionPrimary;
    private Vector2 startPositionSecondary;
    private Vector2 endPositionSecondary;
    private Coroutine zoomCoroutine;

    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        virtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        Debug.Log(virtualCamera.name);
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
        Debug.Log("Start Zoom ");
        startPositionPrimary = positionPrimary;
        startPositionSecondary = positionSecondary;

        zoomCoroutine = StartCoroutine(DetectionZoom());
    }

    private void EndZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        Debug.Log("End Zoom ");
        endPositionPrimary = positionPrimary;
        endPositionSecondary = positionSecondary;

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
            if(distance > previousDistance)
            {
                Debug.Log("Zoom Out");
                float fov = virtualCamera.m_Lens.OrthographicSize;
                float target = Mathf.Clamp(fov - zoomSpeed, zoomInMax, zoomOutMax);
                float ratio = target / zoomOutMax;
                virtualCamera.m_Lens.OrthographicSize = target;
            }
            // Zoom In
            else if(distance < previousDistance)
            {
                Debug.Log("Zoom In");
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
}
