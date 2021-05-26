using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Button buttonNotes;
    public Button buttonLens;
    public Sprite spriteNotesOpen, spriteNotesClose;
    public Sprite spriteLensNormal, spriteLensUV, spriteLensXRay, spriteLensIR, spirteLensNightVision;
    public Color colorLensNormal, colorLensUV, colorLensXRay, colorLensIR, colorLensNightVision;

    
    public void NotesOpen()
    {
        buttonNotes.image.sprite = spriteNotesOpen;
    }

    public void NotesClose()
    {
        buttonNotes.image.sprite = spriteNotesClose;
    }

    public void LensOpen()
    {

    }

    public void LensClose()
    {

    }

    public void LensSelected()
    {
        buttonLens.image.sprite = spriteLensIR;
    }
}
