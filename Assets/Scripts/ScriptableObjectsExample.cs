using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* click droit dans project -> y a une nouvelle colonne qui est apparu par miracle */
[CreateAssetMenu(fileName = "ScriptableObjectExample", menuName = "ScriptableObjects/Example")]
public class ScriptableObjectsExample : ScriptableObject
{
    public string objectName;
    [TextArea(10, 5)]
    public string description;
    public int number;
    public float floatNumber;
    public List<int> aList;
    public bool aBool;
}
