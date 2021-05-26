using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensAnimationScript : MonoBehaviour
{
    public Animator animator;
    public float animationTimer = 10.0f;

    [Header("Debug")]
    [SerializeField]
    private float timer = 0.0f;
    private void Update()
    {
        
        if(timer < animationTimer)
        {
            timer += Time.deltaTime;
            return;
        }


        timer = 0;
        animator.SetBool("Blinking", true);
    }

    public void StopBlinking()
    {
        animator.SetBool("Blinking", false);
    }
}
