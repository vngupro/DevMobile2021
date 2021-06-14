using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Suspect : MonoBehaviour
{
    #region Variable
    public Suspect data;
    public SuspectManager suspectManager;
    
    [Header("   Debug")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private Button buttonAccuse;
    [SerializeField] private Button buttonInfo;
    [SerializeField] private Coroutine coroutine;
    [SerializeField] private float delayBeforeAnimation;
    public bool isGuilty { get; private set; }

    #endregion
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



        StartCoroutine(EyesAnimation());
    }

    private IEnumerator EyesAnimation()
    {
        while (true)
        {
            delayBeforeAnimation = Random.Range(1.0f, 5.0f);
            float timer = 0;
            while(timer < delayBeforeAnimation)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            image.sprite = data.spriteCloseEyes;
            yield return new WaitForSeconds(0.2f);
            image.sprite = data.sprite;
        }
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
