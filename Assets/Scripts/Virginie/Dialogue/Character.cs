using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "DialogueSystem/Character")]
public class Character : ScriptableObject
{
    public new string name;
    public Sprite sprite;

}
