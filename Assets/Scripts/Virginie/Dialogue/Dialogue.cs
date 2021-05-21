using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "DialogueSystem/Dialogue")]
public class Dialogue : ScriptableObject
{
    public Character character;
    [TextArea(5,5)]
    public string[] dialogueList;
}
