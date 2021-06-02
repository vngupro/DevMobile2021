using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomEffect : MonoBehaviour
{
    public GameObject zoomCircles;
    [SerializeField] private GameObject outterCircle;
    [SerializeField] private GameObject middleCircle;
    [SerializeField] private GameObject insideCircle;

    [SerializeField] private float rotationSpeed = 2.0f;

    [Header("Debug")]
    [SerializeField] private Coroutine zoomOutCoroutine;
    [SerializeField] private Coroutine zoomInCoroutine;

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
        zoomInCoroutine = StartCoroutine(ZoomInCoroutine());
    }

    public void ZoomOutAnimation()
    {
        zoomOutCoroutine = StartCoroutine(ZoomOutCoroutine());
    }

    private IEnumerator ZoomInCoroutine()
    {
        while (true)
        {
            middleCircle.transform.Rotate(0, 0, -rotationSpeed, Space.Self);
            yield return null;
        }
    }

    private IEnumerator ZoomOutCoroutine()
    {
        while (true)
        {
            middleCircle.transform.Rotate(0, 0, rotationSpeed, Space.Self);
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
    }
}
