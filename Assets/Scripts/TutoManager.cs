using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{

    private TutoStep currentStep;

    // Start is called before the first frame update
    void Start()
    {
        //Open Diologue Box
        UtilsEvent.blockMoveControls.Invoke();
        currentStep = TutoStep.FIRST_DIALOGUE;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStep == TutoStep.FIRST_DIALOGUE)
        {
            //Dialogue d'explication 
            //Next si Fin des explication
        }
        else if (currentStep == TutoStep.FIRST_CLUE)
        {
            //Explique les indice 
            //permet de recupere le premiere indice
            //Next si indice pris
        }
        else if (currentStep == TutoStep.USE_NOTE)
        {
            //Explique le carnet
            //Next si carnet ouvert
        }
        else if (currentStep == TutoStep.USE_UV_LENS)
        {
            //Explique les filtre
            //Next si UV actif 
        }
        else if (currentStep == TutoStep.UV_CLUE)
        {
            //Montre l'indice UV
            //Next si l'indice UV pris
        }
        else if (currentStep == TutoStep.SLIDE_ZOOM)
        {
            //Montre le Slide
            //Next si slide
        }
        else if (currentStep == TutoStep.CHANGE_ROOM)
        {
            //Montre changement de room
            //Next si changement de room
        }
        else if (currentStep == TutoStep.REFLECTION_TIME)
        {
            //Expilque au joueur de chercher
            //Next si le joueur a recuperée un autre indice
        }
        else if (currentStep == TutoStep.EXIT_CRIME_SCENE)
        {
            //propose de sortire
            //Next si sort
        }
        else if (currentStep == TutoStep.SUSPECT)
        {
            //Explication Suspet 
            //Next si dialogue fini 
        }
        else if (currentStep == TutoStep.TUTO_END)
        {
            //Explication finale
            //end of tuto 
        }
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
    REFLECTION_TIME,
    EXIT_CRIME_SCENE,
    SUSPECT,
    TUTO_END,
}