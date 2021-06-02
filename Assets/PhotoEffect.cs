using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoEffect : MonoBehaviour
{
    [SerializeField]
    Animation anim;
    public AudioClip Photo;

    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void TakeAShot()
    {
        StartCoroutine("CaptureIt");
    }

    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        anim.Play("Flash");
        source.PlayOneShot(Photo);
        anim.Play("");
    }
}
