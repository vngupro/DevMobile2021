using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    // Dialogues
    [SerializeField] private DialogueData currentDialogue;
    private int currentDialogueId = 0;

    //Clues
    [SerializeField] private Item firstClue;
    [SerializeField] private Item firstLensClue;

    //HUD
    [SerializeField] private HUDManager HUDManager;
    private bool haveOpenNote = false;
    private bool haveCloseNote = false;

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
            //Envoie les prochaine explications
            if (currentDialogueId == 0 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }

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
            //Envoie les prochaine explications
            if (currentDialogueId == 1 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }

            //Next si carnet ouvert puis refermé
            if (HUDManager.IsLayerNotesOpen && !haveOpenNote)
            {
                haveOpenNote = true;
            }
            if (!HUDManager.IsLayerNotesOpen && haveOpenNote)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                haveCloseNote = true;
            }
            if (haveCloseNote)
            {
                currentStep = TutoStep.USE_UV_LENS;
            }
        }
        else if (currentStep == TutoStep.USE_UV_LENS)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 2 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }

            //Next si UV actif 
            if (LensManager.instance.currentLens == LensEnum.UV)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                currentStep = TutoStep.UV_CLUE;
            }

        }
        else if (currentStep == TutoStep.UV_CLUE)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 3 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }

            //Next si l'indice UV pris
            if (!firstLensClue.gameObject.activeInHierarchy)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                currentStep = TutoStep.SLIDE_ZOOM;
            }
        }
        else if (currentStep == TutoStep.SLIDE_ZOOM)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 4 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }

            UtilsEvent.unlockMoveControls.Invoke();

            //Next si slide
            if (currentDialogue.isFinished)
            {
                currentStep = TutoStep.CHANGE_ROOM;
            }
        }
        else if (currentStep == TutoStep.CHANGE_ROOM)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 5 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }
            //Montre changement de room
            //Next si changement de room
        }
        else if (currentStep == TutoStep.REFLECTION_TIME)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 6 && currentDialogue.isFinished)
            {
                NextDialogueStep();
            }
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
            currentDialogue.dialogueListIndex = 0;
            currentDialogue.isFinished = false;
            CustomDialogueEvents.openBoxDialogue.Invoke(currentDialogue.nextDialogueData);
            currentDialogue = currentDialogue.nextDialogueData;
            currentDialogueId++;
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