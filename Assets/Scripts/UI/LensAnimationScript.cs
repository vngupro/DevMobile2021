using System.Collections;
using UnityEngine;

public class LensAnimationScript : MonoBehaviour
{
    public Animator animator;
    public float animationTimer = 10.0f;

    [Header("Debug")]
    [SerializeField] private bool isPlaying = false;
    private void Update()
    {
        if (!isPlaying)
        {
            StartCoroutine(PlayAnimation());
        }
    }

    private IEnumerator PlayAnimation()
    {
        isPlaying = true;
        animationTimer = Random.Range(3.0f, 10.0f);
        yield return new WaitForSeconds(animationTimer);
        int rng = Random.Range(0, 9);

        switch (rng)
        {
            case 0: animator.SetBool("ShiftUp", true); break;
            case 1: animator.SetBool("ShiftDown", true); break;
            case 2: animator.SetBool("ShiftRight", true); break;
            case 3: animator.SetBool("ShiftLeft", true); break;
            case 4: animator.SetBool("ShiftUpRight", true); break;
            case 5: animator.SetBool("ShiftUpLeft", true); break;
            case 6: animator.SetBool("ShiftDownRight", true); break;
            case 7: animator.SetBool("ShiftDownLeft", true); break;
            case 8: animator.SetBool("Blinking", true); break;
            default: animator.SetBool("Blinking", true); break;
        }
    }
    public void StopBlinking()
    {
        animator.SetBool("Blinking", false);
        isPlaying = false;
    }

    public void StopShiftUp()
    {
        animator.SetBool("ShiftUp", false);
        isPlaying = false;
    }

    public void StopShiftDown()
    {
        animator.SetBool("ShiftDown", false);
        isPlaying = false;
    }

    public void StopShiftRight()
    {
        animator.SetBool("ShiftRight", false);
        isPlaying = false;
    }

    public void StopShiftLeft()
    {
        animator.SetBool("ShiftLeft", false);
        isPlaying = false;
    }

    public void StopShiftUpRight()
    {
        animator.SetBool("ShiftUpRight", false);
        isPlaying = false;
    }
    public void StopShiftUpLeft()
    {
        animator.SetBool("ShiftUpLeft", false);
        isPlaying = false;
    }

    public void StopShiftDownRight()
    {
        animator.SetBool("ShiftDownRight", false);
        isPlaying = false;
    }
    public void StopShiftDownLeft()
    {
        animator.SetBool("ShiftDownLeft", false);
        isPlaying = false;
    }
}
