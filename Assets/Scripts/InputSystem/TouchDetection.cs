using System.Collections;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    #region Event
    public delegate void TouchEvent();
    public event TouchEvent OnTouch;
    #endregion 

    #region Variable
    [Header("Animation")]
    [SerializeField] private GameObject circle;
    [SerializeField] private float animationTime = 0.2f;

    private InputManager inputManager;
    private bool hasTapMenuScreen = false;
    #endregion

    public static TouchDetection Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        inputManager = InputManager.Instance;

        // Listeners MenuManager
        CustomGameEvents.hasTapScreen.AddListener(ChangeHasTapMenuScreen);
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += DetectTouch;
        inputManager.OnStartTouch += TapScreen;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= DetectTouch;
    }
    public void DetectTouch(Vector2 position, float time)
    {
        OnTouch?.Invoke();

        //Animation
        if (circle != null)
        {
            StartCoroutine(CircleAnimation());
            circle.transform.position = position;
        }
    }

    private IEnumerator CircleAnimation()
    {
        circle.SetActive(true);
        yield return new WaitForSeconds(animationTime);
        circle.SetActive(false);
    }

    private void TapScreen(Vector2 position, float time)
    {
        if (!hasTapMenuScreen)
        {
            CustomGameEvents.hasPressAnyButtonEvent.Invoke();
        }
    }
    private void ChangeHasTapMenuScreen()
    {
        hasTapMenuScreen = true;
        inputManager.OnStartTouch -= TapScreen;
    }
}
