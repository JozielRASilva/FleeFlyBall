using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]

    [Range(0f, 1f)]
    public float maxMusicVolume;

    float musicVolume;

    public Sound[] music;

    [Header("Sound FX")]

    [Range(0f, 1f)]
    public float maxSoundFXVolume;

    float soundFXVolume;

    public Sound[] soundFX;

    [Header("Play on Awake")]
    public string playOnAwake;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            musicVolume = maxMusicVolume;
        }
        else
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                musicVolume = maxMusicVolume;
            }
            else
            {
                musicVolume = 0;
            }
        }

        if (!PlayerPrefs.HasKey("Sound FX"))
        {
            soundFXVolume = maxSoundFXVolume;
        }
        else
        {
            if (PlayerPrefs.GetInt("Sound FX") == 1)
            {
                soundFXVolume = maxSoundFXVolume;
            }
            else
            {
                soundFXVolume = 0;
            }
        }

        foreach (Sound s in music)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = musicVolume * s.volume;

            s.audioSource.loop = s.loop;
        }

        foreach (Sound s in soundFX)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = soundFXVolume * s.volume;

            s.audioSource.loop = s.loop;
        }

        Play(playOnAwake);
    }

    void SetMusic(bool set)
    {
        if (set)
        {
            musicVolume = maxMusicVolume;

            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            musicVolume = 0f;

            PlayerPrefs.SetInt("Music", 0);
        }

        foreach (Sound s in music)
        {
            s.audioSource.volume = musicVolume * s.volume;
        }
    }

    void SetSoundFX(bool set)
    {
        if (set)
        {
            soundFXVolume = maxSoundFXVolume;

            PlayerPrefs.SetInt("Sound FX", 1);
        }
        else
        {
            soundFXVolume = 0f;

            PlayerPrefs.SetInt("Sound FX", 0);
        }

        foreach (Sound s in soundFX)
        {
            s.audioSource.volume = soundFXVolume * s.volume;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);

        if (s == null)
        {
            s = Array.Find(soundFX, sound => sound.name == name);

            if (s == null) return;
        }

        s.audioSource.Play();
    }
}
