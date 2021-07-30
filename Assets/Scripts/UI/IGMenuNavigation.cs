using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMenuNavigation : MonoBehaviour
{
    public Pause pauseScript;

    public AudioPlayer audioPlayer;

    public GameObject pause;
    public GameObject giveUpConfirmation;
    public GameObject matchEnd;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (matchEnd.activeSelf)
            {
                return;
            }
            
            if (giveUpConfirmation.activeSelf)
            {
                giveUpConfirmation.SetActive(false);

                audioPlayer.PlaySound("Return");
            }
            else if (pause.activeSelf)
            {
                pause.SetActive(false);

                pauseScript.PauseGame(false);

                audioPlayer.PlaySound("Return");
            }
            else
            {
                pause.SetActive(true);

                pauseScript.PauseGame(true);
            }
        }
    }
}
