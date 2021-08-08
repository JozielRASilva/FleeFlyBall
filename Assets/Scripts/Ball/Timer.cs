using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int startingTime = 0;
    public int remainingTime;
    public int timeInMins;
    public int timeInSecs;
    public int secondTime;
    public int thirdTime;
    public int fourthTime;
    public int endTime;

    float referenceTime;
    public Text tempo;

    public Positions _positions;

    public UnityAction OnTimeChanged;

    public Score _score;

    public GameObject redWin;
    public GameObject greeWin;
    public GameObject drawGame;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioPlayer audioPlayer;
    public string sound;

    void Start()
    {
        remainingTime = startingTime;
        referenceTime = Time.time;

        audioPlayer.PlaySound(sound);
    }


    void LateUpdate()
    {
        if (referenceTime + 1 <= Time.time)
        {
            referenceTime = Time.time;
            remainingTime++;

            OnTimeChanged.Invoke();
        }

        timeInMins = (int)remainingTime / 60;
        timeInSecs = (int)remainingTime % 60;

        if (tempo)
            tempo.text = "timer : " + timeInMins.ToString() + " : " + timeInSecs.ToString();

        if (remainingTime == secondTime)
        {
            _positions.GolPosition();
            if (audioManager)
                if (!audioManager.GetSound(sound).audioSource.isPlaying)
                {
                    audioPlayer.PlaySound(sound);
                }

        }
        if (remainingTime == thirdTime)
        {
            _positions.GolPosition();

            if (audioManager)
                if (!audioManager.GetSound(sound).audioSource.isPlaying)
                {
                    audioPlayer.PlaySound(sound);
                }
        }
        if (remainingTime == fourthTime)
        {
            _positions.GolPosition();
            if (audioManager)
                if (!audioManager.GetSound(sound).audioSource.isPlaying)
                {
                    audioPlayer.PlaySound(sound);
                }
        }
        if (remainingTime == endTime)
        {
            if (_score.redScore > _score.greenScore)
            {
                Time.timeScale = 0;
                redWin.SetActive(true);
            }
            if (_score.redScore < _score.greenScore)
            {
                Time.timeScale = 0;
                greeWin.SetActive(true);
            }
            if (_score.redScore == _score.greenScore)
            {
                Time.timeScale = 0;
                drawGame.SetActive(true);
            }

        }

    }



}
