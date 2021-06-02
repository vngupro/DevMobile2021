using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Suspect", menuName = "SuspectSystem/Suspect")]
public class Suspect : ScriptableObject
{
    public new string name;
    public int age;
    public string height;
    public SexeType sexe;
    public string bloodType;
    public string job;
    public Nationality nationality;
    public Nationality secondNationality;
    public string placeOfResidence;
    [TextArea(5,5)]
    public string alibi;
    [TextArea(5,5)]
    public string suspectedMotive;
    [TextArea(5, 5)]
    public string description;
    public Sprite sprite;
    public Sprite fingerprint;
    public bool isGuilty;

    [HideInInspector]
    private string[] descriptions;
}

public enum SexeType
{
    Female,
    Male,
}

public enum Nationality
{
    None,
    French,
    English,
    Italian,
    German,
    Spanish,
    American,
    Algerian,
    Irish,
    Canadian,
    Brazilian,
    Russian,
    Japanese,
    Chinese,
    Vietnamese,
    Indian,
    Australian,
    Taiwanese,
    Thai,
}