using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
public class PinchDetection : MonoBehaviour
{
    #region Event
    public delegate void PinchEvent();
    public event PinchEvent OnPinch;

    #endregion
    #region Variable
    [SerializeField] private float defaultZoom = 5.0f;
    [SerializeField] private float zoomSpeed = 0.2f;
    [SerializeField] private float zoomInMax = 2f;
    [SerializeField] private float zoomOutMax = 5f;
    [SerializeField] private float distanceTolerance = 15.0f;

    [Header("Animation")]
    [SerializeField] private ZoomEffect zoomEffect;

    private InputManager inputManager;
    private Coroutine coroutine;

    private CinemachineVirtualCamera virtualCamera;
    private Camera cameraItem;

    [Header("   Debug")]
    [SerializeField] private bool isBlocked = false;
    private float zoomDefaultSpeed = 0.2f;
    #endregion

    public static PinchDetection Instance { get; protected set; }
    private void Awake()
    {
        Instance = this;
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
        inputManager.OnEndTouchPrimary += EndZoom2;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouchSecondary -= StartZoom;
        inputManager.OnEndTouchSecondary -= EndZoom;
        inputManager.OnEndTouchPrimary -= EndZoom2;
    }

    private void RecupVirtualCamera(CinemachineVirtualCamera vcam)
    {
        virtualCamera = vcam;
        virtualCamera.m_Lens.OrthographicSize = defaultZoom;

        GameObject cam = GameObject.FindGameObjectWithTag("CameraItem");
        if (cam == null) return;

        cameraItem = cam.GetComponent<Camera>();
        cameraItem.orthographicSize = defaultZoom;

        float ratio = defaultZoom / zoomOutMax;
        float newAmount = Mathf.Lerp(1, 0, ratio);
        zoomEffect.graduatedBar.fillAmount = newAmount;
        //zoomEffect.graduatedBar.fillAmount = defaultZoom / ((zoomOutMax + zoomInMax) - zoomInMax);
    }
    private void RecupCamera()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("CameraItem");
        if (cam == null) return;

        cameraItem = cam.GetComponent<Camera>();
        cameraItem.orthographicSize = defaultZoom;
    }

    private void StartZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (HUDManager.Instance != null)
        {
            if (HUDManager.Instance.IsLayerNotesOpen) { return; }
        }
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        //if (virtualCamera == null || cameraItem == null) { return; }

        coroutine = StartCoroutine(DetectionZoom());

        //Animation
        zoomEffect.ActivateCrosshair();

        OnPinch?.Invoke();
    }

    private void EndZoom(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        zoomEffect.DeactivateCrossHair();

        if (coroutine != null) StopCoroutine(coroutine);
    }

    private void EndZoom2(Vector2 positionPrimary, float time)
    {
        zoomEffect.DeactivateCrossHair();
        if (coroutine != null) StopCoroutine(coroutine);
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
            if (distance > previousDistance + distanceTolerance)
            {
                float newSize = virtualCamera.m_Lens.OrthographicSize - zoomSpeed;
                ChangeOrthographicSize(newSize);

                // Animation
                if (!zoomEffect.IsZoomingOut)
                {
                    zoomEffect.ZoomOutAnimation();
                }
            }
            // Zoom In
            else if(distance < previousDistance - distanceTolerance)
            {
                float newSize = virtualCamera.m_Lens.OrthographicSize + zoomSpeed;
                ChangeOrthographicSize(newSize);

                // Animation
                if (!zoomEffect.IsZoomingIn)
                {
                    zoomEffect.ZoomInAnimation();
                }
            }
            else
            {
                // Animation
                zoomEffect.StopAllAnimation();
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

        string newText = "x" + (target / defaultZoom).ToString("0.00");
        zoomEffect.zoomText.text = newText;

        
        //float newAmount = target / ((zoomOutMax + zoomInMax) - zoomInMax);
        float ratio = target / zoomOutMax;
        float newAmount = Mathf.Lerp(1, 0, ratio);
        zoomEffect.graduatedBar.fillAmount = newAmount;
    }

    public void ChangeZoomSpeed(float value, float max)
    {
        zoomSpeed = value * zoomDefaultSpeed / (max/2);
    }
}
