using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    public Sound[] sounds;

    private AudioMixer audioMixer;
    private AudioMixerGroup masterGroup;
    private AudioMixerGroup musicGroup;
    private AudioMixerGroup ambientGroup;
    private AudioMixerGroup sfxGroup;
    private AudioMixerGroup uiGroup;

    public static SoundManager Instance { get; private set;  }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);

            //Load AudioMixer
            audioMixer = Resources.Load<AudioMixer>("Audio/NewAudioMixer");

            //Find AudioMixerGroup you want to load
            AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master/Music");
            musicGroup = audioMixGroup[0];
            audioMixGroup = audioMixer.FindMatchingGroups("Master/Ambient");
            ambientGroup = audioMixGroup[0];
            audioMixGroup = audioMixer.FindMatchingGroups("Master/SFX");
            sfxGroup = audioMixGroup[0];
            audioMixGroup = audioMixer.FindMatchingGroups("Master/UI");
            uiGroup = audioMixGroup[0];
            
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
      
                s.source.clip = s.clips[0];
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.spatialBlend = s.spatialBlend;
                s.source.minDistance = s.minDist3D;
                s.source.maxDistance = s.maxDist3D;
                s.source.mute = s.mute;
                s.source.loop = s.loop;
                s.source.playOnAwake = s.playOnAwake;

                switch (s.type)
                {
                    case SoundType.MUSIC: s.source.outputAudioMixerGroup = musicGroup; break;
                    case SoundType.AMBIENT: s.source.outputAudioMixerGroup = ambientGroup; break;
                    case SoundType.SFX: s.source.outputAudioMixerGroup = sfxGroup; break;
                    case SoundType.UI: s.source.outputAudioMixerGroup = uiGroup; break;
                }
            }

            // | Listen To
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(LevelManager.Instance != null)
        {
            string currentScene = LevelManager.Instance.GetCurrentSceneName();
            if (currentScene == "Menu")
            {
                PlaySound("menu_music");
            }
            else if (currentScene == "Tutorial Safe")
            {
                PlaySound("ingame_music");
            }else if(currentScene == "Level1")
            {
                PlaySound("level_1_music");
            }
        }
    }

    //How to use : SoundManager.Instance.PlaySound(name);
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !\nCheck name spelling");
            return;
        }
        if (s.clips.Length > 1)
        {
            int clipNumber = UnityEngine.Random.Range(0, s.clips.Length);
            s.source.clip = s.clips[clipNumber];
        }
        s.source.Play();
    }

    public void StopAllSound()
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
    public void StopAllSoundWithFade()
    {
        foreach(Sound s in sounds)
        {
            StartCoroutine(FadeSound(s));
        }
    }
    public void StopSoundWithFade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(FadeSound(s));
    }

    private IEnumerator FadeSound(Sound s)
    {
        float timer = 0;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeDuration;
            s.source.volume = Mathf.Lerp(1, 0, ratio);
            yield return null;
        }

        s.source.Stop();
        s.source.volume = 1;
    }

    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("VolumeMaster", Mathf.Log10(sliderValue) * 20);
    }
    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("VolumeMusic", Mathf.Log10(sliderValue) * 20);
    }
    public void SetAmbientVolume(float sliderValue)
    {
        audioMixer.SetFloat("VolumeAmbient", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("VolumeSFX", Mathf.Log10(sliderValue) * 20);
    }
    public void SetUIVolume(float sliderValue)
    {
        audioMixer.SetFloat("VolumeUI", Mathf.Log10(sliderValue) * 20);
    }
    public void ChangeMute(bool mute)
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = mute;
        }
    }

    public bool IsSoundPlaying(string sound)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == sound)
            {
                return s.source.isPlaying;
            }
        }

        return false;
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
    AMBIENT,
    UI,
}
