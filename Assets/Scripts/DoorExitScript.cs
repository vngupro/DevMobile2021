using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using TMPro;

public class DoorExitScript : MonoBehaviour
{

    #region Variable
    public CanvasExitDoorScript canvasExit;

    [Header("Animation")]
    public TMP_Text text;
    public SpriteRenderer arrow;
    public Color black;
    public Color grey;
    public float scaleFactor = 1.5f;

    [Header("Info")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;           //no conflict with slide detection

    [Header("Sound")]
    [SerializeField] private string soundPopUp;

    [Header("    Debug")]
    [SerializeField] private short count = 0;                                     // for double tap to exit scene
    [SerializeField] private bool isBlocked = false;

    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitDoor;
    private GameObject currentDoor;                                 // for counting

    private InputManager inputManager;

    #endregion

    private void Update()
    {
        if(inputManager == null)
        {
            // Bug input manager instance is not the good one
            inputManager = InputManager.Instance;
            inputManager = FindObjectOfType<InputManager>();
            // Listen To
            // TutoManager.cs
            UtilsEvent.blockMoveControls.AddListener(BlockControls);
            UtilsEvent.unlockMoveControls.AddListener(UnblockControls);

            inputManager.OnStartTouch += StartExitDoor;
            inputManager.OnEndTouch += EndExitDoor;

            Debug.Log(inputManager.name);
        }

    }
    private void BlockControls() { isBlocked = true; }
    private void UnblockControls() { isBlocked = false; }

    private void OnDisable()
    {
        
        if(inputManager != null)
        {
            inputManager.OnStartTouch -= StartExitDoor;
            inputManager.OnEndTouch -= EndExitDoor;
        }

    }

    private void StartExitDoor(Vector2 position, float time)
    {
        if (isBlocked) { return; }
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

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
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

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

            //Animation
            text.color = black;
            arrow.color = black;
            text.transform.localScale = new Vector2(scaleFactor, scaleFactor);

            // Display Pop up "Are you sure Exit ?"
            if (count >= 2)
            {
                canvasExit.background.SetActive(true);

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
                //Animation 
                text.color = grey;
                arrow.color = grey;
                text.transform.localScale = new Vector2(1, 1);
            }
                
        }
    }

   
}
