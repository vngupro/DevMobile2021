using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoEffect : MonoBehaviour
{
    [SerializeField] private Animation anim;
    public void TakeAShot()
    {
        anim.Play("Flash");
    }
}
