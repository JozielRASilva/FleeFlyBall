using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    public UnityAction OnTimeChanged;
    
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
            remainingTime++;

            OnTimeChanged.Invoke();
        }

        timeInMins = (int)remainingTime / 60;
        timeInSecs = (int)remainingTime % 60;

        tempo.text = "timer : " + timeInMins.ToString() + " : " + timeInSecs.ToString();

        if (remainingTime == secondTime)
        {
            //if (goal.GetComponent<Goal>().ScoreOne > goal.GetComponent<Goal>().ScoreTwo)
          //  {
           //     print("TEAM ONE WINS");
         //   }
           // if (goal.GetComponent<Goal>().ScoreOne < goal.GetComponent<Goal>().ScoreTwo)
           // {
         //       print("TEAM TWO WINS");
          //  }

        }
        if(remainingTime == thirdTime)
        {

        }
        if(remainingTime == fourthTime)
        {

        }
        if(remainingTime == endTime)
        {

        }
        
    }

}
