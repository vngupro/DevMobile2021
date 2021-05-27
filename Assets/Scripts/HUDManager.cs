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

    private bool isGroupLensOpen = false;
    //public Sprite spriteLensNormal, spriteLensUV, spriteLensXRay, spriteLensIR, spirteLensNightVision;
    //public Color colorLensNormal, colorLensUV, colorLensXRay, colorLensIR, colorLensNightVision;

    private List<Vector2> lensPosition = new List<Vector2>();
    private Vector2 buttonLensPosition;

    private void Awake()
    {
        buttonLensPosition = buttonLens.GetComponent<RectTransform>().anchoredPosition;
        foreach(RectTransform len in lens)
        {
            lensPosition.Add(len.anchoredPosition);
            len.anchoredPosition = buttonLensPosition;
        }
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
    }

    public void LensToogle()
    {
        Debug.Log("Toogle Lens");
        isGroupLensOpen = !isGroupLensOpen;
        StartCoroutine(LensToogleAnimation());
    }
    public void LensSelected(Image image)
    {
        buttonLens.image.color = image.color;
    }

    IEnumerator LensToogleAnimation()
    {
        bool isFinished = false;

        if (isGroupLensOpen)
        {
            groupLens.SetActive(isGroupLensOpen);
        }
        while (!isFinished)
        {
            int index = 0;
            foreach (RectTransform len in lens)
            {
                isFinished = true;


                if (isGroupLensOpen)
                {
                    Vector2 direction = lensPosition[index] - len.anchoredPosition;
                    len.anchoredPosition += direction.normalized;
                    if (Vector2.Distance(len.anchoredPosition, lensPosition[index]) > 0.1f)
                    {
                        isFinished = false;
                    }

                }
                else
                {
                    Vector2 direction = buttonLensPosition - len.anchoredPosition;
                    len.anchoredPosition += direction.normalized;
                    if (Vector2.Distance(len.anchoredPosition, buttonLensPosition) > 0.1f)
                    {
                        isFinished = false;
                    }
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
