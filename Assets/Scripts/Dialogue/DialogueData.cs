using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "DialogueSystem/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public int id = 0;
    public Character character;

    [TextArea(3, 3)]
    public List<string> dialogueList;

    public bool hasNextDialogueData = false;
    public DialogueData nextDialogueData;

    [Header("Debug")]
    public int dialogueListIndex = 0;
    public bool isFinished = false;
}
