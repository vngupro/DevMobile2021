using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZoomEffect : MonoBehaviour
{
    public GameObject zoomCircles;
    public TMP_Text zoomText;
    [SerializeField] private GameObject outterCircle;
    [SerializeField] private GameObject middleCircle;
    [SerializeField] private GameObject insideCircle;
    public Image graduatedBar;


    [SerializeField] private float rotationSpeed = 2.0f;

    [Header("Debug")]
    [SerializeField] private Coroutine zoomOutCoroutine;
    [SerializeField] private Coroutine zoomInCoroutine;
    [SerializeField] private bool isZoomingOut = false;
    [SerializeField] private bool isZoomingIn = false;
    public bool IsZoomingOut { get => this.isZoomingOut; set => this.isZoomingOut = value; }
    public bool IsZoomingIn { get => this.isZoomingIn; set => this.isZoomingIn = value; }

    public void ActivateCrosshair()
    {
        zoomCircles.SetActive(true);
    }

    public void DeactivateCrossHair()
    {
        zoomCircles.SetActive(false);
    }

    public void ZoomInAnimation()
    {
        isZoomingIn = true;
        zoomInCoroutine = StartCoroutine(ZoomInCoroutine());
    }

    public void ZoomOutAnimation()
    {
        isZoomingOut = true;
        zoomOutCoroutine = StartCoroutine(ZoomOutCoroutine());
    }

    private IEnumerator ZoomInCoroutine()
    {
        while (true)
        {
            middleCircle.transform.Rotate(0, 0, rotationSpeed, Space.Self);
            yield return null;
        }
    }

    private IEnumerator ZoomOutCoroutine()
    {
        while (true)
        {
            middleCircle.transform.Rotate(0, 0, -rotationSpeed, Space.Self);
            yield return null;
        }
    }

    public void StopAllAnimation()
    {
        if(zoomOutCoroutine != null)
        {
            StopCoroutine(zoomOutCoroutine);
        }

        if(zoomInCoroutine != null)
        {
            StopCoroutine(zoomInCoroutine);
        }

        isZoomingIn = false;
        isZoomingOut = false;
    }
}
