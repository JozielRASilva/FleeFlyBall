using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Goal goal1;
    public Goal goal2;

    public Timer timer;

    public Text score1Display;
    public Text score2Display;
    public Text timerDisplay;
    public GameObject[] timeCourseDisplays;

    string fixedTimeInMins;
    string fixedTimeInSecs;

    int timeCourse;

    void Start()
    {        
        timeCourse = 0;

        if (goal1 != null)
        {
            goal1.OnGoalScored += DisplayScore1;

            DisplayScore1();
        }
        if (goal2 != null)
        {
            goal2.OnGoalScored += DisplayScore2;

            DisplayScore2();
        }
        if (timer != null)
        {
            timer.OnTimeChanged += CheckTimeCourse;
            timer.OnTimeChanged += DisplayTimer;

            DisplayTimer();
        }
        
        DisplayTimeCourse();
    }

    void DisplayScore1()
    {
        score1Display.text = goal1.ScoreOne.ToString();
    }

    void DisplayScore2()
    {
        score1Display.text = goal2.ScoreOne.ToString();
    }

    void DisplayTimer()
    {
        if (timer.timeInMins < 10)
        {
            fixedTimeInMins = "0" + timer.timeInMins;
        }
        else
        {
            fixedTimeInMins = timer.timeInMins.ToString();
        }

        if (timer.timeInSecs < 10)
        {
            fixedTimeInSecs = "0" + timer.timeInSecs;
        }
        else
        {
            fixedTimeInSecs = timer.timeInSecs.ToString();
        }

        timerDisplay.text = fixedTimeInMins + ":" + fixedTimeInSecs;
    }

    void CheckTimeCourse()
    {
        if (timer.timeInMins % 10 == 0 && timer.timeInMins != 0 && timer.timeInSecs == 0)
        {
            timeCourse++;

            DisplayTimeCourse();
        }
    }

    void DisplayTimeCourse()
    {
        for (int i = 0; i < timeCourseDisplays.Length; i++)
        {
            timeCourseDisplays[i].SetActive(i == timeCourse);
        }
    }
}
