using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Image))]
public class TabButtonScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    [Header("Debug")]
    public Image background;
    public TMP_Text textBox;
    public Transform iconTransform;
    public Image icon;
    public Transform arrowTransform;
    public Image arrow;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        textBox = GetComponentInChildren<TMP_Text>();
        iconTransform = this.gameObject.transform.Find("Image");
        icon = iconTransform.gameObject.GetComponent<Image>();
        arrowTransform = this.gameObject.transform.Find("Arrow");
        if (arrowTransform != null)
        {
            arrow = iconTransform.gameObject.GetComponent<Image>();
        }
        tabGroup.Subscribe(this);
    }
}
