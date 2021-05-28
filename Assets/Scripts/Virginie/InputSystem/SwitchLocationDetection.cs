using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLocationDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;

    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitDoor;

    private InputManager inputManager;
    private InventoryManager inventory;
    private BlackScreenScript blackscreen;

    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
        blackscreen = BlackScreenScript.Instance;
    }

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
        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }

        startPos = position;
        startTime = time;
        hitDoor = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer);
    }

    private void EndDoor(Vector2 position, float time)
    {
        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }

        endPos = position;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;
        if (distance <= distanceTolerance &&
            hitDoor &&
            timer < timerBeforeHold
            )
        {
            StartCoroutine(SwitchLocation());
        }
    }
    
    IEnumerator SwitchLocation()
    {
        blackscreen.FadeIn();
        yield return new WaitForSeconds(blackscreen.fadeDuration);
        DoorScript door = hitDoor.transform.gameObject.GetComponent<DoorScript>();
        CustomGameEvents.switchLocation.Invoke(door);
        yield return new WaitForSeconds(0);
        blackscreen.FadeOut();
    }
}