using UnityEngine;
using Cinemachine;
public class DoorScript : MonoBehaviour
{
    [Tooltip("Virtual camera corresponding to the NEXT location")]
    public CinemachineVirtualCamera vcam;

    public Vector3 vcamStartPos { get; private set; }
    private void Awake()
    {
        vcamStartPos = new Vector3(
            vcam.transform.position.x,
            vcam.transform.position.y,
            -10);
    }
}
