using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int ScoreOne = 0;
    
    public int onePoint = 1;
    public int twoPoints = 2;
    public int threePoints = 3;
    public int fourPoints = 4;

    public GameObject pointDetect;
    
   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (pointDetect.GetComponent<PointDetection>().areaUm == true) ScoreOne++;
            if (pointDetect.GetComponent<PointDetection>().areaDois == true) ScoreOne += twoPoints;
            if (pointDetect.GetComponent<PointDetection>().areaTres == true) ScoreOne += threePoints;
            if (pointDetect.GetComponent<PointDetection>().areaQuatro == true) ScoreOne += fourPoints;



            print("GOL!!!!!!!!11");
            Debug.Log("SCORE: " + ScoreOne.ToString());
        }
    }
}
