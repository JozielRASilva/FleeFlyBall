using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetect : MonoBehaviour
{
    public GameObject Bola;
    public Vector3 primeiraArea;
    public Vector3 segundaArea;
    public Vector3 terceiraArea;
    
    
    public bool AreaOne;
    public bool AreaTwo;
    public bool AreaThree;
    public bool AreaFour;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Bola.transform.position.x < primeiraArea.x)
        {
            AreaOne = true;
            AreaTwo = false;
            AreaThree = false;
            AreaFour = false;
        }
        if (Bola.transform.position.x > primeiraArea.x && Bola.transform.position.x < segundaArea.x)
        {
            AreaOne = false;
            AreaTwo = true;
            AreaThree = false;
            AreaFour = false;
        }
        if (Bola.transform.position.x > segundaArea.x && Bola.transform.position.x < terceiraArea.x)
        {
            AreaOne = false;
            AreaTwo = false;
            AreaThree = true;
            AreaFour = false;
        }
        if (Bola.transform.position.x >  terceiraArea.x)
        {
            AreaOne = false;
            AreaTwo = false;
            AreaThree = false;
            AreaFour = true;
        }
    }
}
