using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [Header("Animation")]
    public bool upDown = false;
    public bool leftRight = false;
    public float animationTime = 1.0f;
    public float distance = 10.0f;
    public AnimationCurve animCurve;
 
    private Vector2 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Start()
    {
        StartCoroutine(ArrowSwing());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ArrowSwing()
    {
  
        Vector2 endPos = new Vector2(0, 0);

        if (upDown)
        {
            endPos = new Vector2(startPos.x, startPos.y + distance);
        }
        else if (leftRight)
        {
            endPos = new Vector2(startPos.x + distance, startPos.y);
        }

        bool isReverse = false;

        while (true)
        {
            float timer = 0;
            Vector2 min, max;
            while (timer < animationTime)
            {
                if (isReverse)
                {
                    min = startPos;
                    max = endPos;
                }
                else
                {
                    min = endPos;
                    max = startPos;
                }

                timer += Time.deltaTime;
                float ratio = timer / animationTime;
                Vector2 newPos = Vector2.Lerp(min, max, animCurve.Evaluate(ratio));
                transform.position = newPos;

                yield return null;
            }

            isReverse = !isReverse;
        }
    }
}
