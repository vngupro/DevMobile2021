using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetection : MonoBehaviour
{
    #region Variable
    [Header("Animation")]
    [SerializeField] private GameObject circle;
    [SerializeField] private float animationTime = 0.2f;

    private InputManager inputManager;
    private bool hasTapMenuScreen = false;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;

        // Listeners MenuManager
        CustomGameEvents.hasTapScreen.AddListener(ChangeHasTapMenuScreen);
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
        inputManager.OnStartTouch += TapScreen;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }
    public void Move(Vector2 position, float time)
    {
        //Animation
        if (circle != null)
        {
            StartCoroutine(CircleAnimation());
            circle.transform.position = position;
            Debug.Log(inputManager.GetTouchScreenPosition());
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
