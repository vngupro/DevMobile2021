using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
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

    private int characterIndex = 0, dialogueIndex = 0;
    private bool isDialogueActive = false;

    private void Awake()
    {
        if(dialogues.Length == 0) {
            Debug.Log("No Dialogues to show");  
            return;
        }

        Debug.Log("New dialogue");
        textDialogue.text = dialogues[characterIndex].dialogueList[dialogueIndex];
        textName.text = dialogues[characterIndex].character.name;
        imageCharacter.sprite = dialogues[characterIndex].character.sprite;

        isDialogueActive = true;

        // | Listener
        CustomGameEvents.changeDialogueActive.AddListener(ChangeDialogueActive);
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
                Debug.Log("Next Dialogue");
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
            Debug.Log("Next Character");
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
}
