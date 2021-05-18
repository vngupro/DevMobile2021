using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

/* 
 * BUG REPORT -> 
 *     the first touch is always (0,0) position 
 *     not sure why, I think it's just the input waking up
 *     
 * On PC : 
 * you can test with Mouse Control
 * Window > Analysis > Input Debugger 
 * Select Options > Simulate Touch Input From Mouse or Pen
 * 
 * On Mobile :
 *  doesn't work with Unity Remote 
 *  need to build and run
*/

//Execute in priority
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    #region Event
    // | Events other scripts can fire
    // | Listeners : DragDetection.cs PickUpDetection.cs PinchDetection.cs SwipeDetection.cs TouchDetection.cs, 
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnEndTouch;
    public delegate void StartTouchPrimaryEvent(Vector2 position, float time);
    public event StartTouchPrimaryEvent OnStartTouchPrimary;
    public delegate void EndTouchPrimaryEvent(Vector2 position, float time);
    public event EndTouchPrimaryEvent OnEndTouchPrimary;
    public delegate void StartTouchSecondaryEvent(Vector2 positionPrimary, Vector2 positionSecondary, float time);
    public event StartTouchSecondaryEvent OnStartTouchSecondary;
    public delegate void EndTouchSecondaryEvent(Vector2 positionPrimary, Vector2 positionSecondary, float time);
    public event EndTouchSecondaryEvent OnEndTouchSecondary;
    #endregion
    #region Property
    protected MobileControls mobileControls;
    public Camera cam { get; private set; }
    public static InputManager Instance { get; private set; }
    #endregion

    private void Awake()
    {
        if(Instance != null && Instance != this) { 
            Destroy(this.gameObject); 
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        mobileControls = new MobileControls();
        cam = Camera.main;
    }
    private void OnEnable()
    {
        mobileControls.Enable();

        ////Enchanced Touch Simulation
        //TouchSimulation.Enable();
       // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        mobileControls.Disable();

        ////Enchanced Touch Simulation
        //TouchSimulation.Disable();
        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    private void Start()
    {
        mobileControls.Mobile.TouchPress.started += ctx => StartTouch(ctx);
        mobileControls.Mobile.TouchPress.canceled += ctx => EndTouch(ctx);
        mobileControls.Mobile.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        mobileControls.Mobile.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        mobileControls.Mobile.SecondaryContact.started += ctx => StartTouchSecondary(ctx);
        mobileControls.Mobile.SecondaryContact.canceled += ctx => EndTouchSecondary(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        //the first is always 0, 0 why ????
        //Debug.Log("Touch Start " + mobileControls.Mobile.TouchPosition.ReadValue<Vector2>());

        if (OnStartTouch != null)
        {
            OnStartTouch(
                GetWorldPosition(mobileControls.Mobile.TouchPosition.ReadValue<Vector2>()),
                (float)context.startTime
            );
        }
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(
                GetWorldPosition(mobileControls.Mobile.TouchPosition.ReadValue<Vector2>()),
                (float)context.time
            );
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouchPrimary != null)
        {
            OnStartTouchPrimary(
                GetWorldPosition(mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>()),
                (float)context.startTime
            );
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouchPrimary != null)
        {
            OnEndTouchPrimary(
                GetWorldPosition(mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>()),
                (float)context.time
            );
        }
    }

    private void StartTouchSecondary(InputAction.CallbackContext context)
    {
        if (OnStartTouchSecondary != null)
        {
            OnStartTouchSecondary(
                GetWorldPosition(mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>()),
                GetWorldPosition(mobileControls.Mobile.SecondaryPosition.ReadValue<Vector2>()),
                (float)context.startTime
            );
        }
    }

    private void EndTouchSecondary(InputAction.CallbackContext context)
    {
        if (OnEndTouchSecondary != null)
        {
            OnEndTouchSecondary(
                GetWorldPosition(mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>()),
                GetWorldPosition(mobileControls.Mobile.SecondaryPosition.ReadValue<Vector2>()),
                (float)context.time
            );
        }
    }

    private Vector3 GetWorldPosition(Vector2 position)
    {
        return Utils.ScreenToWorld(cam, position);
    }

    public Vector3 GetPrimaryWorldPosition()
    {
        return GetWorldPosition(mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>());
    }
    public Vector3 GetSecondaryWorldPosition()
    {
        return GetWorldPosition(mobileControls.Mobile.SecondaryPosition.ReadValue<Vector2>());
    }

    //Testing Enchancing Touch
    //private void Update()
    //{
    //    Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);
    //    foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
    //    {
    //        Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
    //    }
    //}

    //private void FingerDown(Finger finger)
    //{
    //    //Debug.Log("Finger Down " + finger.screenPosition);

    //    // | Invoke
    //    if (OnStartTouch != null)
    //    {
    //        OnStartTouch(
    //            finger.screenPosition,
    //            Time.time
    //        );
    //    }
    //}
}
