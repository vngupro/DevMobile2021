using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideOneFingerDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float distanceTolerance = 0.1f;                        //less conflict with zoom detection
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;        //angle's difference acceptance for dot product
    [SerializeField] private float cameraSpeed = 100.0f;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Camera cam;
    private Coroutine coroutine;

    private Vector2 startPos;
    private bool isDragging = false;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
        cam = Camera.main;

        // | Listeners 
        CustomGameEvents.dragEvent.AddListener(IsDraggingTrue);
    }
    private void OnEnable()
    {
        inputManager.OnStartTouchPrimary += StartSlide;
        inputManager.OnEndTouchPrimary += EndSlide;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouchPrimary -= StartSlide;
        inputManager.OnEndTouchPrimary -= EndSlide;
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
        while (true)
        {
            Vector2 positionPrimary = inputManager.GetPrimaryWorldPosition();
            Vector2 positionSecondary = inputManager.GetSecondaryWorldPosition();
            bool hasMovePrimary = Vector2.Distance(startPos, positionPrimary) > distanceTolerance;

            if (hasMovePrimary)
            {
                Vector3 direction = positionPrimary - startPos;
                Vector2 directionPrimary2D = new Vector2(direction.x, direction.y).normalized;
                float speed = Vector2.Distance(startPos, positionPrimary) * cameraSpeed * Time.deltaTime;
                DirectionSlide(directionPrimary2D, speed);

                //Keep Track of previous position
                startPos = positionPrimary;
            }

            yield return null;
        }
    }
    private void DirectionSlide(Vector2 direction, float speed)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - speed, cam.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + speed, cam.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x + speed, cam.transform.position.y, cam.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x - speed, cam.transform.position.y, cam.transform.position.z);
        }
    }

    private void IsDraggingTrue()
    {
        isDragging = true;
    }
}
