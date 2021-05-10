using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    private AudioMixer mixer;

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("Audio/NewAudioMixer");
                s.source.clip = s.clips[0];
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.spatialBlend = s.spatialBlend;
                s.source.minDistance = s.minDist3D;
                s.source.maxDistance = s.maxDist3D;
                s.source.mute = s.mute;
                s.source.loop = s.loop;
                s.source.playOnAwake = s.playOnAwake;
            }

            mixer = Resources.Load<AudioMixer>("Audio/NewAudioMixer");

            // | Listeners
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //How to use : FindObjectOfType<SoundManager>().PlaySound(name);
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + "not found !\nCheck name spelling");
            return;
        }
        if (s.clips.Length > 1)
        {
            int clipNumber = UnityEngine.Random.Range(0, s.clips.Length);
            s.source.clip = s.clips[clipNumber];
        }
        s.source.Play();
    }

    private void StopAllSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SetMasterVolume(float sliderValue)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Log10(sliderValue) * 20);
        Debug.Log("Set Master Volume");
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public SoundType type;
    //public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range(-3f, 3f)]
    public float pitch = 1.0f;
    [Range(0f, 1f)]
    public float spatialBlend = 0f;
    public float minDist3D = 1f;
    public float maxDist3D = 500f;
    public bool mute = false;
    public bool loop = false;
    public bool playOnAwake = true;

    //Anti bug : overlay in inspector
    public AudioClip[] clips;

    [HideInInspector]
    public AudioSource source;
}

public enum SoundType
{
    MUSIC,
    SFX,
    ENVIRONMENT,
    UI,
}
