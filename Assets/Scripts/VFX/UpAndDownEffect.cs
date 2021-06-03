using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownEffect : MonoBehaviour
{
    [Header("Animation")]
    public float animationTime = 2.0f;
    public float distance = 10.0f;
    public AnimationCurve animCurve;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        StartCoroutine(SwingEffect());
    }

    private IEnumerator SwingEffect()
    {
        bool isReverse = false;
        float posDif = distance;
        while (true)
        {
            float timer = 0;
            while (timer < animationTime)
            {
                posDif = isReverse ? -distance : distance;
                timer += Time.deltaTime;
                float ratio = timer / animationTime;
                float newPos = Mathf.Lerp(-posDif, posDif, animCurve.Evaluate(ratio));
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x, 
                    startPos.y + newPos);

                yield return null;
            }

            isReverse = !isReverse;
        }
    }
}
