using UnityEngine;

[CreateAssetMenu(fileName = "New Autopsy", menuName = "CaseFiles/Autopsy")]
public class Autopsy : ScriptableObject
{
    public string victimName;
    public string physicalDescription;
    public string height;
    public string job;
    public string bloodType;
    public string timeOfDeath;
    [TextArea(5, 5)]
    public string causeOfDeath;
    [TextArea(5, 5)]
    public string remarks;
}
