using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensManager : MonoBehaviour
{
    private GameObject[] clues;
    [SerializeField] private Color color;

    

    public static LensManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUpUVClues()
    {
        clues = GameObject.FindGameObjectsWithTag("Clue");

        foreach (GameObject clue in clues)
        {
            clue.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void NormalMode()
    {
        clues = GameObject.FindGameObjectsWithTag("Clue");

        foreach (GameObject clue in clues)
        {
            clue.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}
