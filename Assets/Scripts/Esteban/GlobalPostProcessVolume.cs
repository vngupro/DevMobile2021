using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalPostProcessVolume : MonoBehaviour
{
    private VolumeProfile vp;

    private Color _normalColorFilter = Color.white;
    [SerializeField] private Color _UVColorFilter;
    [SerializeField] private Color _IRColorFilter;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<Volume>().profile; //get VolumeProfile from VolumeComponent 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color GetNormalColor()
    {
        return _normalColorFilter;
    }
    public Color GetUVColor()
    {
        return _UVColorFilter;
    }
    public Color GetIRColor()
    {
        return _IRColorFilter;
    }

    public void ChangeColorToNormal()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = _normalColorFilter;
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
                colorAdjustments.colorFilter.value = _UVColorFilter;
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
                colorAdjustments.colorFilter.value = _IRColorFilter;
            }
        }
    }
    

}
