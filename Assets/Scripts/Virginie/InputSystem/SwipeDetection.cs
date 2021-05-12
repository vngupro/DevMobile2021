using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minDistance2Swipe = 0.2f;                        //minmum distance the player has to make on screen to do a swipe
    [SerializeField] private float maxTimeSwipe = 1.0f;                             //maximum time the player can hold before it's consider a hold & drag instead of swipe
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;        //Dot product direction
    [SerializeField] private GameObject trail;

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine trailCoroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouchPrimary += StartSwipe;
        inputManager.OnEndTouchPrimary += EndSwipe;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouchPrimary -= StartSwipe;
        inputManager.OnEndTouchPrimary -= EndSwipe;
    }

    private void StartSwipe(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;

        trail.SetActive(true);
        trail.transform.position = position;
        trailCoroutine = StartCoroutine(Trail());
    }

    private void EndSwipe(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(trailCoroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
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
        if(Vector3.Distance(startPosition, endPosition) >= minDistance2Swipe &&
            (endTime - startTime) <= maxTimeSwipe)
        {
            Debug.Log("Detect Swipe");
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
            Debug.Log("Swipe Up");
        }
        else if(Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if(Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if(Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe Right");
        }
    }
}
