using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class SwitchLocationDetection : MonoBehaviour
{
    #region Event
    public delegate void SwitchLocationEvent();
    public event SwitchLocationEvent OnSwitchLocation;

    #endregion
    #region Variable
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;

    [Header("Sound")]
    [SerializeField] private string switchLocationSound;

    [Header("Debug")]
    [SerializeField]
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitDoor;
    private GameObject currentDoor;

    private InputManager inputManager;
    private CanvasBlackscreen blackscreen;

    private short count = 0;            // for double tap to switch location
    private bool isBlocked = false;
    #endregion

    public static SwitchLocationDetection Instance { get; protected set; }
    private void Awake()
    {
        Instance = this;
        inputManager = InputManager.Instance;
        blackscreen = CanvasBlackscreen.Instance;

        //Listen To
        // TutoManager.cs
        UtilsEvent.blockMoveControls.AddListener(BlockControls);
        UtilsEvent.unlockMoveControls.AddListener(UnblockControls);
    }

    private void BlockControls() { isBlocked = true; }
    private void UnblockControls() { isBlocked = false; }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartDoor;
        inputManager.OnEndTouch += EndDoor;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartDoor;
        inputManager.OnEndTouch -= EndDoor;
    }

    private void StartDoor(Vector2 position, float time)
    {
        if(HUDManager.Instance != null)
        {
            if (HUDManager.Instance.IsLayerNotesOpen) { return; }
        }

        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        startPos = position;
        startTime = time;
        hitDoor = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer);


        if (hitDoor)
        {
            if (currentDoor != null)
            {
                if (currentDoor != hitDoor.transform.gameObject)
                {
                    count = 0;
                }
            }

            currentDoor = hitDoor.transform.gameObject;
        }
    }

    private void EndDoor(Vector2 position, float time)
    {
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        endPos = position;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;

        
        // If Click on a door which is not Exit
        if (hitDoor &&
             distance <= distanceTolerance &&
              timer < timerBeforeHold &&
               !currentDoor.CompareTag("Exit")
            )
        {
            count++;
            hitDoor.transform.gameObject.GetComponent<DoorScript>().ChangeToBlack();
            // Click Twice to change location
            if(count >= 2)
            {
                ChangeLocation();
            }
        }
        // If click anywhere else reset count
        else
        {
            count = 0;
            if(currentDoor != null)
            {
                currentDoor.transform.gameObject.GetComponent<DoorScript>().ChangeToGrey();
            }

        }
        
    }
    
    public void ChangeLocation()
    {
        blackscreen = CanvasBlackscreen.Instance;
        if (blackscreen != null)
        {
            StartCoroutine(SwitchLocation());
        }
        else
        {
            DoorScript door = currentDoor.GetComponent<DoorScript>();
            CustomGameEvents.switchLocation.Invoke(door);
        }
    }

    IEnumerator SwitchLocation()
    {
        OnSwitchLocation?.Invoke();

        blackscreen.FadeIn();
        yield return new WaitForSeconds(blackscreen.fadeDuration);
        DoorScript door = hitDoor.transform.gameObject.GetComponent<DoorScript>();
        CustomGameEvents.switchLocation.Invoke(door);
        yield return new WaitForSeconds(0);
        blackscreen.FadeOut();
    }
}
