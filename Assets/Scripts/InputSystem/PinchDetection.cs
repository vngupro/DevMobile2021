using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
public class PinchDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float defaultZoom = 5.0f;
    [SerializeField] private float zoomSpeed = 0.2f;
    [SerializeField] private float zoomInMax = 2f;
    [SerializeField] private float zoomOutMax = 5f;
    [SerializeField] private float distanceTolerance = 15.0f;

    private InputManager inputManager;
    private Coroutine coroutine;

    private CinemachineVirtualCamera virtualCamera;
    private Camera cameraItem;

    private bool isBlocked = false;

    #endregion
    private void Awake()
    {
        inputManager = InputManager.Instance;

        // Listen To
        // LevelManager.cs
        CustomGameEvents.switchCamera.AddListener(RecupVirtualCamera);
        CustomGameEvents.sceneLoaded.AddListener(RecupCamera);
        // TutoManager.cs
        UtilsEvent.blockMoveControls.AddListener(BlockControls);
        UtilsEvent.unlockMoveControls.AddListener(UnblockControls);
    }
    
    private void BlockControls() { isBlocked = true; }
    private void UnblockControls() { isBlocked = false; }
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

    private void RecupVirtualCamera(CinemachineVirtualCamera vcam)
    {
        virtualCamera = vcam;
        virtualCamera.m_Lens.OrthographicSize = defaultZoom;
    }
    private void RecupCamera()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("CameraItem");
        if (cam == null) return;

        cameraItem = cam.GetComponent<Camera>();
        cameraItem.orthographicSize = defaultZoom;
        Debug.Log(cameraItem.name);
    }

    private void StartZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (virtualCamera == null || cameraItem == null) { return; }

        coroutine = StartCoroutine(DetectionZoom());
    }

    private void EndZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (virtualCamera == null || cameraItem == null) { return; }

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
                float newSize = virtualCamera.m_Lens.OrthographicSize - zoomSpeed;
                ChangeOrthographicSize(newSize);
            }
            // Zoom In
            else if(distance < previousDistance - distanceTolerance)
            {
                float newSize = virtualCamera.m_Lens.OrthographicSize + zoomSpeed;
                ChangeOrthographicSize(newSize);
            }

            //Keep Track of Previous Distance
            previousDistance = distance;

            yield return null;
        }
    }

    public void ChangeOrthographicSize(float newSize)
    {
        float target = Mathf.Clamp(newSize, zoomInMax, zoomOutMax);
        virtualCamera.m_Lens.OrthographicSize = target;
        cameraItem.orthographicSize = target;
    }
}