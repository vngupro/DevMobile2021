using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasResultScript : MonoBehaviour
{
    public GameObject backgroundGeneral;
    public GameObject background;
    public CanvasGroup backgroundCanvasGroup;

    public float fadeBackgroundDuration;
    public AnimationCurve curve;

    [Header ("  First Part")]
    public TMP_Text textMurderer;
    public Image imageAccuse;

    [Header("  Second Part")]
    public TMP_Text caseTitle;

    public TMP_Text clueText; 
    public TMP_Text timeInCrimeScene; 
    public TMP_Text timeInSuspectScene; 
    public TMP_Text caseNotes;

    public GameObject[] stars;
    public Sprite spriteFullStar;
    public Sprite spriteEmptyStar;

    public Button buttonClose;

    [Header("  Debug")]
    [SerializeField] private bool isShowingResult = false;
    [SerializeField] private InputManager inputManager;

    private void Awake()
    {
        backgroundGeneral.SetActive(true);
        background.SetActive(false);
        backgroundCanvasGroup.alpha = 0; 

        buttonClose.onClick.AddListener(ReturnToMenu);
    }

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        inputManager.OnStartTouch += ShowSecondScreen;
    }
    private void OnDisable()
    {
        InputManager.Instance.OnEndTouch -= ShowSecondScreen;
    }
    public void UpdateInfo(string _caseTitle, string _clueText, string _timeCrime, string _timeSuspect, string _caseNotes, int _nbStars, int totalClues, string endText, Sprite spriteAccuse)
    {
        textMurderer.text = endText;
        imageAccuse.sprite = spriteAccuse;

        caseTitle.text = _caseTitle;
        clueText.text = "Clue Founds : " + _clueText +  " / " + totalClues;
        timeInCrimeScene.text = "Time in Crime Scene : " + _timeCrime;
        timeInSuspectScene.text = "Time in Suspect Scene : " + _timeSuspect;
        caseNotes.text = _caseNotes;

        int currentStar = 0;
        foreach (GameObject star in stars)
        {
            if (currentStar < _nbStars)
            {
                star.GetComponent<Image>().sprite = spriteFullStar;
            }
            ++currentStar;
        }

        isShowingResult = true;
    }

    private void ShowSecondScreen(Vector2 pos, float time)
    {
        if(!isShowingResult) { return; }

        isShowingResult = false;
        StartCoroutine(FadeInBackground());
    }

    private IEnumerator FadeInBackground()
    {
        background.SetActive(true);

        float timer = 0;

        while (timer < fadeBackgroundDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeBackgroundDuration;
            backgroundCanvasGroup.alpha = Mathf.Lerp(0, 1, curve.Evaluate(ratio));
            yield return null;
        }
    }

    private void ReturnToMenu()
    {
        LevelManager.Instance.OpenSceneByName("Menu");
    }
}
