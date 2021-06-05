using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public Sprite spriteTabIdle;
    public Sprite spriteTabHover;
    public Sprite spriteTabActive;

    public Color colorTabIdle;
    public Color colorTabHover;
    public Color colorTabActive;

    public Color colorTextIdle;
    public Color colorTextHover;
    public Color colorTextActive;

    public Color colorIconIdle;
    public Color colorIconHover;
    public Color colorIconActive;

    public bool hasColorSpriteChange = false;

    public List<GameObject> objectsToSwap;

    [Header("Sound")]
    [SerializeField] private string sound;

    [Header("Animation")]
    public GameObject arrow;
    public float slideAnimation = 0.2f;
    public AnimationCurve curve;

    [Header("Debug")]
    public List<TabButtonScript> tabButtons;
    public TabButtonScript selectedTab;
    public RectTransform arrowRect;

    private void Awake()
    {
        if(arrow != null)
        {
            arrowRect = arrow.GetComponent<RectTransform>();
            arrow.SetActive(false);
        }

        foreach(GameObject objectToSwap in objectsToSwap)
        {
            objectToSwap.SetActive(false);
        }
    }

    public void Subscribe(TabButtonScript button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtonScript>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButtonScript button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab) {

            if (hasColorSpriteChange)
            {
                button.background.sprite = spriteTabHover;
                button.background.color = colorTabHover;
            }

            button.textBox.color = colorTextHover;
            button.icon.color = colorIconHover;
        }
    }

    public void OnTabExit(TabButtonScript button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButtonScript button)
    {
        selectedTab = button;
        ResetTabs();

       
        if (hasColorSpriteChange)
        {
            button.background.sprite = spriteTabActive;
            button.background.color = colorTabActive;
        }

        button.textBox.color = colorTextActive;
        button.icon.color = colorIconActive;
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }

        //Arrow Animation
        if (arrow != null)
        {
            arrow.SetActive(true);
            StartCoroutine(SlideArrowAnimation(button));
        }

        // Sound
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(sound);
        }
    }

    public void ResetTabs()
    {
        foreach(TabButtonScript button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }

            if (hasColorSpriteChange)
            {
                button.background.sprite = spriteTabIdle;
                button.background.color = colorTabIdle;
            }

            button.textBox.color = colorTextIdle;
            button.icon.color = colorIconIdle;
        }
    }

    IEnumerator SlideArrowAnimation(TabButtonScript button)
    {
        float timer = 0;

        while(timer < slideAnimation)
        {
            timer += Time.deltaTime;
            float ratio = timer / slideAnimation;

            float newPos = button.GetComponent<RectTransform>().anchoredPosition.y;
            arrowRect.anchoredPosition = new Vector2(
                    arrowRect.anchoredPosition.x, 
                    Mathf.Lerp(arrowRect.anchoredPosition.y,  newPos, curve.Evaluate(ratio))
                );

            yield return null;
        }

    }
}
