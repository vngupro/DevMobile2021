using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "DialogueSystem/Dialogue")]
public class Dialogue : ScriptableObject
{
    public int dialogueID = 0;
    public Character character;
    [TextArea(3,3)]
    public string[] dialogueList;

    public bool hasNextDialogue = false;
    public Dialogue nextDialogue;

    [Header("Debug")]
    public int dialogueIndex = 0;
    public bool isFinished = false;
}
