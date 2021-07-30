using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
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
            }
            else if (settings.activeSelf)
            {
                settings.SetActive(false);
            }
            else if (quitConfirmation.activeSelf)
            {
                quitConfirmation.SetActive(false);
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
