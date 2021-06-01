using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VariableManager : MonoBehaviour
{
    public static string killer;
    public Text TheKiller;

    private void Start()
    {
        TheKiller.text = killer;
    }


    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadGame (string input)
    {
        killer = input;
        SceneManager.LoadScene("Resultat");
    }
    
    
}
