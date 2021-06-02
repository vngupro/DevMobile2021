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
    [SerializeField] private GameObject buttonLens;
    [SerializeField] private GameObject groupeLens;
    [SerializeField] private GameObject buttonNotes;
    [SerializeField] private GameObject roomChange;
    [SerializeField] private GameObject text_ToKitchen;
    [SerializeField] private GameObject text_Exit;
    [SerializeField] private GameObject doorExit;
    [SerializeField] private CanvasExitDoorScript canvasDoorExit;
    [SerializeField] private GameObject canvasSuspect;

    private Vector2 camStartPos;

    private bool haveOpenNote = false;
    private bool haveCloseNote = false;
    private bool haveMove = false;
    private bool haveSwitchLocation = false;

    private bool firstClueIsPickable = false;

    private GameManager gameManager;

    [Header ("Debug")]
    [SerializeField]private TutoStep currentStep;

    private void OnDisable()
    {
        currentDialogue.dialogueListIndex = 0;
        currentDialogue.isFinished = false;
    }

    private void Awake()
    {
        CustomGameEvents.switchLocation.AddListener(TrueHaveSwitchLocation);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        if(gameManager != null)
        {
            if (!GameManager.Instance.gameData.IsTutoFinish)
            {
                UtilsEvent.blockMoveControls.Invoke(); //Stop all mouvment
                CustomDialogueEvents.openBoxDialogue.Invoke(currentDialogue); //Oppen DialogueBox
                currentStep = TutoStep.FIRST_DIALOGUE;
                buttonLens.SetActive(false);
                buttonNotes.SetActive(false);
                doorExit.SetActive(false);
                roomChange.SetActive(false);
                text_ToKitchen.SetActive(false);
                text_Exit.SetActive(false);
                camStartPos = Camera.main.transform.position;
                firstClue.isBlocked = true;
            }
            else
            {
                currentStep = TutoStep.TUTO_END;
            }
        }
        else
        {
            UtilsEvent.blockMoveControls.Invoke(); //Stop all mouvment
            CustomDialogueEvents.openBoxDialogue.Invoke(currentDialogue); //Oppen DialogueBox
            currentStep = TutoStep.FIRST_DIALOGUE;
            buttonLens.SetActive(false);
            buttonNotes.SetActive(false);
            doorExit.SetActive(false);
            roomChange.SetActive(false);
            text_ToKitchen.SetActive(false);
            text_Exit.SetActive(false);
            camStartPos = Camera.main.transform.position;
            firstClue.isBlocked = true;
        }

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
                firstClue.isBlocked = false;
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
                buttonNotes.SetActive(true);
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
                buttonLens.SetActive(true);
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

            // Si Slide ou Zoom
            Vector2 camCurrentPosition = Camera.main.transform.position;
            if(camCurrentPosition != camStartPos)
            {
                haveMove = true;
            }

            //Next si slide
            if (haveMove)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                currentStep = TutoStep.CHANGE_ROOM;
            }
        }
        else if (currentStep == TutoStep.CHANGE_ROOM)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 5 && currentDialogue.isFinished)
            {
                NextDialogueStep();
                roomChange.SetActive(true);
                text_ToKitchen.SetActive(true);
            }
            //Montre changement de room
            //Next si changement de room
            if (haveSwitchLocation)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                currentStep = TutoStep.REFLECTION_TIME;
            }
        }
        else if (currentStep == TutoStep.REFLECTION_TIME)
        {
            //Envoie les prochaine explications
            if (currentDialogueId == 6 && currentDialogue.isFinished)
            {
                NextDialogueStep();

                //Propose de sortir
                doorExit.SetActive(true);
                text_Exit.SetActive(true);
            }

            //Next si le joueur est sortie par l'exit
            if (canvasDoorExit.isExiting)
            {
                CustomDialogueEvents.closeBoxDialogue.Invoke();
                currentStep = TutoStep.SUSPECT;
                buttonLens.SetActive(false);
                groupeLens.SetActive(false);
            }

        }
        else if (currentStep == TutoStep.SUSPECT)
        {
            //Envoie les prochaine explications
            if (canvasSuspect.activeInHierarchy)
            {
                if (currentDialogueId == 7 && currentDialogue.isFinished)
                {
                    NextDialogueStep();
                }


                //Next si dialogue fini  ou suspet accusée
                if (currentDialogue.isFinished)
                {
                    currentStep = TutoStep.TUTO_END;
                }
            }
        }
        else if (currentStep == TutoStep.TUTO_END)
        {
            //dire au gameManager que le tuto est passée
            GameManager.Instance.gameData.IsTutoFinish = true;
            this.gameObject.SetActive(false);
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

    private void TrueHaveSwitchLocation(DoorScript door)
    {
        haveSwitchLocation = true;
        CustomGameEvents.switchLocation.RemoveListener(TrueHaveSwitchLocation);
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

//else if (currentStep == TutoStep.EXIT_CRIME_SCENE)
//{
//    //Envoie les prochaine explications
//    if (currentDialogueId == 7 && currentDialogue.isFinished)
//    {
//        NextDialogueStep();
//    }

//    //Next si sort
//    if (canvasDoorExit.isExiting)
//    {

//        currentStep = TutoStep.SUSPECT;
//    }
//}