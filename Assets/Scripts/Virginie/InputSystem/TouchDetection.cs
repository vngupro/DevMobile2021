using System.Collections;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    #region Variable
    [Header("Animation")]
    [SerializeField] private GameObject circle;
    [SerializeField] private float animationTime = 0.2f;

    private InputManager inputManager;
    private InventoryManager inventory;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 position, float time)
    {
        if (inventory.isOpen) return;

        //Animation
        if(circle != null)
        {
            StartCoroutine(CircleAnimation());
            circle.transform.position = position;
        }
    }

    private IEnumerator CircleAnimation()
    {
        circle.SetActive(true);
        yield return new WaitForSeconds(animationTime);
        circle.SetActive(false);
    }
}
