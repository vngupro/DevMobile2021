using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[DefaultExecutionOrder(-1)]
public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private Camera cameraItem;

    public List<CinemachineVirtualCamera> vcamList;

    public static CinemachineSwitcher Instance { get; protected set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        // Listen To
        CustomGameEvents.switchLocation.AddListener(SwitchPriority);
        CustomGameEvents.switchToSuspect.AddListener(SwitchToSuspect);
      
    }

    private void Start()
    {
        // Listeners | SlideOneFingerDetection.cs PinchDEtection.cs
        CustomGameEvents.switchCamera.Invoke(vcamList[0]);
    }

    private void SwitchPriority(DoorScript door)
    {
        cameraItem.orthographicSize = door.vcamOfNEXTLocation.m_Lens.OrthographicSize;

        ResetPriorities();
        door.vcamOfNEXTLocation.GetComponent<CinemachineConfiner>().InvalidatePathCache();
        door.vcamOfNEXTLocation.Priority = 1;
        door.vcamOfNEXTLocation.transform.position = door.vcamStartPos;
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x, 
            Camera.main.transform.position.y, 
            Camera.main.nearClipPlane);

        // OneSlideFinger.cs
        CustomGameEvents.switchCamera.Invoke(door.vcamOfNEXTLocation);

        
    }

    private void SwitchToSuspect(CinemachineVirtualCamera vcam)
    {
        ResetPriorities();
        vcam.GetComponent<CinemachineConfiner>().InvalidatePathCache();
        vcam.Priority = 1;
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            Camera.main.nearClipPlane);

        // OneSlideFinger.cs
        CustomGameEvents.switchCamera.Invoke(vcam);
    }

    private void ResetPriorities()
    {
        foreach(CinemachineVirtualCamera vcam in vcamList)
        {
            vcam.Priority = 0;
        }
    }
}
