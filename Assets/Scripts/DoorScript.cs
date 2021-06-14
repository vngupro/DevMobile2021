using UnityEngine;
using Cinemachine;
using TMPro;
public class DoorScript : MonoBehaviour
{
    [Tooltip("Virtual camera corresponding to the NEXT location")]
    public CinemachineVirtualCamera vcamOfNEXTLocation;

    [Header("Animation")]
    public TMP_Text text;
    public Color black;
    public Color grey;
    public Color lightGrey;
    public Color white;
    public SpriteRenderer arrow;
    public float scaleFactor = 1.5f;
    public Vector3 vcamStartPos { get; private set; }
    private void Awake()
    {
        vcamStartPos = new Vector3(
            vcamOfNEXTLocation.transform.position.x,
            vcamOfNEXTLocation.transform.position.y,
            -10);
    }

    public void ChangeToGrey()
    {
        text.color = grey;
        arrow.color = grey;
        text.transform.localScale = new Vector2(1, 1);
    }

    public void ChangeToBlack()
    {
        text.color = black;
        arrow.color = black;
        text.transform.localScale = new Vector2(scaleFactor, scaleFactor);
    }

    public void ChangeToLightGrey()
    {
        text.color = lightGrey;
        arrow.color = lightGrey;
    }
    public void ChangeToWhite()
    {
        text.color = white;
        arrow.color = white;
    }
}
