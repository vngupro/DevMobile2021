using System.Collections;
using UnityEngine;
using Cinemachine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minDistance2Swipe = 2f;                        //minmum distance the player has to make on screen to do a swipe
    [SerializeField] private float maxTimeSwipe = 1.0f;                             //maximum time the player can hold before it's consider a hold & drag instead of swipe
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;        //Dot product direction
    [SerializeField] private GameObject trail;
    [SerializeField] private float cameraSpeed = 1.0f;

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine trailCoroutine;
    private bool hasInterruptSwipe = false;

    private Camera mainCamera;

    private bool isInventoryOpen = false;


    private void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;
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
        if (isInventoryOpen) return;
        //Debug.Log("Start Swipe");
        startPosition = position;
        startTime = time;

        trail.SetActive(true);
        trail.transform.position = position;
        trailCoroutine = StartCoroutine(Trail());
    }

    private void EndSwipe(Vector2 position, float time)
    {
        if (isInventoryOpen) return;
        trail.SetActive(false);
        StopCoroutine(trailCoroutine);
        endPosition = position;
        endTime = time;

        if(!hasInterruptSwipe) DetectSwipe();

        hasInterruptSwipe = false;

        //Debug.Log("End Swipe" + hasInterruptSwipe);
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition();
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
            //Debug.Log("Detect Swipe");
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
            //Debug.Log("Swipe Up");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - cameraSpeed, mainCamera.transform.position.z);

        }
        else if(Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Down");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + cameraSpeed, mainCamera.transform.position.z);
        }
        else if(Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Left");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + cameraSpeed, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        else if(Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            //Debug.Log("Swipe Right");
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - cameraSpeed, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    private void InterruptSwipe(Vector2 positionPrimary, Vector2 positionSecondary, float time)
    {
        if (isInventoryOpen) return;
        //Debug.Log("Interrupt Swipe");
        hasInterruptSwipe = true;
        trail.SetActive(false);
    }

    public void ChangeIsInventoryOpen()
    {
        isInventoryOpen = !isInventoryOpen;
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
