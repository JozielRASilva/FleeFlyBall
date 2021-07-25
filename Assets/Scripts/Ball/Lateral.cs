using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lateral : MonoBehaviour
{
    public GameObject ball;
    public GameObject lostBall;
    public GameObject area;

    public Vector3 areaUm;
    public Vector3 areaDois;
    public Vector3 areaTres;
    public Vector3 areaQuatro;

    // Update is called once per frame
    void Update()
    {
          
    }
    public void Cobrar()
    {
        lostBall.GetComponent<LostBall>().TeamBallPosetion();
        if (area.GetComponent<AreaDetect>().AreaRight == true)
        {
            Right();
        }
        if (area.GetComponent<AreaDetect>().AreaRight == false)
        {
            Left();
        }
    }
    public void Left()
    {

    }
    public void Right()
    {

    }
}
