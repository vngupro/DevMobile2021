using System.Collections;
using UnityEngine;
using Cinemachine;

public class SwipeDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float minDistance2Swipe = 2f;                          //minmum distance the player has to make on screen to do a swipe
    [SerializeField] private float maxTimeSwipe = 1.0f;                             //maximum time the player can hold before it's consider a hold & drag instead of swipe
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;        //Dot product direction acceptance
    [SerializeField] private float cameraSpeed = 1.0f;

    [Header("Optional")]
    [SerializeField] private GameObject trail;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Camera cam;
    private Coroutine trailCoroutine;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private bool hasInterruptSwipe = false;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
        cam = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouchPrimary += StartSwipe;
        inputManager.OnEndTouchPrimary += EndSwipe;
        inputManager.OnStartTouchSecondary += InterruptSwipe;
   }

    private void OnDisable()
    {
        inputManager.OnStartTouchPrimary -= StartSwipe;
        inputManager.OnEndTouchPrimary -= EndSwipe;
        inputManager.OnStartTouchSecondary -= InterruptSwipe;
    }

    private void StartSwipe(Vector2 position, float time)
    {
        if (inventory.isOpen) return;

        startPosition = position;
        startTime = time;

        if(trail != null)
        {
            trail.SetActive(true);
            trail.transform.position = position;
            trailCoroutine = StartCoroutine(Trail());
        }
    }

    private void EndSwipe(Vector2 position, float time)
    {
        if (inventory.isOpen) return;

        endPosition = position;
        endTime = time;
        
        //Animation
        if(trail != null)
        {
            trail.SetActive(false);
            StopCoroutine(trailCoroutine);
        }

        if(!hasInterruptSwipe) DetectSwipe();

        hasInterruptSwipe = false;
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.GetPrimaryWorldPosition();
            yield return null;
        }
    }

    private void DetectSwipe()
    {
        float distance = Vector3.Distance(startPosition, endPosition);
        float timer = endTime - startTime;
        if (distance >= minDistance2Swipe &&
            timer <= maxTimeSwipe)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 5.0f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            DirectionSwipe(direction2D);
        }
    }

    private void DirectionSwipe(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - cameraSpeed, cam.transform.position.z);
        }
        else if(Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + cameraSpeed, cam.transform.position.z);
        }
        else if(Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x + cameraSpeed, cam.transform.position.y, cam.transform.position.z);
        }
        else if(Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            cam.transform.position = new Vector3(cam.transform.position.x - cameraSpeed, cam.transform.position.y, cam.transform.position.z);
        }
    }

    private void InterruptSwipe(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        StopSwipe();
    }

    public void StopSwipe()
    {
        if (inventory.isOpen) return;

        hasInterruptSwipe = true;

        if (trail != null)
        {
            trail.SetActive(false);
        }
    }
}
