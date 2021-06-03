using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private LayerMask layer2Drag;
    [SerializeField] private float delayBeforeDrag = 0.2f;              //time before starting drag (less conflict with pick up)
    [SerializeField] private SlideOneFingerDetection slide;

    private InputManager inputManager;
    private Coroutine coroutine;
    private GameObject objectDraging;
    private RaycastHit2D hitDrag;

    #endregion

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
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if(HUDManager.Instance != null)
        {
            if (HUDManager.Instance.IsLayerNotesOpen) { return; }
        }


        //Verify touch an object
        hitDrag = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer2Drag);

        if (hitDrag && hitDrag.transform.gameObject.GetComponent<Item>().data.isDragable)
        {
            objectDraging = hitDrag.transform.gameObject;
            coroutine = StartCoroutine(Drag()); 

            if(slide != null)
            {
                slide.StopSlide();
            }

            CustomGameEvents.dragEvent.Invoke();
        }
    }

    private void EndDrag(Vector2 position, float time)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        if (EventSystem.current.IsPointerOverGameObject()) return;
    }
    
    private IEnumerator Drag()
    {
        float timer = delayBeforeDrag;

        while (true)
        {
            //Timer
            while(timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            //Change object's position
            objectDraging.transform.position = inputManager.GetPrimaryWorldPosition();

            yield return null;
        }
    }
}
