using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int ScoreOne = 0;
    public int ScoreTwo = 0;

    
   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ScoreOne++;
            print("GOL!!!!!!!!11");
            Debug.Log("SCORE: " + ScoreOne.ToString());
        }
    }
}
