using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("Autopsy")]
    public TMP_Text autopsyTitle;
    public TMP_Text autopsyCorps;

    [Header("Case Info")]
    public TMP_Text caseTitle;
    public TMP_Text caseCorps;

    [Header("Debug")]
    [SerializeField]
    private bool isGroupLensOpening = false;
    [SerializeField]
    private bool isLayerNotesOpening = false;
    [SerializeField]
    private bool isAnimationFinished = true;

    private List<Vector2> lensPosition = new List<Vector2>();
    private Vector2 buttonLensPosition;

    private Autopsy autopsy;
    private Case caseInfo;

    private int caseIndex = 1;
    private void Awake()
    {
        buttonLensPosition = buttonLens.GetComponent<RectTransform>().anchoredPosition;
        foreach(RectTransform len in lens)
        {
            lensPosition.Add(len.anchoredPosition);
            len.anchoredPosition = buttonLensPosition;
        }

        groupLens.SetActive(false);
        isGroupLensOpening = false;
        layerNotes.SetActive(false);
        isLayerNotesOpening = false;
        isAnimationFinished = true;

        string autopsyPath = "Autopsy/Autopsy " + caseIndex.ToString();
        autopsy = (Autopsy)Resources.Load(autopsyPath);
        string casePath = "Case/Case " + caseIndex.ToString();
        caseInfo = (Case)Resources.Load(casePath);

        autopsyTitle.text = autopsy.title;
        autopsyCorps.text = autopsy.corps;
        caseTitle.text = caseInfo.title;
        caseCorps.text = caseInfo.corps;
    }
    public void NotesToogle()
    {
        if (buttonNotes.image.sprite.name == spriteNotesOpen.name)
        {
            buttonNotes.image.sprite = spriteNotesClose;
        }
        else if (buttonNotes.image.sprite.name == spriteNotesClose.name)
        {
            buttonNotes.image.sprite = spriteNotesOpen;
        }

        isLayerNotesOpening = !isLayerNotesOpening;
        layerNotes.SetActive(isLayerNotesOpening);
    }

    public void LensToogle()
    {
        isGroupLensOpening = !isGroupLensOpening;

        if (isAnimationFinished)
        {
            StartCoroutine(LensToogleAnimation());
        }
    }

    public void LensSelected(Image image)
    {
        buttonLens.image.color = image.color;
    }

    IEnumerator LensToogleAnimation()
    {
        isAnimationFinished = false;

        //Show
        if (isGroupLensOpening)
        {
            groupLens.SetActive(isGroupLensOpening);
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
                if (isGroupLensOpening)
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

        //Hide
        if (!isGroupLensOpening)
        {
            groupLens.SetActive(isGroupLensOpening);
        }

        isAnimationFinished = true;

    }
}
