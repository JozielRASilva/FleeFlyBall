using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public ToggleButton musicToggle;
    public ToggleButton soundFXToggle;

    public AudioManager audioManager;

    void Start()
    {
        musicToggle.Set(audioManager.GetMusic());
        soundFXToggle.Set(audioManager.GetSoundFX());

        musicToggle.OnSwitch += SetMusic;
        soundFXToggle.OnSwitch += SetSoundFX;
    }

    void SetMusic(bool set)
    {
        audioManager.SetMusic(set);
    }

    void SetSoundFX(bool set)
    {
        audioManager.SetSoundFX(set);
    }
}
