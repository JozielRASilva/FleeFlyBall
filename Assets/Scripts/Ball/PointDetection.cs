using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDetection : MonoBehaviour
{
    public bool areaUm, areaDois, areaTres, areaQuatro;
    public GameObject detect;
 

    public void DetectArea()
    {

        if (detect.GetComponent<AreaDetect>().AreaOne == true)
        {
            areaUm = true;
            areaDois = false;
            areaTres = false;
            areaQuatro = false;
        }
        if (detect.GetComponent<AreaDetect>().AreaTwo == true)
        {
            areaUm = false;
            areaDois = true;
            areaTres = false;
            areaQuatro = false;
        }
        if (detect.GetComponent<AreaDetect>().AreaThree == true)
        {
            areaUm = false;
            areaDois = false;
            areaTres = true;
            areaQuatro = false;
        }
        if (detect.GetComponent<AreaDetect>().AreaFour == true)
        {
            areaUm = false;
            areaDois = false;
            areaTres = false;
            areaQuatro = true;
        }

    }
}
