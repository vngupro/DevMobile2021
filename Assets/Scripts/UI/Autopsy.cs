using UnityEngine;

[CreateAssetMenu(fileName = "New Autopsy", menuName = "CaseFiles/Autopsy")]
public class Autopsy : ScriptableObject
{
    public string title;
    public string victim;
    public string timeOfDeath;
    [TextArea(5, 5)]
    public string causeOfDeath;
    [TextArea(5, 5)]
    public string remarks;
}
