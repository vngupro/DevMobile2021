using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("Add Dialogue Scriptable Object")]
    public Dialogue[] dialogues;
    [Tooltip("Canvas Text_Dialogue")]
    public TMP_Text textDialogue;
    [Tooltip("Canvas Text_Name")]
    public TMP_Text textName;
    [Tooltip("Canvas Image_Character")]
    public Image imageCharacter;

    [Header("Animation")]
    public float animDuration = 0.5f;
    public RectTransform backgroundName;
    public RectTransform backgroundDialogue;
    private float sizeDeltaNameX;
    private float sizeDeltaDialogueY;

    private int characterIndex = 0, dialogueIndex = 0;

    [Header("Debug")]
    [SerializeField]
    private bool isDialogueActive = false;

    private void Awake()
    {
        if(dialogues.Length == 0) {
            Debug.Log("No Dialogues to show");  
            return;
        }

        //Debug.Log("New dialogue");
        textDialogue.text = dialogues[characterIndex].dialogueList[dialogueIndex];
        textName.text = dialogues[characterIndex].character.name;
        imageCharacter.sprite = dialogues[characterIndex].character.sprite;
        imageCharacter.color = dialogues[characterIndex].character.color;

        sizeDeltaNameX = backgroundName.sizeDelta.x;
        sizeDeltaDialogueY = backgroundDialogue.sizeDelta.y;
        ResetDialogueBox();
        //isDialogueActive = true;

        // | Listener
        CustomGameEvents.changeDialogueActive.AddListener(ChangeDialogueActive);
    }

    private void Start()
    {
        StartCoroutine(BoxDialogueAnimation());
    }
    public void NextDialogue()
    {
        if (!isDialogueActive) return;

        dialogueIndex++;
        int characterCount = dialogues.Length;
        if (characterIndex < characterCount)
        {
            int dialogueCount = dialogues[characterIndex].dialogueList.Length;
            if (dialogueIndex < dialogueCount)
            {
                textDialogue.text = dialogues[characterIndex].dialogueList[dialogueIndex];
                //Debug.Log("Next Dialogue");
            }
            else
            {
                NextCharacter();
            }
        }
    }

    public void NextCharacter()
    {
        if (!isDialogueActive) return;
        characterIndex++;
        int characterCount = dialogues.Length;
        if (characterIndex < characterCount)
        {
            textDialogue.text = dialogues[characterIndex].dialogueList[dialogueIndex];
            //Debug.Log("Next Character");
        }
        else
        {
            Debug.Log("No More Character");
        }
    }

    public void ChangeDialogueActive()
    {
        isDialogueActive = !isDialogueActive;
    }

    private void ResetDialogueBox()
    {
        backgroundName.sizeDelta = new Vector2(0f, backgroundName.sizeDelta.y);
        backgroundDialogue.sizeDelta = new Vector2(backgroundDialogue.sizeDelta.x, 0f);
        backgroundName.gameObject.SetActive(false);
        backgroundDialogue.gameObject.SetActive(false);
    }

    IEnumerator BoxDialogueAnimation()
    {
        
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

        isDialogueActive = true;
    }

    IEnumerator BoxDialogueBlink()
    {
        backgroundName.gameObject.SetActive(false);
        backgroundDialogue.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        backgroundName.gameObject.SetActive(true);
        backgroundDialogue.gameObject.SetActive(true);
    }
}
