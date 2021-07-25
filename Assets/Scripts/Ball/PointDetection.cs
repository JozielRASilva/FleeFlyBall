using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDetection : MonoBehaviour
{
    public bool areaUm, areaDois, areaTres, areaQuatro, special;
    
    [Header("Detect field")]
    [SerializeField]
    private AreaDetect _areaDetect;

    public GameObject ball;

    public Ball  _ball;

    private void Awake()
    {
        _ball = ball.GetComponent<Ball>();
    }



    public void DetectArea()
    {
        special = false;

        if (_areaDetect.AreaOne == true)
        {
            areaUm = true;
            areaDois = false;
            areaTres = false;
            areaQuatro = false;
        }
        if (_areaDetect.AreaTwo == true)
        {
            areaUm = false;
            areaDois = true;
            areaTres = false;
            areaQuatro = false;
        }
        if (_areaDetect.AreaThree == true)
        {
            areaUm = false;
            areaDois = false;
            areaTres = true;
            areaQuatro = false;
        }
        if (_areaDetect.AreaFour == true)
        {
            areaUm = false;
            areaDois = false;
            areaTres = false;
            areaQuatro = true;
        }

        if (_ball.GetKickType() == Ball.KickType.SPECIAL)
        {
            special = true;
        }
        

        
    }
}
