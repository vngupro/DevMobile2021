using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SlideDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float distanceTolerance = 0.1f;                        //less conflict with zoom detection
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;        //angle's difference acceptance for dot product
    [SerializeField] private float cameraSpeed = 1.0f;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Camera cam;
    private Coroutine coroutine;

    private Vector2 startPositionPrimary;
    private Vector2 startPositionSecondary;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
        cam = Camera.main;
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
        if (inventory.isOpen) return;

        startPositionPrimary = positionPrimary;
        startPositionSecondary = positionSecondary;
        coroutine = StartCoroutine(DetectionSlide());
    }

    private void EndSlide(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (inventory.isOpen) return;

        StopCoroutine(coroutine);
    }

    private IEnumerator DetectionSlide()
    {
        while (true)
        {
            Vector2 positionPrimary = inputManager.GetPrimaryWorldPosition();
            Vector2 positionSecondary = inputManager.GetSecondaryWorldPosition();
            bool hasMovePrimary = Vector2.Distance( startPositionPrimary, positionPrimary ) > distanceTolerance;
            bool hasMoveSecondary = Vector2.Distance( startPositionSecondary, positionSecondary ) > distanceTolerance;

            Debug.Log("Slide = " + hasMovePrimary + " " + hasMoveSecondary);
            if (hasMovePrimary && hasMoveSecondary)
            {
                Vector3 directionSecondary = positionSecondary - startPositionSecondary;
                Vector3 directionPrimary = positionPrimary - startPositionPrimary;
                Vector2 directionSecondary2D = new Vector2(directionSecondary.x, directionSecondary.y).normalized;
                Vector2 directionPrimary2D = new Vector2(directionPrimary.x, directionPrimary.y).normalized;
                float dotProduct = Vector2.Dot(directionPrimary2D, directionSecondary2D);

                // dot Product == 0 | Perpendicular
                // dot Product < 0  | inverse direction
                // dot Product > 0  | same direction
                if(dotProduct > 0)
                {
                    DirectionSlide(directionSecondary2D);
                }

                //Keep Track of previous position
                startPositionPrimary = positionPrimary;
                startPositionSecondary = positionSecondary;
            }

            yield return null;
        }
    }
    private void DirectionSlide(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - cameraSpeed, cam.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + cameraSpeed, cam.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x + cameraSpeed, cam.transform.position.y, cam.transform.position.z);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x - cameraSpeed, cam.transform.position.y, cam.transform.position.z);
        }
    }
}
