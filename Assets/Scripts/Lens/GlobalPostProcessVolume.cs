using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalPostProcessVolume : MonoBehaviour
{
    private VolumeProfile vp;

    private Color _normalColorFilter = Color.white;
    [SerializeField] private Color _UVColorFilter;
    [SerializeField] private Color _IRColorFilter;
    [SerializeField] private Color _XRAYColorFilter;
    [SerializeField] private Color _NIGHTSHOTColorFilter;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<Volume>().profile; //get VolumeProfile from VolumeComponent 
    }

    public Color GetLensColor(LensEnum lens)
    {
        switch (lens)
        {
            case LensEnum.NONE:         return _normalColorFilter;
            case LensEnum.UV:           return _UVColorFilter;
            case LensEnum.IR:           return _IRColorFilter;
            case LensEnum.XRAY:         return _XRAYColorFilter;
            case LensEnum.NIGHTSHOT:    return _NIGHTSHOTColorFilter;   
        }

        return _normalColorFilter;
    }
    //public Color GetNormalColor()
    //{
    //    return _normalColorFilter;
    //}
    //public Color GetUVColor()
    //{
    //    return _UVColorFilter;
    //}
    //public Color GetIRColor()
    //{
    //    return _IRColorFilter;
    //}
    //public Color GetXRAYColor()
    //{
    //    return _XRAYColorFilter;
    //}
    //public Color GetNIGHTSHOTColor()
    //{
    //    return _NIGHTSHOTColorFilter;
    //}

    public void ChangeColorToNormal()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = _normalColorFilter;
            }

            if (vc is FilmGrain)
            {
                FilmGrain filmGrain = vc as FilmGrain;
                filmGrain.active = false;
            }

            if (vc is Vignette)
            {
                Vignette vignette = vc as Vignette;
                vignette.active = false;
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

            if (vc is FilmGrain)
            {
                FilmGrain filmGrain = vc as FilmGrain;
                filmGrain.active = true;
                filmGrain.type.value = FilmGrainLookup.Thin2;
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

            if (vc is FilmGrain)
            {
                FilmGrain filmGrain = vc as FilmGrain;
                filmGrain.active = true;
                filmGrain.type.value = FilmGrainLookup.Medium5;
            }
        }
    }

    public void ChangeColorToXRAY()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = _XRAYColorFilter;
            }

            if (vc is FilmGrain)
            {
                FilmGrain filmGrain = vc as FilmGrain;
                filmGrain.active = true;
                filmGrain.type.value = FilmGrainLookup.Thin2;
            }
        }
    }

    public void ChangeColorToNIGHTSHOT()
    {
        foreach (VolumeComponent vc in vp.components)
        {
            if (vc is ColorAdjustments)
            {
                ColorAdjustments colorAdjustments = vc as ColorAdjustments;
                colorAdjustments.colorFilter.value = _NIGHTSHOTColorFilter;
            }

            if (vc is FilmGrain)
            {
                FilmGrain filmGrain = vc as FilmGrain;
                filmGrain.active = true;
                filmGrain.type.value = FilmGrainLookup.Large02;
            }

            if (vc is Vignette)
            {
                Vignette vignette = vc as Vignette;
                vignette.active = true;
            }
        }
    }
}
