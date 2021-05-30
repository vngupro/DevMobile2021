using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    // Dialogues
    [SerializeField] private DialogueData currentDialogue;

    //Clues
    [SerializeField] private Item firstClue;

    private bool firstClueIsPickable = false;

    private TutoStep currentStep;

    private void OnDisable()
    {
        currentDialogue.dialogueListIndex = 0;
        currentDialogue.isFinished = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        UtilsEvent.blockMoveControls.Invoke(); //Stop all mouvment
        CustomDialogueEvents.openBoxDialogue.Invoke(currentDialogue); //Oppen DialogueBox
        currentStep = TutoStep.FIRST_DIALOGUE;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStep == TutoStep.FIRST_DIALOGUE)
        {
            //Next si Fin des explication
            if (currentDialogue.isFinished)
            {
                currentStep = TutoStep.FIRST_CLUE;
            }
        }
        else if (currentStep == TutoStep.FIRST_CLUE)
        {
            NextDialogueStep();

            if (!firstClueIsPickable)   //permet de recupere le premiere indice
            {
                firstClue.isHidden = false;
            }

            //Next si indice pris
            if (!firstClue.gameObject.activeInHierarchy)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                currentStep = TutoStep.USE_NOTE;
            }
        }
        else if (currentStep == TutoStep.USE_NOTE)
        {
            NextDialogueStep();
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

    private void NextDialogueStep()
    {
        if (currentDialogue.isFinished && currentDialogue.hasNextDialogueData)
        {
            CustomDialogueEvents.openBoxDialogue.Invoke(currentDialogue.nextDialogueData);
            currentDialogue = currentDialogue.nextDialogueData;
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