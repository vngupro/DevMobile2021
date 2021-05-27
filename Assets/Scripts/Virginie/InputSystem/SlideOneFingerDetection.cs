using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideOneFingerDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float distanceTolerance = 0.1f;                        //less conflict with zoom detection
    [SerializeField] private float cameraSpeed = 100.0f;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Coroutine coroutine;

    private float cameraInitialSpeed;
    private Vector2 startPos;
    private bool isDragging = false;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
        cameraInitialSpeed = cameraSpeed;

        // | Listeners 
        CustomGameEvents.dragEvent.AddListener(IsDraggingTrue);
    }
    private void OnEnable()
    {
        inputManager.OnStartTouchPrimary += StartSlide;
        inputManager.OnEndTouchPrimary += EndSlide;
        inputManager.OnStartTouchSecondary += InterruptSlide;
        inputManager.OnEndTouchSecondary += ReSlide;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouchPrimary -= StartSlide;
        inputManager.OnEndTouchPrimary -= EndSlide;
        inputManager.OnStartTouchSecondary -= InterruptSlide;
        inputManager.OnEndTouchSecondary -= ReSlide;
    }

    private void StartSlide(Vector2 position, float time)
    {
        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }
        if (isDragging) { return; }
        startPos = position;
        coroutine = StartCoroutine(DetectionSlide());
    }

    private void EndSlide(Vector2 position, float time)
    {
        if (isDragging)
        {
            isDragging = false;
            return;
        }

        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }

        StopCoroutine(coroutine);
    }

    private IEnumerator DetectionSlide()
    {
        //Vector2 startPosScreen = Camera.main.WorldToScreenPoint();
        startPos = inputManager.GetPrimaryScreenPosition();
        while (true)
        {
//            Vector2 positionPrimary = inputManager.GetPrimaryWorldPosition();
            Vector2 positionPrimary = inputManager.GetPrimaryScreenPosition();
            
            bool hasMovePrimary = Vector2.Distance(startPos, positionPrimary) > distanceTolerance;

            if (hasMovePrimary)
            {
                Vector3 direction = positionPrimary - startPos;
                Camera.main.transform.position -= direction.normalized * cameraSpeed * Time.deltaTime;

                //Keep Track of previous position
                startPos = positionPrimary;
            }
            yield return null;
        }
    }

    private void InterruptSlide(Vector2 primaryPosition, Vector2 secondaryPosition, float time)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void ReSlide(Vector2 primaryPosition, Vector2 secondaryPosition, float time)
    {
        StartSlide(primaryPosition, time);
    }

    private void IsDraggingTrue()
    {
        isDragging = true;
    }

    public void ChangeSlideSpeed(float speed)
    {
        float ratio = speed / 5f;
        cameraSpeed = cameraInitialSpeed * ratio;
    }
}
