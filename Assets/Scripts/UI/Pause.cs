using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused;

    void Start()
    {
        PauseGame(false);
    }

    void Update()
    {

    }

    public void PauseGame(bool set)
    {
        if (set)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        paused = set;
    }
}
