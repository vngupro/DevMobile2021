using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensManager : MonoBehaviour
{
    public GlobalPostProcessVolume processVolume;

    private GameObject[] clues;
    [SerializeField] private Color colorUV;
    [SerializeField] private Color colorIR;
    private LensEnum currentLens;    

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
            clues = GameObject.FindGameObjectsWithTag("Clue");
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
        if (currentLens != LensEnum.UV)
        {
            NormalMode();

            foreach (GameObject clue in clues)
            {
                if(clue.GetComponent<Item>().data.filter == LensEnum.UV)
                {
                    clue.GetComponent<SpriteRenderer>().color = colorUV;
                }
                else
                {
                    clue.GetComponent<SpriteRenderer>().color = processVolume.GetUVColor();
                }
            }

            currentLens = LensEnum.UV;
        }
    }
    
    public void LightUpIRClues()
    {
        if (currentLens != LensEnum.IR)
        {
            NormalMode();

            foreach (GameObject clue in clues)
            {
                if (clue.GetComponent<Item>().data.filter == LensEnum.IR)
                {
                    clue.GetComponent<SpriteRenderer>().color = colorIR;
                }
                else
                {
                    clue.GetComponent<SpriteRenderer>().color = processVolume.GetIRColor();
                }
            }

            currentLens = LensEnum.IR;
        }
    }

    public void NormalMode()
    {
        foreach (GameObject clue in clues)
        {
            clue.GetComponent<SpriteRenderer>().color = Color.white;
        }

        currentLens = LensEnum.NONE;
    }

}
