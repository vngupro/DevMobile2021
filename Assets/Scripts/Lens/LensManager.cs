using UnityEngine;
using UnityEngine.UI;

public class LensManager : MonoBehaviour
{
    public GlobalPostProcessVolume processVolume;
    public GameObject shadows;

    [Header("   UI")]
    [SerializeField] private Button buttonLens;
    [SerializeField] private Button buttonNormal;
    [SerializeField] private Button buttonUV;
    [SerializeField] private Button buttonIR;
    [SerializeField] private Button buttonXRAY;
    [SerializeField] private Button buttonNight;


    private GameObject[] items;

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
        processVolume.ChangeColorToUV();
        buttonLens.image.color = buttonUV.image.color;
        if (shadows != null) { shadows.SetActive(true); }
    }
    
    public void LightUpIRClues()
    {
        SwitchLens(LensEnum.IR);
        processVolume.ChangeColorToIR();
        buttonLens.image.color = buttonIR.image.color;
        if (shadows != null) { shadows.SetActive(true); }
    }

    public void LightUpXRAYClues()
    {
        SwitchLens(LensEnum.XRAY);
        processVolume.ChangeColorToXRAY();
        buttonLens.image.color = buttonXRAY.image.color;
        if (shadows != null) { shadows.SetActive(true); }
    }
    public void LightUpNIGHTSHOTClues()
    {
        SwitchLens(LensEnum.NIGHTSHOT);
        processVolume.ChangeColorToNIGHTSHOT();
        buttonLens.image.color = buttonNight.image.color;
        if(shadows != null) { shadows.SetActive(false); }
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
        processVolume.ChangeColorToNormal();
        buttonLens.image.color = buttonNormal.image.color;
    }



}
