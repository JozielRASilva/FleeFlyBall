using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    //Mudar os valores do score para gol do time vermelho

    public Score _score;


    public int ScoreOne = 0;
    public int onePoint = 1;
    public int twoPoints = 2;
    public int threePoints = 3;
    public int fourPoints = 4;

    public PointDetection _pointDetection;

    public UnityAction OnGoalScored;

    public bool golPlayer;

    public Positions _positions;

    [Header("Audio")]
    public AudioPlayer audioPlayer;
    public string[] sounds;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            foreach(string s in sounds)
            {
                audioPlayer.PlaySound(s);
            }

            if (_pointDetection.areaUm == true) ScoreOne += onePoint;
            if (_pointDetection.areaDois == true) ScoreOne += twoPoints;
            if (_pointDetection.areaTres == true) ScoreOne += threePoints;
            if (_pointDetection.areaQuatro == true) ScoreOne += fourPoints;
            if (_pointDetection.special == true) ScoreOne ++;

            //OnGoalScored.Invoke();

            if (golPlayer)
            {
                _score.redScore = ScoreOne;
            }
            else
            {
                _score.greenScore = ScoreOne;
            }


            print("GOL!!!!!!!!11");
            Debug.Log("SCORE: " + ScoreOne.ToString());

            _positions.GolPosition();
        }
    }
}
