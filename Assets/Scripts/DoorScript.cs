using UnityEngine;
using Cinemachine;
using TMPro;
public class DoorScript : MonoBehaviour
{
    [Tooltip("Virtual camera corresponding to the NEXT location")]
    public CinemachineVirtualCamera vcamOfNEXTLocation;

    [Header("Animation")]
    public TMP_Text text;

    public Vector3 vcamStartPos { get; private set; }
    private void Awake()
    {
        vcamStartPos = new Vector3(
            vcamOfNEXTLocation.transform.position.x,
            vcamOfNEXTLocation.transform.position.y,
            -10);
    }
}
