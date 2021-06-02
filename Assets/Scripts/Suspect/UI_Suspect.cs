using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Suspect : MonoBehaviour
{
    public Suspect data;
    public SuspectManager suspectManager;
    [Header("Debug")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private Button buttonAccuse;
    [SerializeField] private Button buttonInfo;
    public bool isGuilty { get; private set; }

    private void Start()
    {

        image = this.gameObject.transform.Find("Image_Suspect").GetComponent<Image>();
        textDescription = GetComponentInChildren<TMP_Text>();
        buttonAccuse = this.gameObject.transform.Find("Button_Accuse").GetComponent<Button>();
        buttonInfo = this.gameObject.transform.Find("Button_Info").GetComponent<Button>();
        image.sprite = data.sprite;
        isGuilty = data.isGuilty;
        buttonInfo.onClick.AddListener(OpenSuspectInfo);
        buttonAccuse.onClick.AddListener(Accuse);
    }

    private void Accuse()
    {
        suspectManager.OnAccuse(this);
    }

    private void OpenSuspectInfo()
    {
        suspectManager.OpenPanelInfo(this);
    }
    
}
