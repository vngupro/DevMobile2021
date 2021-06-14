using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ProofBagEffect : MonoBehaviour
{
    public Image image_proof;
    public RectTransform rectTransform;
    public float animationDuration = 2.0f;
    public AnimationCurve curve;
    public float waitBeforePutAway = 2.0f;
    public float startPosY = -100f;
    public float endPosY = 50f;

    public void PlayAnimation(GameObject proof)
    {
        image_proof.sprite = proof.GetComponent<Item>().data.spriteInInventory;
        StartCoroutine(BagTranslation());
    }

    private IEnumerator BagTranslation()
    {
        float timer = 0;
        while(timer < animationDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / animationDuration;
            float newYPos = Mathf.Lerp(startPosY, endPosY, curve.Evaluate(ratio));
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                newYPos);
            yield return null;
        }

        yield return new WaitForSeconds(waitBeforePutAway);
        
        timer = 0;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / animationDuration;
            float newYPos = Mathf.Lerp(endPosY, startPosY, curve.Evaluate(ratio));
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                newYPos);
            yield return null;
        }

    }
}
