using System.Collections;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private float animationTime = 0.2f;
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
            StartCoroutine(CircleAnimation());
            circle.transform.position = worldCoordinates;
        }

    }

    private IEnumerator CircleAnimation()
    {
        circle.SetActive(true);
        yield return new WaitForSeconds(animationTime);
        circle.SetActive(false);
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
