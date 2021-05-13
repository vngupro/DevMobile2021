using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDetection : MonoBehaviour
{
    [SerializeField] private LayerMask layer2Drag;
    [SerializeField] private float timerBeforeHold = 0.2f;
    private InputManager inputManager;
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    RaycastHit2D hasClickOnObject2Drag;
    private GameObject dragObject;
    private Coroutine dragCoroutine;
    public bool isInventoryOpen = false; 
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartDrag;
        inputManager.OnEndTouch += EndDrag;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartDrag;
        inputManager.OnEndTouch -= EndDrag;
    }

    private void StartDrag(Vector2 position, float time)
    {
        if (isInventoryOpen) return;
        //Debug.Log("Start Drag");

        Vector3 touchPos = Camera.main.ScreenToWorldPoint(position);
        touchPos.z = Camera.main.nearClipPlane;
        startPos = touchPos;
        startTime = time;
        hasClickOnObject2Drag = Physics2D.Raycast(touchPos, Vector3.forward, 20.0f, layer2Drag);
        if (hasClickOnObject2Drag)
        {
            dragObject = hasClickOnObject2Drag.transform.gameObject;
            dragCoroutine = StartCoroutine(Drag());
        }
    }

    private void EndDrag(Vector2 position, float time)
    {
        if (isInventoryOpen) return;
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(position);
        touchPos.z = Camera.main.nearClipPlane;
        endPos = touchPos;
        endTime = time;

        if (dragCoroutine != null)
        {
            StopCoroutine(dragCoroutine);
        }
 

        //Debug.Log("End Drag");
    }
    
    IEnumerator Drag()
    {
        float timer = timerBeforeHold;

        while (true)
        {
            while(timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            Vector3 newPos = Camera.main.ScreenToWorldPoint(inputManager.mobileControls.Mobile.TouchPosition.ReadValue<Vector2>());
            newPos.z = Camera.main.nearClipPlane;
            dragObject.transform.position = newPos;
            yield return null;
        }
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
    }
}
