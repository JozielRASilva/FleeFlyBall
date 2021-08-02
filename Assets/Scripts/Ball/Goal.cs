using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    //Mudar os valores do score para gol do time vermelho
    
    
    
    public int ScoreOne = 0;
    public int onePoint = 1;
    public int twoPoints = 2;
    public int threePoints = 3;
    public int fourPoints = 4;

    public PointDetection  _pointDetection;

    public UnityAction OnGoalScored;

    public bool GolPlayer;
    


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (_pointDetection.areaUm == true) ScoreOne += onePoint;
            if (_pointDetection.areaDois == true) ScoreOne += twoPoints;
            if (_pointDetection.areaTres == true) ScoreOne += threePoints;
            if (_pointDetection.areaQuatro == true) ScoreOne += fourPoints;
            if (_pointDetection.special == true) ScoreOne ++;

            OnGoalScored.Invoke();

            print("GOL!!!!!!!!11");
            Debug.Log("SCORE: " + ScoreOne.ToString());
        }
    }
}
