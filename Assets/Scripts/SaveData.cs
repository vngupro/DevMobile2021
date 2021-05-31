using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField]
    private int lastScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 25) ;
    }

    // Update is called once per frame
    void Update()
    {
        lastScore = PlayerPrefs.GetInt("Score");
    }
}
