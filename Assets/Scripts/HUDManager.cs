using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HUDManager : MonoBehaviour
{
    public Button buttonNotes;
    public Button buttonLens;
    public Sprite spriteNotesOpen, spriteNotesClose;
    public List<RectTransform> lens;
    public GameObject groupLens;
    public GameObject layerNotes;
    [Header("Animation")]
    public List<float> lensAnimTime;

    private List<Vector2> lensPosition = new List<Vector2>();
    private Vector2 buttonLensPosition;

    [Header("Debug")]
    [SerializeField]
    private bool isGroupLensOpen = false;
    [SerializeField]
    private bool isLayerNotesOpen = false;

    private void Awake()
    {
        buttonLensPosition = buttonLens.GetComponent<RectTransform>().anchoredPosition;
        foreach(RectTransform len in lens)
        {
            lensPosition.Add(len.anchoredPosition);
            //Debug.Log(len.anchoredPosition);
            len.anchoredPosition = buttonLensPosition;
        }

        groupLens.SetActive(false);
        isGroupLensOpen = false;
        layerNotes.SetActive(false);
        isLayerNotesOpen = false;

    }
    public void NotesToogle()
    {
        if (buttonNotes.image.sprite.name == spriteNotesOpen.name)
        {
            buttonNotes.image.sprite = spriteNotesClose;
        }
        else
        {
            buttonNotes.image.sprite = spriteNotesOpen;
        }

        isLayerNotesOpen = !isLayerNotesOpen;
        layerNotes.SetActive(isLayerNotesOpen);
    }

    public void LensToogle()
    {
        //Debug.Log("Toogle Lens");
        isGroupLensOpen = !isGroupLensOpen;
        StartCoroutine(LensToogleAnimation());
    }
    public void LensSelected(Image image)
    {
        buttonLens.image.color = image.color;
    }

    IEnumerator LensToogleAnimation()
    {
        if (isGroupLensOpen)
        {
            groupLens.SetActive(isGroupLensOpen);
        }

        float timer = 0f;
        
        while (timer <= lensAnimTime[lensAnimTime.Count - 1])
        {
            int index = 0;

            foreach (RectTransform len in lens)
            {
                timer += Time.deltaTime;
                float ratio = timer / lensAnimTime[index];

                //Open
                if (isGroupLensOpen)
                {
                    len.anchoredPosition = Vector2.Lerp(buttonLensPosition, lensPosition[index], ratio);
                }
                //Close
                else
                {
                    len.anchoredPosition = Vector2.Lerp(lensPosition[index], buttonLensPosition, ratio);
                }
                index++;
            }

            yield return null;
        }

        if (!isGroupLensOpen)
        {
            groupLens.SetActive(isGroupLensOpen);
        }

    }
}
