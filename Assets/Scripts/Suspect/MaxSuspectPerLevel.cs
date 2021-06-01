using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Max Suspect Chart", menuName = "SuspectSystem/MaxSuspectPerLevel")]
public class MaxSuspectPerLevel : ScriptableObject
{
    public List<int> maxSuspectList;
}
