using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    private InputManager inputManager;
    private Camera cam;

    public bool isInventoryOpen = false;
    private void Awake()
    {
        inputManager = InputManager.Instance;
        cam = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPosition, float time)
    {
        if (isInventoryOpen) return;
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, 0f);
        //Debug.Log("Move " + screenCoordinates);
        Vector3 worldCoordinates = cam.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;

        if(circle != null)
        {
            circle.transform.position = worldCoordinates;
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
