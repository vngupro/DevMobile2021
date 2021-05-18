using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Problem 
 *    RaycastHit2D just hit one layer for now,
 *    but you will want to hit at least 2 layer
 *    if the clue is in a hiding place
 *    or you can do a bool isHidden
 *    to make it impossible to take if isHidden
 * */
public class PickUpDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private LayerMask layer2PickUp;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitClue;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
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

    private void StartPickUp(Vector2 position, float time)
    {
        if (inventory.isOpen) return;
        
        // Verify touch an object
        startPos = position;
        startTime = time;
        hitClue = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer2PickUp);
    }

    private void EndPickUp(Vector2 position, float time)
    {
        if (inventory.isOpen) return;

        endPos = position;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;
        if (distance <= distanceTolerance && hitClue && timer < timerBeforeHold && hitClue.transform.gameObject.tag == "Clue")
        {
            PickUp(hitClue.transform.gameObject);
        }
    }

    private void PickUp(GameObject object2PickUp)
    {
        //here add verify bool isHidden
        object2PickUp.SetActive(false);
        CustomGameEvents.pickUpEvent.Invoke(object2PickUp);
        //Destroy(object2PickUp);
    }
}
