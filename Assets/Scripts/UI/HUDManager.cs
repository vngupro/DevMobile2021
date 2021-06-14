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
    public GameObject boxDialogue;

    [Header("Animation")]
    // Notes
    public float notesTranslateDuration = 1.0f;
    public float notesScaleDuration = 0.5f;
    public AnimationCurve translateCurve;
    public AnimationCurve scaleToMaxCurve;
    public AnimationCurve scaleToMinCurve;
    public Vector2 startPos;
    public Vector2 endPos;
    public float minScale = 0.1f;
    private RectTransform notesRectTransform;

    // Lens
    public List<float> lensAnimTime;

    [Header("Autopsy")]
    public Autopsy autopsyData;
    public TMP_Text autopsyVictimName;
    public TMP_Text autopsyPhysicalDescription;
    public TMP_Text autopsyHeight;
    public TMP_Text autopsyJob;
    public TMP_Text autopsyBloodType;
    public TMP_Text autopsyTimeOfDeath;
    public TMP_Text autopsyCauseOfDeath;
    public TMP_Text autopsyRemarks;

    [Header("Case Info")]
    public Case caseData;
    public TMP_Text caseTitle;
    public TMP_Text caseCorps;

    [Header("Debug")]
    [SerializeField]
    private bool isGroupLensOpen = false;
    public bool IsGroupLensOpen { get => isGroupLensOpen; private set => isGroupLensOpen = value ; }
    [SerializeField]
    private bool isLayerNotesOpen = false;
    public bool IsLayerNotesOpen { get => isLayerNotesOpen; private set => isLayerNotesOpen = value; }
    [SerializeField]
    private bool isAnimationFinished = true;

    private List<Vector2> lensPosition = new List<Vector2>();
    private Vector2 buttonLensPosition;

    public static HUDManager Instance { get; protected set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        
        buttonLensPosition = buttonLens.GetComponent<RectTransform>().anchoredPosition;
        foreach(RectTransform len in lens)
        {
            lensPosition.Add(len.anchoredPosition);
            len.anchoredPosition = buttonLensPosition;
        }

        buttonLens.gameObject.SetActive(true);
        buttonNotes.gameObject.SetActive(true);
        boxDialogue.SetActive(true);
        notesRectTransform = layerNotes.GetComponent<RectTransform>();

        // Load autopsy information and case information
        autopsyVictimName.text = autopsyData.victimName;
        autopsyPhysicalDescription.text = autopsyData.physicalDescription;
        autopsyHeight.text = autopsyData.height;
        autopsyJob.text = autopsyData.job;
        autopsyBloodType.text = autopsyData.bloodType;
        autopsyTimeOfDeath.text = autopsyData.timeOfDeath;
        autopsyCauseOfDeath.text = autopsyData.causeOfDeath;
        autopsyRemarks.text = autopsyData.remarks;

        caseTitle.text = caseData.title;
        caseCorps.text = caseData.corps;
    }

    private void Start()
    {
        groupLens.SetActive(false);
        isGroupLensOpen = false;
        layerNotes.SetActive(false);
        isLayerNotesOpen = false;
        isAnimationFinished = true;
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

        isLayerNotesOpen = !isLayerNotesOpen;

        if (isLayerNotesOpen)
        {
            StartCoroutine(NotesOpenAnimation());
        }
        else
        {
            StartCoroutine(NotesCloseAnimations());
        }
    }

    private IEnumerator NotesOpenAnimation()
    {
        notesRectTransform.localScale = new Vector2(minScale, minScale);
        layerNotes.SetActive(isLayerNotesOpen);

        float timer = 0;
        while(timer < notesTranslateDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / notesTranslateDuration;
            Vector2 newPos = Vector2.Lerp(startPos, endPos, translateCurve.Evaluate(ratio));
            notesRectTransform.anchoredPosition = newPos;
            yield return null;
        }
        
        timer = 0;
        while (timer < notesScaleDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / notesScaleDuration;
            float newScale = Mathf.Lerp(minScale, 1, scaleToMaxCurve.Evaluate(ratio));
            notesRectTransform.localScale = new Vector2(newScale, newScale);
            yield return null;
        }
    }

    private IEnumerator NotesCloseAnimations()
    {
        float timer = 0;
        while (timer < notesScaleDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / notesScaleDuration;
            float newScale = Mathf.Lerp(1, minScale, scaleToMinCurve.Evaluate(ratio));
            notesRectTransform.localScale = new Vector2(newScale, newScale);
            yield return null;
        }

        timer = 0;
        while (timer < notesTranslateDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / notesTranslateDuration;
            Vector2 newPos = Vector2.Lerp(endPos, startPos, translateCurve.Evaluate(ratio));
            notesRectTransform.anchoredPosition = newPos;
            yield return null;
        }

        layerNotes.SetActive(isLayerNotesOpen);
    }
    public void LensToogle()
    {
        isGroupLensOpen = !isGroupLensOpen;

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

        //Hide
        if (!isGroupLensOpen)
        {
            groupLens.SetActive(isGroupLensOpen);
        }

        isAnimationFinished = true;

    }
}
