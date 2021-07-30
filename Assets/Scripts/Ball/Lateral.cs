using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lateral : MonoBehaviour
{
    public GameObject ball;
    public GameObject lostBall;
    public GameObject area;
    public GameObject quadraDetect;
    

    private Quadra  _quadraDetect;
    private Ball _ball;
    private AreaDetect _areaDetect;

    



    // Update is called once per frame

    private void Awake()
    {
        _quadraDetect = quadraDetect.GetComponent<Quadra>();
        _ball = ball.GetComponent<Ball>();
        _areaDetect = area.GetComponent<AreaDetect>();
    }

    void Update()
    {
        if (!_ball.inField && _ball.grounded)
        {
            GetBallPosition();
        }
    }
    public void GetBallPosition()
    {
       // lostBall.GetComponent<LostBall>().TeamBallPosetion();
        if (_areaDetect.AreaRight)
        {
            Right();
            if (_areaDetect.AreaOne)
            {
                _areaDetect.AreaOne = true;
                print("Lateral na area um");
                //Posição onde a lateral é cobrada pelo time que não chutou ela por ultimo
            }
            if (_areaDetect.AreaTwo)
            {
                _areaDetect.AreaTwo = true;
            }
            if (_areaDetect.AreaThree)
            {
                _areaDetect.AreaThree = true;
            }
            if (_areaDetect.AreaFour)
            {
                _areaDetect.AreaFour = true;
                print("Lateral na Area Quatro");
            }



        }

        if (!_areaDetect.AreaRight)
        {
            Left();
            if (_areaDetect.AreaOne)
            {
                _areaDetect.AreaOne = true;
                //Posição onde a lateral é cobrada pelo time que não chutou ela por ultimo
            }
            if (_areaDetect.AreaTwo)
            {
                _areaDetect.AreaTwo = true;
            }
            if (_areaDetect.AreaThree)
            {
                _areaDetect.AreaThree = true;
            }
            if (_areaDetect.AreaFour)
            {
                _areaDetect.AreaFour = true;
            }
        }
    }
    public void BolaFora()
    {
        
    }
    public void Left()
    {

    }
    public void Right()
    {

    }
}
