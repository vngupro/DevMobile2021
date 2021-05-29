using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
public class SlideOneFingerDetection : MonoBehaviour
{
    #region Variable
    [SerializeField] private float distanceTolerance = 0.1f;                        //less conflict with zoom detection
    [SerializeField] private float cameraSpeed = 100.0f;

    [Header("Animation")]
    [SerializeField] private GameObject slideTrail;
    private InputManager inputManager;
    private Coroutine coroutine;
    private Coroutine trailCoroutine;
    private float cameraInitialSpeed;
    private Vector2 startPos;
    private bool isDragging = false;
    private CinemachineVirtualCamera vcam;
    private Collider2D boundary;
    private float cameraWidth;
    private float cameraHeight;
    #endregion

    private void Awake()
    {
        inputManager = InputManager.Instance;
        cameraInitialSpeed = cameraSpeed;

        //direct link on menu scene can work too
        vcam = CinemachineSwitcher.Instance.vcamList[0];
        boundary = vcam.GetComponent<CinemachineConfiner>().m_BoundingShape2D;
        cameraWidth = 2f * vcam.m_Lens.OrthographicSize;
        cameraHeight = vcam.m_Lens.OrthographicSize * Camera.main.aspect;

        if(slideTrail != null) slideTrail.SetActive(false);
        // | Listeners 
        CustomGameEvents.dragEvent.AddListener(IsDraggingTrue);
        // doorscripts.cs
        CustomGameEvents.switchCamera.AddListener(GetVirtualCamera);
        // levelmanager.Cs
        CustomGameEvents.sceneLoaded.AddListener(RecupVirtualCamera);
        
    }

    private void RecupVirtualCamera()
    {
        vcam = CinemachineSwitcher.Instance.vcamList[0];
        boundary = vcam.GetComponent<CinemachineConfiner>().m_BoundingShape2D;
    }
    private void GetVirtualCamera(CinemachineVirtualCamera _vcam)
    {
        vcam = _vcam;
        CinemachineConfiner cinemachineConfiner = vcam.GetComponent<CinemachineConfiner>();
        if(cinemachineConfiner != null) boundary = cinemachineConfiner.m_BoundingShape2D;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouchPrimary += StartSlide;
        inputManager.OnEndTouchPrimary += EndSlide;
        inputManager.OnStartTouchSecondary += InterruptSlide;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouchPrimary -= StartSlide;
        inputManager.OnEndTouchPrimary -= EndSlide;
        inputManager.OnStartTouchSecondary -= InterruptSlide;
    }

    private void StartSlide(Vector2 position, float time)
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }


        if (isDragging) { return; }
        startPos = position;
        coroutine = StartCoroutine(DetectionSlide());

        if (slideTrail != null)
        {
            slideTrail.transform.position = position;
            slideTrail.SetActive(true);
            trailCoroutine = StartCoroutine(Trail());

        }
    }

    private void EndSlide(Vector2 position, float time)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (isDragging)
        {
            isDragging = false;
            return;
        }

        //Animation
        if (slideTrail != null)
        {
            Debug.Log("Slide trail false");
            slideTrail.SetActive(false);
            StopCoroutine(trailCoroutine);
        }

        StopCoroutine(coroutine);


    }

    private IEnumerator DetectionSlide()
    {
        startPos = inputManager.GetPrimaryScreenPosition();
        while (true)
        {
            Vector2 positionPrimary = inputManager.GetPrimaryScreenPosition();
            
            bool hasMovePrimary = Vector2.Distance(startPos, positionPrimary) > distanceTolerance;

            if (hasMovePrimary)
            {
                Vector3 direction = positionPrimary - startPos;
                Vector3 nDirection = direction.normalized;
                
                Vector3 targetPosiion = vcam.transform.position - nDirection * cameraSpeed * Time.deltaTime;
                float newPosX = Mathf.Clamp(targetPosiion.x, boundary.bounds.min.x + cameraWidth/2, boundary.bounds.max.x - cameraWidth/2);
                float newPosY = Mathf.Clamp(targetPosiion.y, boundary.bounds.min.y + cameraHeight/2, boundary.bounds.max.y - cameraHeight/2);
                vcam.transform.position = new Vector3(newPosX, newPosY, -10);

                //Keep Track of previous position
                startPos = positionPrimary;
            }
            yield return null;
        }
    }
    private IEnumerator Trail()
    {
        while (true)
        {
            slideTrail.transform.position = inputManager.GetPrimaryWorldPosition();
            yield return null;
        }
    }

    private void InterruptSlide(Vector2 primaryPosition, Vector2 secondaryPosition, float time)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void IsDraggingTrue()
    {
        isDragging = true;
    }
    public void StopSlide()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    public void ChangeSlideSpeed(float speed)
    {
        float ratio = speed / 5f;
        cameraSpeed = cameraInitialSpeed * ratio;
    }
}
