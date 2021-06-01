using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCounter : MonoBehaviour
{
    private int items;
    public Text counter;

    // Start is called before the first frame update
    void Start()
    {
        counter.text = items.ToString();
    }

    void OnTriggerEnter2D(Collider2D trucQuiMeTraverse)
    {
        if (trucQuiMeTraverse.gameObject.tag == "hostage")
        {
            Destroy(trucQuiMeTraverse.gameObject);
            items++;
            counter.text = items.ToString();
        }
    }
}
