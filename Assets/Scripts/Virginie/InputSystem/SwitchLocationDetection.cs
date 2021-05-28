using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLocationDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private LayerMask layer;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Coroutine coroutine;
    private GameObject door;
    private RaycastHit2D hitDoor;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
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

        hitDoor = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer);

        if (hitDoor)
        {
            coroutine = StartCoroutine(SwitchLocation(hitDoor.transform.gameObject));
        }
    }

    private void EndDoor(Vector2 position, float time)
    {
        if (inventory != null)
        {
            if (inventory.isOpen) return;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator SwitchLocation(GameObject door)
    {
        Debug.Log("Change Room");
        //Animation Fade Black Screen
        yield return null;
        DoorScript data = door.GetComponent<DoorScript>();
        CustomGameEvents.switchLocation.Invoke(data);
    }
}
