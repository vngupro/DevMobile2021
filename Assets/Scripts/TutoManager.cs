using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}


public enum TutoStep
{
    FIRST_DIALOGUE,
    FIRST_CLUE,
    USE_NOTE,
    USE_UV_LENS,
    UV_CLUE,
    SLIDE_ZOOM,
    CHANGE_ROOM,
    EXIT_CRIME_SCENE,
    REFLECTION_TIME,
    SUSPECT,
    TUTO_END,
}