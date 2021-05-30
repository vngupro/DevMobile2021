using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSourceImage : MonoBehaviour
{
    public Sprite spriteMute;
    public Sprite spriteUnmute;

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void ChangeImage(bool mute)
    {
        if (mute)
        {
            image.sprite = spriteMute;
        }
        else
        {
            image.sprite = spriteUnmute;
        }
    }
}
