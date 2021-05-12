using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalPostProcessVolume : MonoBehaviour
{
    private VolumeProfile vp;

    [SerializeField] private Color normalColorFilter;
    [SerializeField] private Color UVColorFilter;
    [SerializeField] private Color IRColorFilter;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<Volume>().profile; //get VolumeProfile from VolumeComponent 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorToNormal()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = normalColorFilter;
            }
        }
    }

    public void ChangeColorToUV()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = UVColorFilter;
            }
        }
    }
    
    public void ChangeColorToIR()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = IRColorFilter;
            }
        }
    }
    

}
