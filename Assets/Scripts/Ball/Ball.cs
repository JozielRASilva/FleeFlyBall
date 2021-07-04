using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 chute = new Vector3(500, 500, 0);

    public bool onPlayer;

    public bool inField;

    
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Chutar();
        }

        ControleFisica();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            print("tocou no chão");
        }
                
    }

    private void OnTriggerExit(Collider other)   {
        if (other.gameObject.CompareTag("InField"))
        {
            inField = false;
            print("Fora da Quadra");
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InField"))
        {
            inField = true;
            print("Dentro da Quadra");
        }
    }



   



    public void Chutar()
    {
        onPlayer = false;
        gameObject.GetComponent<Rigidbody>().AddForce(chute);
    }

    public void ControleFisica()
    {
        if (!onPlayer)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}
