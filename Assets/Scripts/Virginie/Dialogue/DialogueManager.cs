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

    [Header("Animation")]
    public float animDuration = 0.5f;
    public RectTransform backgroundName;
    public RectTransform backgroundDialogue;
    private float sizeDeltaNameX;
    private float sizeDeltaDialogueY;

    [Header("Debug")]
    [SerializeField]
    private bool isDialogueActive = false;
    [SerializeField]
    private List<Dialogue> dialoguesSeries = new List<Dialogue>();
    [SerializeField]
    private Dialogue currentDialogue;
    private int levelIndex = 1;
    private void Awake()
    {
        string dialoguePath = "Dialogue/General/";
        GetDialogue(dialoguePath);
        string partiePath = "Dialogue/Partie " + levelIndex + "/";
        GetDialogue(partiePath);

        NextDialogueByID(1);

        sizeDeltaNameX = backgroundName.sizeDelta.x;
        sizeDeltaDialogueY = backgroundDialogue.sizeDelta.y;
        ResetDialogueBox();

        // | Listener
        CustomGameEvents.changeDialogueActive.AddListener(ChangeDialogueActive);
    }

    private void OnDisable()
    {
        foreach (Dialogue dialogue in dialoguesSeries)
        {
            dialogue.dialogueIndex = 0;
        }
    }
    private void Start()
    {
        StartCoroutine(BoxDialogueAnimation());
    }

    private void GetDialogue(string path)
    {
        Dialogue[] tempDialogues = Resources.LoadAll(path, typeof(Dialogue)).Cast<Dialogue>().ToArray();
        foreach (Dialogue dialogue in tempDialogues)
        {
            dialoguesSeries.Add(dialogue);
        }
    }

    public void NextDialogueByDialogue(Dialogue dialogue)
    {
        ChangeDialogueUI(dialogue);
    }
    public void NextDialogueByID(int id)
    {
        if (!isDialogueActive) return;

        foreach(Dialogue dialogue in dialoguesSeries)
        {
            if(dialogue.dialogueID == id)
            {
                ChangeDialogueUI(dialogue);

                return;
            }
        }
    }

    public void ChangeDialogueUI(Dialogue dialogue)
    {
        imageCharacter.sprite = dialogue.character.sprite;
        textName.text = dialogue.character.name;
        textDialogue.text = dialogue.dialogueList[dialogue.dialogueIndex];
        dialogue.dialogueIndex++;

        currentDialogue = dialogue;
    }

    public void NextDialogue()
    {
        if (!isDialogueActive) return;

        if(currentDialogue.dialogueIndex < currentDialogue.dialogueList.Length)
        {
            Debug.Log("Dialogue Index : " + currentDialogue.dialogueIndex);
            ChangeDialogueUI(currentDialogue);
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
