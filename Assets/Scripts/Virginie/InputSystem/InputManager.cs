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
    // | Events other scripts can fire
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnEndTouch;
    public delegate void StartTouchPrimaryEvent(Vector2 position, float time);
    public event StartTouchPrimaryEvent OnStartTouchPrimary;
    public delegate void EndTouchPrimaryEvent(Vector2 position, float time);
    public event EndTouchPrimaryEvent OnEndTouchPrimary;
    //

    private MobileControls mobileControls;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this) { 
            Destroy(this.gameObject); 
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        mobileControls = new MobileControls();
    }
    private void OnEnable()
    {
        mobileControls.Enable();
        TouchSimulation.Enable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        mobileControls.Disable();
        TouchSimulation.Disable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    private void Start()
    {
        mobileControls.Mobile.TouchPress.started += ctx => StartTouch(ctx);
        mobileControls.Mobile.TouchPress.canceled += ctx => EndTouch(ctx);
        mobileControls.Mobile.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        mobileControls.Mobile.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    //private void Update()
    //{
    //    Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);
    //    foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
    //    {
    //        Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
    //    }
    //}

    private void StartTouch(InputAction.CallbackContext context)
    {
        //the first is always 0, 0 why ????
        Debug.Log("Touch Start " + mobileControls.Mobile.TouchPosition.ReadValue<Vector2>());

        // | Invoke
        if (OnStartTouch != null)
        {
            OnStartTouch(
                mobileControls.Mobile.TouchPosition.ReadValue<Vector2>(),
                (float)context.startTime
            );
        }
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch End" + mobileControls.Mobile.TouchPosition.ReadValue<Vector2>());

        // | Invoke
        if (OnEndTouch != null)
        {
            OnEndTouch(
                mobileControls.Mobile.TouchPosition.ReadValue<Vector2>(),
                (float)context.time
            );
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("Start Touch Primary" + PrimaryPosition());

        // | Invoke
        if (OnStartTouchPrimary != null)
        {
            OnStartTouchPrimary(
                PrimaryPosition(),
                (float)context.startTime
            );
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("End Touch Primary" + PrimaryPosition());

        // | Invoke
        if (OnEndTouchPrimary != null)
        {
            OnEndTouchPrimary(
                PrimaryPosition(),
                (float)context.time
            );
        }
    }

    private void FingerDown(Finger finger)
    {
        Debug.Log("Finger Down " + finger.screenPosition);

        // | Invoke
        if (OnStartTouch != null)
        {
            OnStartTouch(
                finger.screenPosition,
                Time.time
            );
        }
    }

    public Vector2 PrimaryPosition()
    {
        Vector3 pos = mobileControls.Mobile.PrimaryPosition.ReadValue<Vector2>();
        pos.z = 0;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        return worldPos;
    }
}
