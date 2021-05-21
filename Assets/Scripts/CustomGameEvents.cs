using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
}

public class PickUpEvent : UnityEvent<GameObject> { }
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
