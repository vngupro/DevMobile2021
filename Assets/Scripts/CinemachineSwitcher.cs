using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public List<CinemachineVirtualCamera> vcamList;

    public static CinemachineSwitcher Instance { get; protected set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        // Invoker 
        CustomGameEvents.switchLocation.AddListener(SwitchPriority);
    }

    private void Start()
    {
        // Listeners | SlideOneFingerDetection.cs PinchDEtection.cs
        CustomGameEvents.switchCamera.Invoke(vcamList[0]);
    }

    private void SwitchPriority(DoorScript door)
    {
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

    private void ResetPriorities()
    {
        foreach(CinemachineVirtualCamera vcam in vcamList)
        {
            vcam.Priority = 0;
        }
    }
}
