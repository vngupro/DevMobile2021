using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Problem 
 *    RaycastHit2D just hit one layer for now,
 *    but you will want to hit at least 2 layer
 *    if the clue is in a hiding place
 *    or you can do a bool isHidden
 *    to make it impossible to take if isHidden
 * */
public class PickUpDetection : MonoBehaviour
{
    [SerializeField]
    GameObject blink;
    
    #region Variable
    [SerializeField] private LayerMask layer2PickUp;
    [SerializeField] private float distanceTolerance = 0.5f;         //sensibility on small sliding on touch
    [SerializeField] private float timerBeforeHold = 1.0f;
    private InputManager inputManager;
    private InventoryManager inventory;
    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float endTime;
    private RaycastHit2D hitClue;
    private int items;
    public Text counter;
    #endregion
    void Start()
    {
        //counter.text = items.ToString();
    }
    private void Awake()
    {
        inputManager = InputManager.Instance;
        inventory = InventoryManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartPickUp;
        inputManager.OnEndTouch += EndPickUp;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartPickUp;
        inputManager.OnEndTouch -= EndPickUp;
    }

    private void StartPickUp(Vector2 position, float time)
    {
        if(inventory != null)
        {
            if (inventory.isOpen) return;
        }

        
        // Verify touch an object
        startPos = position;
        startTime = time;
        hitClue = Physics2D.Raycast(position, Vector3.forward, 20.0f, layer2PickUp);
    }

    private void EndPickUp(Vector2 position, float time)
    {
        if(inventory != null)
        {
            if (inventory.isOpen) return;
        }


        endPos = position;
        endTime = time;

        float distance = Vector3.Distance(startPos, endPos);
        float timer = endTime - startTime;
        if (distance <= distanceTolerance && 
            hitClue && 
            timer < timerBeforeHold && 
            hitClue.transform.gameObject.tag == "Clue" && 
            hitClue.transform.gameObject.GetComponent<Item>().data.isPickable)
        {
            PickUp(hitClue.transform.gameObject);
        }
    }

    private void PickUp(GameObject object2PickUp)
    {
        //here add verify bool isHidden
        CustomGameEvents.pickUpEvent.Invoke(object2PickUp);
        object2PickUp.SetActive(false);
        //Destroy(object2PickUp);
        StartCoroutine("CaptureIt");
        //if (object2PickUp.gameObject.tag == "Clue")
        //{
          //  items++;
            //counter.text = items.ToString();
        //}
    }
    private GameObject GoBlink;

    IEnumerator CaptureIt()
    {
        yield return new WaitForSeconds(0f);
        GoBlink = Instantiate(blink);
        Destroy(GoBlink, 0.1f);
    }
    

}
