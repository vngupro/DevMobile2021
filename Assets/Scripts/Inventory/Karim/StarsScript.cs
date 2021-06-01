using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScript : MonoBehaviour
{
    public GameObject[] stars;
    private int cluesCount;
    // Start is called before the first frame update
    void Start()
    {
        cluesCount = GameObject.FindGameObjectsWithTag("Clue").Length;
    }

    // Update is called once per frame
   public void starAcheived()
    {
        int cluesLeft = GameObject.FindGameObjectsWithTag("Clue").Length;
        int cluesCollected = cluesCount - cluesLeft;

        float percentage = float.Parse( cluesCollected.ToString()) / float.Parse(cluesCount.ToString()) * 100f;
        Debug.Log(percentage + "%%");
        if(percentage >= 33f && percentage <66)
        {
            //2 stars
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        else if (percentage >= 66 && percentage < 70)
        {
            //3 stars
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
        else if(percentage < 30)
        {

        }
        else
        {
            //5 stars
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
            stars[3].SetActive(true);
            stars[4].SetActive(true);
        }
    }
}
