using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoEffect : MonoBehaviour
{
    public GameObject flash;
    public float flashDuration;
    public AnimationCurve curve;
    public AnimationCurve alphaCurve;
    public Vector2 minFlashScale = new Vector2(1f, 1f);
    public Vector2 maxFlashScale = new Vector2(20f, 10f);
    public float minAlpha = 0;
    public float maxAlpha = 1;

    [Header("Debug")]
    [SerializeField] private CanvasGroup flashAlpha;
    [SerializeField] private RectTransform flashRect;
    private void Awake()
    {
        flashAlpha = flash.GetComponent<CanvasGroup>();
        flashRect = flash.GetComponent<RectTransform>();
    }
    public void PlayFlashEffect()
    {
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        float timer = 0;


        while(timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float ratio = timer / (flashDuration / 2);
         
            flashAlpha.alpha = Mathf.Lerp(minAlpha, maxAlpha, alphaCurve.Evaluate(ratio));
            flashRect.localScale = Vector2.Lerp(minFlashScale, maxFlashScale, curve.Evaluate(ratio));
            yield return null;
        }

        timer = 0;
        while (timer < flashDuration / 2)
        {
            timer += Time.deltaTime;
            float ratio = timer / (flashDuration / 2);

            flashAlpha.alpha = Mathf.Lerp(maxAlpha, minAlpha, alphaCurve.Evaluate(ratio));
            flashRect.localScale = Vector2.Lerp(maxFlashScale, minFlashScale, curve.Evaluate(ratio));
            yield return null;
        }

        flashAlpha.alpha = 0;
        flashRect.localScale = minFlashScale;

    }
}
