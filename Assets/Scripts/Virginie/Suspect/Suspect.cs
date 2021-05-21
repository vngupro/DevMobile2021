using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Suspect", menuName = "SuspectSystem/Suspect")]
public class Suspect : ScriptableObject
{
    public new string name;
    [TextArea(5, 5)]
    public string description;
    public bool isGuilty;

    [HideInInspector]
    private string[] descriptions;
}
