using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderVolumeScript : MonoBehaviour
{
    public SoundType type;

    private SoundManager soundManager;
    private Sound[] sounds;
    private void Start()
    {
        soundManager = SoundManager.Instance;
        sounds = soundManager.sounds;
    }

    public void SetVolume(float sliderValue)
    {
        foreach (Sound s in sounds)
        {
            if (s.type == type)
            {
                s.volume = sliderValue;
                s.source.volume = sliderValue;
                Debug.Log("Set Volume of " + type);
            }
        }
    }
}
