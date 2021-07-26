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

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }

        if (!PlayerPrefs.HasKey("Sound FX"))
        {
            PlayerPrefs.SetInt("Sound FX", 1);
        }

        SetMusic(PlayerPrefs.GetInt("Music") == 1);

        SetSoundFX(PlayerPrefs.GetInt("Sound FX") == 1);

        foreach (Sound s in music)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = maxMusicVolume * s.volume;
        }

        foreach (Sound s in soundFX)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = maxSoundFXVolume * s.volume;
        }
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
    }

    void PlayMusic(string name)
    {
        Array.Find(music, sound => sound.name == name).audioSource.Play();
    }

    void PlaySoundFX(string name)
    {
        Array.Find(soundFX, sound => sound.name == name).audioSource.Play();
    }
}
