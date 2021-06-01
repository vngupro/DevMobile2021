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

    public List<GameObject> objectsToSwap;

    [Header("Debug")]
    public List<TabButtonScript> tabButtons;
    public TabButtonScript selectedTab;

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
            //button.background.sprite = spriteTabHover;
            //button.background.color = colorTabHover;
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
        //button.background.sprite = spriteTabActive;
        //button.background.color = colorTabActive;
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
    }

    public void ResetTabs()
    {
        foreach(TabButtonScript button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            //button.background.sprite = spriteTabIdle;
            //button.background.color = colorTabIdle;
            button.textBox.color = colorTextIdle;
            button.icon.color = colorIconIdle;
        }
    }
}
