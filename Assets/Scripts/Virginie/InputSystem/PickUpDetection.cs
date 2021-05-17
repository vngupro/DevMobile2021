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
    [SerializeField] private LayerMask layer2PickUp;
    [SerializeField] private float distanceTolerance = 0.5f;
    [SerializeField] private float timerBeforeHold = 1.0f;
    private InputManager inputManager;
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    RaycastHit2D hitClue;

    public bool isInventoryOpen = false;
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

    private void StartPickUp(Vector2 position, float time)
    {
        if (isInventoryOpen) return;
        //Debug.Log("Start Pick Up");

        Vector3 touchPos = Camera.main.ScreenToWorldPoint(position);
        touchPos.z = Camera.main.nearClipPlane;
        startPos = touchPos;
        startTime = time;
        hitClue = Physics2D.Raycast(touchPos, Vector3.forward, 20.0f, layer2PickUp);
    }

    private void EndPickUp(Vector2 position, float time)
    {
        if (isInventoryOpen) return;
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(position);
        touchPos.z = Camera.main.nearClipPlane;
        endPos = touchPos;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;
        if (distance <= distanceTolerance && hitClue && timer < timerBeforeHold && hitClue.transform.gameObject.tag == "Clue")
        {
            PickUp(hitClue.transform.gameObject);
        }

        //Debug.Log("End Pick Up");
    }

    private void PickUp(GameObject object2PickUp)
    {
        //here add verify bool isHidden
        //Debug.Log("Pick Up " + object2PickUp.name);
        //object2PickUp.SetActive(false);
        Destroy(object2PickUp);
    }

    public void InventoryTrue()
    {
        isInventoryOpen = true;
        Debug.Log("inventory open  " + isInventoryOpen);
    }

    public void InventoryFalse()
    {
        isInventoryOpen = false;
        Debug.Log("inventory open  " + isInventoryOpen);
    }
    public void ChangeIsInventoryOpen()
    {
        isInventoryOpen = !isInventoryOpen;
        Debug.Log("inventory open  " + isInventoryOpen);
    }
}
