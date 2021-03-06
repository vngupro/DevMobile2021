using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text textDialogue;
    public TMP_Text textName;
    public Image imageCharacter;
    public Button buttonNextDialogue;

    [Header("Animation")]
    public float animDuration = 0.5f;
    public RectTransform backgroundName;
    public RectTransform backgroundDialogue;
    private float sizeDeltaNameX;
    private float sizeDeltaDialogueY;

    [Header("Debug")]
    [SerializeField]
    private bool isBoxDialogueOpen = false;
    [SerializeField]
    private bool isAnimationActive = false;
    [SerializeField]
    private List<DialogueData> dialoguesSeries = new List<DialogueData>();
    [SerializeField]
    private DialogueData currentDialogue;

    #region Initialization
    private void Awake()
    {
        //UI
        sizeDeltaNameX = backgroundName.sizeDelta.x;
        sizeDeltaDialogueY = backgroundDialogue.sizeDelta.y;
        ResetDialogueBox();

        // | Listen To
        CustomDialogueEvents.openBoxDialogue.AddListener(OpenBoxDialogue);
        CustomDialogueEvents.closeBoxDialogue.AddListener(CloseBoxDialogue);

    }

    private void OnDisable()
    {
        dialoguesSeries.Clear();
    }

    private void ResetDialogueBox()
    {
        backgroundName.sizeDelta = new Vector2(0f, backgroundName.sizeDelta.y);
        backgroundDialogue.sizeDelta = new Vector2(backgroundDialogue.sizeDelta.x, 0f);
        backgroundName.gameObject.SetActive(false);
        backgroundDialogue.gameObject.SetActive(false);
        buttonNextDialogue.gameObject.SetActive(false);
    }

    #endregion

    public void NextDialogue()
    {
        if (!isBoxDialogueOpen) return;

        if (currentDialogue.dialogueListIndex == currentDialogue.dialogueList.Count)
        {
            CloseBoxDialogue();
        }
        else if (currentDialogue.dialogueListIndex < currentDialogue.dialogueList.Count)
        {
            UpdateUIBoxDialogueData(currentDialogue);
        }
    }

    #region Next DialogueData List
    public void NextDialogueListByID(int id)
    {
        if (!isBoxDialogueOpen) return;

        foreach(DialogueData dialogue in dialoguesSeries)
        {
            if(dialogue.id == id)
            {
                UpdateUIBoxDialogueData(dialogue);

                return;
            }
        }
    }

    public void NextDialogueListByDialogue(DialogueData dialogue)
    {
        if (!isBoxDialogueOpen) return;

        UpdateUIBoxDialogueData(dialogue);
    }
    #endregion

    public void UpdateUIBoxDialogueData(DialogueData dialogue)
    {
        imageCharacter.sprite = dialogue.character.sprite;
        textName.text = dialogue.character.name;
        textDialogue.text = dialogue.dialogueList[dialogue.dialogueListIndex];
        dialogue.dialogueListIndex++;
        currentDialogue = dialogue;
    }

    #region Box DialogueData Method
    public void OpenBoxDialogue(DialogueData dialogueData)
    {
        UpdateUIBoxDialogueData(dialogueData);
        if (isAnimationActive) return;
        StartCoroutine(OpenBoxDialogueAnimation());
    }

    public void CloseBoxDialogue()
    {
        if (isAnimationActive) return;
        StartCoroutine(CloseBoxDialogueAnimation());
    }

    IEnumerator OpenBoxDialogueAnimation()
    {
        isAnimationActive = true;
        // Animation Title
        backgroundName.gameObject.SetActive(true);
        float timer = 0f;
        while(backgroundName.sizeDelta.x < sizeDeltaNameX)
        {
            timer += Time.deltaTime;
            float ratio = timer / animDuration;
            float newValue = Mathf.Lerp(0, sizeDeltaNameX, ratio);
            backgroundName.sizeDelta = new Vector2(newValue, backgroundName.sizeDelta.y);

            yield return null;
        }
        backgroundName.sizeDelta = new Vector2(sizeDeltaNameX, backgroundName.sizeDelta.y);

        // Animation Corpus Text
        backgroundDialogue.gameObject.SetActive(true);
        timer = 0f;
        while(backgroundDialogue.sizeDelta.y < sizeDeltaDialogueY)
        {
            timer += Time.deltaTime;
            float ratio = timer / animDuration;
            float newValue = Mathf.Lerp(0, sizeDeltaDialogueY, ratio);
            backgroundDialogue.sizeDelta = new Vector2(backgroundDialogue.sizeDelta.x, newValue);
            yield return null;
        }
        backgroundDialogue.sizeDelta = new Vector2(backgroundDialogue.sizeDelta.x, sizeDeltaDialogueY);

        isAnimationActive = false;
        isBoxDialogueOpen = true;
        buttonNextDialogue.gameObject.SetActive(true);
    }

    IEnumerator CloseBoxDialogueAnimation()
    {
        isAnimationActive = true;
        buttonNextDialogue.gameObject.SetActive(false);

        // Animation Corpus Text
        float timer = 0f;
        while (backgroundDialogue.sizeDelta.y > 0)
        {
            timer += Time.deltaTime;
            float ratio = timer / animDuration;
            float newValue = Mathf.Lerp(sizeDeltaDialogueY, 0, ratio);
            backgroundDialogue.sizeDelta = new Vector2(backgroundDialogue.sizeDelta.x, newValue);
            yield return null;
        }
        backgroundDialogue.sizeDelta = new Vector2(backgroundDialogue.sizeDelta.x, 0);

        // Animation Title
        timer = 0f;
        while (backgroundName.sizeDelta.x > 0)
        {
            timer += Time.deltaTime;
            float ratio = timer / animDuration;
            float newValue = Mathf.Lerp(sizeDeltaNameX, 0, ratio);
            backgroundName.sizeDelta = new Vector2(newValue, backgroundName.sizeDelta.y);
            yield return null;
        }
        backgroundName.sizeDelta = new Vector2(0, backgroundName.sizeDelta.y);

        isBoxDialogueOpen = false;
        isAnimationActive = false;
        currentDialogue.isFinished = true;
    }

    IEnumerator BoxDialogueBlink()
    {
        backgroundName.gameObject.SetActive(false);
        backgroundDialogue.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        backgroundName.gameObject.SetActive(true);
        backgroundDialogue.gameObject.SetActive(true);
    }

    #endregion
}
