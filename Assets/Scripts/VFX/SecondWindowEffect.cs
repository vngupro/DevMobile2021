using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SecondWindowEffect : MonoBehaviour
{
    public RectTransform panelItemInfo;
    public Image imageItem;
    public TMP_Text textItemName;
    public TMP_Text textItemDescription;

    public float animationDuration = 1.0f;
    public AnimationCurve curve;
    public float waitBeforeClose = 2.0f;
    public float startPosX = 100f;
    public float endPosX = -100f;

    public Queue<Item> items = new Queue<Item>();

    [Header("  Debug")]
    private bool isPlaying = false;

    private void Update()
    {
        if (items.Count > 0)
        {
            if (!isPlaying)
            {
                PlaySecondWindow(items.Dequeue());
            }
        }
    }

    public void AddToQueue(Item item)
    {
        items.Enqueue(item);
    }
    public void PlaySecondWindow(Item item)
    {
        imageItem.sprite = item.data.spriteOnLens;
        textItemName.text = item.data.name;
        textItemDescription.text = item.data.description;
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        isPlaying = true;
        float timer = 0;
        
        while(timer < animationDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / animationDuration;
            float newPos = Mathf.Lerp(startPosX, endPosX, curve.Evaluate(ratio));
            panelItemInfo.anchoredPosition = new Vector2(
                newPos,
                panelItemInfo.anchoredPosition.y);
            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeClose);

        timer = 0;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / animationDuration;
            float newPos = Mathf.Lerp(endPosX, startPosX, curve.Evaluate(ratio));
            panelItemInfo.anchoredPosition = new Vector2(
                newPos,
                panelItemInfo.anchoredPosition.y);
            yield return null;
        }

        isPlaying = false;
    }
}
