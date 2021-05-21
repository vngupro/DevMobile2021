using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DialogueManager : MonoBehaviour
{
    public Dialogue[] dialogues;
    public TMP_Text textDialogue;
    public TMP_Text textName;
    public Image imageCharacter;

    private int characterIndex = 0, dialogueIndex = 0;
    private bool isDialogueActive = false;

    private void Awake()
    {
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
