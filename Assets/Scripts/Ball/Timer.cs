using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int startingTime = 180;
    public int remainingTime;
    public int timeInMins;
    public int timeInSecs;
    float referenceTime;
    public Text tempo;
    public GameObject goal;
    void Start()
    {
        remainingTime = startingTime;
        referenceTime = Time.time;
    }


    void LateUpdate()
    {
        if (referenceTime + 1 <= Time.time)
        {
            referenceTime = Time.time;
            remainingTime--;
        }

        timeInMins = (int)remainingTime / 60;
        timeInSecs = (int)remainingTime % 60;

        tempo.text = "timer : " + timeInMins.ToString() + " : " + timeInSecs.ToString();

        if (remainingTime <= 0)
        {
            if (goal.GetComponent<Goal>().ScoreOne > goal.GetComponent<Goal>().ScoreTwo)
            {
                print("TEAM ONE WINS");
            }
            if (goal.GetComponent<Goal>().ScoreOne < goal.GetComponent<Goal>().ScoreTwo)
            {
                print("TEAM TWO WINS");
            }

        }
    }

}
