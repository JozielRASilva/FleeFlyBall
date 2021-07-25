using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostBall : MonoBehaviour
{
    public GameObject bola;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeamBallPosetion()
    {
        if (bola.GetComponent<Ball>().firstTeam == true)
        {
            bola.GetComponent<Ball>().firstTeam = false;
            bola.GetComponent<Ball>().firstTeam = true;
        }
    }
}
