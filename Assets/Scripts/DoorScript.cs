using UnityEngine;
using Cinemachine;
public class DoorScript : MonoBehaviour
{
    [Tooltip("Virtual camera corresponding to the NEXT location")]
    public CinemachineVirtualCamera vcamOfNEXTLocation;

    public Vector3 vcamStartPos { get; private set; }
    private void Awake()
    {
        vcamStartPos = new Vector3(
            vcamOfNEXTLocation.transform.position.x,
            vcamOfNEXTLocation.transform.position.y,
            -10);
    }
}
