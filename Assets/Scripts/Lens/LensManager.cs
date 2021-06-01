using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensManager : MonoBehaviour
{
    public GlobalPostProcessVolume processVolume;

    private GameObject[] items;
    //[SerializeField] private Color colorUV;
    //[SerializeField] private Color colorIR;
    //[SerializeField] private Color colorXRAY;
    //[SerializeField] private Color colorNIGHTSHOT;
    public LensEnum currentLens { get; private set; }    

    public static LensManager instance { get; protected set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            items = GameObject.FindGameObjectsWithTag("Item");
        }
    }

    private void SwitchLens(LensEnum lens)
    {
        if (currentLens != lens)
        {
            NormalMode();

            foreach (GameObject clue in items)
            {
                SpriteRenderer spriteRenderer = clue.GetComponent<SpriteRenderer>();
                Item item = clue.GetComponent<Item>();
                Debug.Log(item.data.filter);
                if (item.data.filter == lens)
                {
                    spriteRenderer.sprite = item.data.spriteOnLens;
                    Debug.Log(spriteRenderer.gameObject.name);
                    item.isHidden = false;
                }
                else
                {
                    spriteRenderer.color = processVolume.GetLensColor(lens);
                    if (item.data.filter != LensEnum.NONE)
                    {
                        item.isHidden = true;
                    }
                    
                }
            }
            currentLens = lens;
        }
    }

    public void LightUpUVClues()
    {
        SwitchLens(LensEnum.UV);
    }
    
    public void LightUpIRClues()
    {
        SwitchLens(LensEnum.IR);
    }

    public void LightUpXRAYClues()
    {
        SwitchLens(LensEnum.XRAY);
    }
    public void LightUpNIGHTSHOTClues()
    {
        SwitchLens(LensEnum.NIGHTSHOT);
    }

    public void NormalMode()
    {
        foreach (GameObject clue in items)
        {
            SpriteRenderer spriteRenderer = clue.GetComponent<SpriteRenderer>();
            Item item = clue.GetComponent<Item>();

            spriteRenderer.color = Color.white;
            if (item.data.sprite != null)
            {
                spriteRenderer.sprite = item.data.sprite;
            }
            else
            {
                spriteRenderer.sprite = null;
            }

            if(item.data.filter == LensEnum.NONE)
            {
                item.isHidden = false;
            }
            else
            {
                item.isHidden = true;
            }
        }

        currentLens = LensEnum.NONE;
    }

}
