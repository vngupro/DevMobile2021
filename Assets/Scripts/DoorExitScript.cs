using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using TMPro;

public class DoorExitScript : MonoBehaviour
{

    #region Variable
    public CanvasExitDoorScript canvasExit;
    public CinemachineVirtualCamera vcamOfNEXTLocation;

    [Header("Animation")]
    public TMP_Text text;
    public SpriteRenderer arrow;
    public Color black;
    public Color grey;

    [Header("Info")]
    public string sceneToLoad;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;           //no conflict with slide detection

    [Header("Sound")]
    [SerializeField] private string soundPopUp;

    [Header("Debug")]
    [SerializeField]
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitDoor;
    private GameObject currentDoor;                                 // for counting

    private InputManager inputManager;

    private short count = 0;                                     // for double tap to exit scene
    private bool isBlocked = false;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;

        // Listen To
        // TutoManager.cs
        UtilsEvent.blockMoveControls.AddListener(BlockControls);
        UtilsEvent.unlockMoveControls.AddListener(UnblockControls);
    }
    private void BlockControls() { isBlocked = true; }
    private void UnblockControls() { isBlocked = false; }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartExitDoor;
        inputManager.OnEndTouch += EndExitDoor;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartExitDoor;
        inputManager.OnEndTouch -= EndExitDoor;
    }

    private void StartExitDoor(Vector2 position, float time)
    {
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) return;

        startPos = position;
        startTime = time;
        hitDoor = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer);


        if (hitDoor && hitDoor.transform.gameObject.CompareTag("Exit"))
        {
            if (currentDoor != null)
            {
                //If is another exit door
                if (currentDoor != hitDoor.transform.gameObject)
                {
                    count = 0;
                }
            }

            currentDoor = hitDoor.transform.gameObject;
        }
    }

    private void EndExitDoor(Vector2 position, float time)
    {
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) return;

        endPos = position;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;

        // if hit door exit
        if (hitDoor &&
             distance <= distanceTolerance &&
              timer < timerBeforeHold &&
               currentDoor.CompareTag("Exit")
            )
        {
            count++;

            text.color = black;
            arrow.color = black;
            // Display Pop up "Are you sure Exit ?"
            if (count >= 2)
            {
                canvasExit.background.SetActive(true);
                canvasExit.sceneToLoad = sceneToLoad;

                // Sound
                if(SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlaySound(soundPopUp);
                }
            }
        }
        // if hit another door
        else
        {
            count = 0;
            if(currentDoor != null)
            {
                text.color = grey;
                arrow.color = grey;
            }
                
        }
    }

   
}
