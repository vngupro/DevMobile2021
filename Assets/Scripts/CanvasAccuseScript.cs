using UnityEngine;
using UnityEngine.UI;
public class CanvasAccuseScript : MonoBehaviour
{
    public GameObject background;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;

    private void Start()
    {
        buttonNo.onClick.AddListener(NoAccuse);
        buttonYes.onClick.AddListener(YesAccuse);
        background.SetActive(false);
    }

    public void YesAccuse()
    {
        
    }

    public void NoAccuse()
    {
        background.SetActive(false);
    }
}
