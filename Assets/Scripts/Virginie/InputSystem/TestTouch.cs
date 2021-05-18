using UnityEngine;

public class TestTouch : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    private InputManager inputManager;
    private Camera cam;
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
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, 0f);
        Debug.Log(screenCoordinates);
        Vector3 worldCoordinates = cam.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        transform.position = worldCoordinates;
    }
}
