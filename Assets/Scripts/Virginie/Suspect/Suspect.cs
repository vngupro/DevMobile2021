using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Suspect", menuName = "SuspectSystem/Suspect")]
public class Suspect : ScriptableObject
{
    public new string name;
    public int age;
    public GenderType gender;
    [TextArea(5, 5)]
    public string description;
    public Sprite sprite;
    public bool isGuilty;

    [HideInInspector]
    private string[] descriptions;
}

public enum GenderType
{
    Female,
    Male,
}
