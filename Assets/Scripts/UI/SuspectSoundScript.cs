using UnityEngine;
using UnityEngine.UI;

public class SuspectSoundScript : MonoBehaviour
{
    [Header("   Button")]
    [SerializeField] private Button yesAccuse;
    [SerializeField] private Button noAccuse;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonPrevious;
    [SerializeField] private Button buttonBack;

    [Header("   Sound")]
    [SerializeField] private string yesAccuseSound;
    [SerializeField] private string noAccuseSound;
    [SerializeField] private string buttonAccuseSound;
    [SerializeField] private string buttonSuspectSound;
    [SerializeField] private string buttonNextSound;
    [SerializeField] private string buttonPreviousSound;
    [SerializeField] private string buttonBackSound;

    private void Awake()
    {
        yesAccuse.onClick.AddListener(SoundYesAccuse);
        noAccuse.onClick.AddListener(SoundNoAccuse);
        buttonNext.onClick.AddListener(SoundButtonNext);
        buttonPrevious.onClick.AddListener(SoundButtonPrevious);
        buttonBack.onClick.AddListener(SoundButtonBack);
    }

    private void OnEnable()
    {
        if(SuspectManager.Instance == null) { return; }
        SuspectManager.Instance.AccuseSuspect += SoundButtonAccuse;
        SuspectManager.Instance.OpenSuspectInfo += SoundButtonSuspect;
    }

    private void OnDisable()
    {
        SuspectManager.Instance.AccuseSuspect -= SoundButtonAccuse;
        SuspectManager.Instance.OpenSuspectInfo -= SoundButtonSuspect;
    }
    private void SoundYesAccuse()
    {
        SoundManager.Instance?.PlaySound(yesAccuseSound);
    }
    
    private void SoundNoAccuse()
    {
        SoundManager.Instance?.PlaySound(noAccuseSound);
    }

    private void SoundButtonAccuse()
    {
        SoundManager.Instance?.PlaySound(buttonAccuseSound);
    }

    private void SoundButtonSuspect()
    {
        SoundManager.Instance?.PlaySound(buttonSuspectSound);
    }

    private void SoundButtonNext()
    {
        SoundManager.Instance?.PlaySound(buttonNextSound);
    }

    private void SoundButtonPrevious()
    {
        SoundManager.Instance?.PlaySound(buttonPreviousSound);
    }

    private void SoundButtonBack()
    {
        SoundManager.Instance?.PlaySound(buttonBackSound);
    }

}
