using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PanAndZoom : MonoBehaviour
{
    [SerializeField] private float panSpeed = 2f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomInMax = 40f;
    [SerializeField] private float zoomOutMax = 90f;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform camTransform;

    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        camTransform = virtualCamera.VirtualCameraGameObject.transform;
    }
    void Start()
    {

    }

    void Update()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);

        //if(x != 0 || y != 0)
        //{
        //    PanScreen(x, y);
        //}

        if(z != 0)
        {
            ZoomScreen(z);
        }
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;
        
        //up
        if(y >= Screen.height * 0.95f)
        {
            direction.y += 1;
        }//donw
        else if(y <= Screen.height * 0.05f)
        {
            direction.y -= 1;
        }

        //right
        if(x >= Screen.width * 0.95f)
        {
            direction.x += 1;
        }
        //left
        else if(x <= Screen.width * 0.05f)
        {
            direction.x -= 1;
        }
        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        camTransform.position = Vector3.Lerp(camTransform.position,
                                             camTransform.position + (Vector3)direction * panSpeed,
                                             Time.deltaTime);
    }

    public void ZoomScreen(float increment)
    {
        float fov = virtualCamera.m_Lens.OrthographicSize;
        float target = Mathf.Clamp(fov + increment, zoomInMax, zoomOutMax);
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
    }
}
