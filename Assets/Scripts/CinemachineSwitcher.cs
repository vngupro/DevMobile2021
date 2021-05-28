using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField]
    private List<CinemachineVirtualCamera> vcamList;

    private void Awake()
    {
        // Invoker 
        CustomGameEvents.switchLocation.AddListener(SwitchPriority);
    }

    private void Start()
    {
        // Listeners | SlideOneFingerDetection.cs
        CustomGameEvents.switchCamera.Invoke(vcamList[0]);
    }
    private void SwitchPriority(DoorScript door)
    {
        ResetPriorities();
        door.vcam.GetComponent<CinemachineConfiner>().InvalidatePathCache();
        door.vcam.Priority = 1;
        door.vcam.transform.position = door.vcamStartPos;
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x, 
            Camera.main.transform.position.y, 
            Camera.main.nearClipPlane);

        // OneSlideFinger.cs
        CustomGameEvents.switchCamera.Invoke(door.vcam);
    }

    private void ResetPriorities()
    {
        foreach(CinemachineVirtualCamera vcam in vcamList)
        {
            vcam.Priority = 0;
        }
    }
}
