using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private LayerMask layer2PickUp;
    [SerializeField] private LayerMask layerUI;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;
    
    [Header("Animation")]
    [SerializeField] private PhotoEffect photoEffect;

    private InputManager inputManager;
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitItem;
    private Item currentItem;
    private GameObject currentItemGameObj;
    private HUDManager hudManager;



    #endregion
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartPickUp;
        inputManager.OnEndTouch += EndPickUp;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartPickUp;
        inputManager.OnEndTouch -= EndPickUp;
    }

    private void Start()
    {
        hudManager = HUDManager.Instance;
    }
    private void StartPickUp(Vector2 position, float time)
    {
        if(hudManager != null)
        {
            if (hudManager.IsLayerNotesOpen) { return; }
        }

        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Verify touch an object
        startPos = position;
        startTime = time;
        hitItem = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer2PickUp);
    }

    private void EndPickUp(Vector2 position, float time)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        endPos = position;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;

        if(hitItem &&
            distance <= distanceTolerance &&
                timer < timerBeforeHold)
        {
            currentItemGameObj = hitItem.transform.gameObject;
            currentItem = currentItemGameObj.GetComponent<Item>();

            if(
                currentItem.data.isClue &&
                currentItem.data.isPickable && 
                !currentItem.isHidden &&
                !currentItem.isBlocked)
            {
                PickUp(currentItemGameObj);
            }
        }
    }

    private void PickUp(GameObject object2PickUp)
    {
        photoEffect.PlayFlashEffect();
        CustomGameEvents.pickUpEvent.Invoke(object2PickUp);
        //destroy
        if (currentItem.data.isPickable)
        {
            object2PickUp.SetActive(false);
        }
        
    }

}
