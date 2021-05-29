using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
//How to use :

// In Awake :
// CustomGameEvents.customUnityEvent.AddListener(function);

// Call this function anywhere when you want to SendMessage
// CustomGameEvents.customUnityEvent.Invoke();
public static class CustomGameEvents
{
    // | Exemple
    //public static UnityEvent unityEvent = new UnityEvent();
    //public static CustomUnityEvent customUnityEvent = new CustomUnityEvent();
    //public static CustomUnityEvent2 customUnityEvent2 = new CustomUnityEvent2();
    //----------------------------------------------------------------------------

    public static UnityEvent openInventory = new UnityEvent();
    public static UnityEvent closeInventory = new UnityEvent();
    public static PickUpEvent pickUpEvent = new PickUpEvent();
    public static UnityEvent dragEvent = new UnityEvent();
    public static UnityEvent changeDialogueActive = new UnityEvent();

    public static UnityEvent videoStarted = new UnityEvent();
    public static UnityEvent videoEnded = new UnityEvent();
    public static UnityEvent cinematicStarted = new UnityEvent();
    public static UnityEvent cinematicEnded = new UnityEvent();

    public static UnityEvent hasPressAnyButtonEvent = new UnityEvent();
    public static UnityEvent hasNotInteruptVideo = new UnityEvent();
    public static UnityEvent hasTapScreen = new UnityEvent();
    public static UnityEvent enteredMenu = new UnityEvent();

    public static UnityEvent sceneLoaded = new UnityEvent();
    public static SwitchEvent switchLocation = new SwitchEvent();
    public static CameraEvent switchCamera = new CameraEvent();
    public static SceneEvent exitScene = new SceneEvent();
    public static DialogueByIDEvent switchDialogueByID = new DialogueByIDEvent();
    public static DialogueEvent switchDialogueByDialogue = new DialogueEvent();
}

public class SceneEvent : UnityEvent<string> { }
public class DialogueEvent : UnityEvent<Dialogue> { }
public class DialogueByIDEvent : UnityEvent<int> { }
public class PickUpEvent : UnityEvent<GameObject> { }
public class InventoryEvent : UnityEvent<InventoryItem> { }
public class SwitchEvent : UnityEvent<DoorScript> { }
public class CameraEvent : UnityEvent<CinemachineVirtualCamera> { }
// | Exemple
//If you want to pass value (a copy only)
//public class CustomUnityEvent : UnityEvent<int> { }
//public class CustomUnityEvent2 : UnityEvent<CustomUnityEvent2Data> { }

//public class CustomUnityEvent2Data {
//    public int number;
//    public string word;
//    public GameObject gameObject;

//    public CustomUnityEvent2Data(int _number, string _word, GameObject _gameObject)
//    {
//        this.number = _number;
//        this.word = _word;
//        this.gameObject = _gameObject;
//    }
//}
