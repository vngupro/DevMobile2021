using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Suspect", menuName = "SuspectSystem/Suspect")]
public class Suspect : ScriptableObject
{
    public new string name;
    public int age;
    public string height;
    public GenderType gender;
    public string bloodType;
    public string job;
    public Nationality nationality;
    public string placeOfResidence;
    [TextArea(5,5)]
    public string alibi;
    [TextArea(5,5)]
    public string suspectedMotive;
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

public enum Nationality
{
    American,
    French,
    English,
    Italian,
    German,
}