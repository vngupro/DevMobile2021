using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    #region Variable
    [SerializeField] private LayerMask layer2Drag;

    private InputManager inputManager;
    private InventoryManager inventory;
    private Coroutine coroutine;
    private GameObject objectDraging;
    private SwipeDetection swipe;
    private RaycastHit2D hitDrag;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
    }
}
