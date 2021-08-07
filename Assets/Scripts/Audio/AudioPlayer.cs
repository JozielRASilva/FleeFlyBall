using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlaySound(string name)
    {
        audioManager.Play(name);
    }

    public Sound GetSound(string name)
    {
        return audioManager.GetSound(name);
    }
}
