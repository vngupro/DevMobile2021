using UnityEngine;

[CreateAssetMenu(fileName = "New Case", menuName = "CaseFiles/Case")]
public class Case : ScriptableObject
{
    public string title;
    [TextArea(10, 10)]
    public string corps;
}
