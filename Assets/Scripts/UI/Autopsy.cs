using UnityEngine;

[CreateAssetMenu(fileName = "New Autopsy", menuName = "CaseFiles/Autopsy")]
public class Autopsy : ScriptableObject
{
    public string title;
    [TextArea(10, 10)]
    public string corps;
}
