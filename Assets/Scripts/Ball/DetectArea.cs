using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject field;

   

    void Start()
    {
            
        
    }

    void Update()
    {
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            field.GetComponent<AreaData>().areaOne = false;
         
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            field.GetComponent<AreaData>().areaOne = true;
            print("Area1");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }
}
