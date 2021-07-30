using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    public AudioPlayer audioPlayer;

    public GameObject mainMenu;
    public GameObject quitConfirmation;
    public GameObject settings;
    public GameObject seeConfirmation;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (seeConfirmation.activeSelf)
            {
                seeConfirmation.SetActive(false);

                audioPlayer.PlaySound("Return");
            }
            else if (settings.activeSelf)
            {
                settings.SetActive(false);

                audioPlayer.PlaySound("Return");
            }
            else if (quitConfirmation.activeSelf)
            {
                quitConfirmation.SetActive(false);

                audioPlayer.PlaySound("Return");
            }
            else
            {
                quitConfirmation.SetActive(true);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
