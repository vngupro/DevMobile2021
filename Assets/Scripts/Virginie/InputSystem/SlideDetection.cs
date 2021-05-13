using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SlideDetection : MonoBehaviour
{
    [SerializeField] float distanceTolerance = 2.0f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;
    [SerializeField] private float cameraSpeed = 1.0f;

    private InputManager inputManager;
    private Camera mainCamera;
    private Vector2 startPositionPrimary;
    private Vector2 startPositionSecondary;
    private bool isInventoryOpen = false;
    private Coroutine coroutine;
    private void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouchSecondary += StartSlide;
        inputManager.OnEndTouchSecondary += EndSlide;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouchSecondary -= StartSlide;
        inputManager.OnEndTouchSecondary -= EndSlide;
    }

    private void StartSlide(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (isInventoryOpen) return;

        startPositionPrimary = positionPrimary;
        startPositionSecondary = positionSecondary;
        coroutine = StartCoroutine(DetectionSlide());

    }

    private void EndSlide(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (isInventoryOpen) return;

        StopCoroutine(coroutine);
    }

    private IEnumerator DetectionSlide()
    {
        while (true)
        {
            Vector2 positionPrimary = inputManager.mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>();
            Vector2 positionSecondary = inputManager.mobileControls.Mobile.SecondaryPosition.ReadValue<Vector2>();
            bool hasMovePrimary = Vector2.Distance( startPositionPrimary, positionPrimary ) > distanceTolerance;
            bool hasMoveSecondary = Vector2.Distance( startPositionSecondary, positionSecondary ) > distanceTolerance;
            Debug.Log("Dectection Slide " + hasMovePrimary + "  " + hasMoveSecondary);

            if (hasMovePrimary && hasMoveSecondary)
            {
                Vector3 direction = positionSecondary - startPositionSecondary;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                DirectionSlide(direction2D);
            }

            startPositionPrimary = inputManager.mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>();
            startPositionSecondary = inputManager.mobileControls.Mobile.SecondaryPosition.ReadValue<Vector2>();
            yield return null;
        }
    }
    private void DirectionSlide(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Up");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - cameraSpeed, mainCamera.transform.position.z);

        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Down");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + cameraSpeed, mainCamera.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Left");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + cameraSpeed, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Right");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - cameraSpeed, mainCamera.transform.position.y, mainCamera.transform.position.z);
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
}
