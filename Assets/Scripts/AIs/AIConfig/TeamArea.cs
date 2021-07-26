using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamArea : MonoBehaviour
{
    public GameObject center;

    public GameObject teste;

    public Vector3 areasSize = Vector3.one;

    public bool ShowGismos = true;

    private const int AreaNumber = 4;

    private List<AreaBase> areas = new List<AreaBase>();

    private void Awake()
    {
        InitAreas();
    }

    private void InitAreas()
    {
        areas = new List<AreaBase>();

        areas.Add(new FirstArea(areasSize));

        areas.Add(new SecondArea(areasSize));

        areas.Add(new ThirdArea(areasSize));

        areas.Add(new FourthArea(areasSize));
    }

    public List<AreaBase> GetAreas()
    {
        return areas;
    }

    public void ChangeCenter(GameObject newCenter = null)
    {
        center = newCenter;
    }

    private void OnDrawGizmos()
    {
        if (!ShowGismos)
            return;

        if (Application.isEditor)
            InitAreas();

        Vector3 _center = center ? center.transform.position : this.transform.position;

        foreach (var area in areas)
        {
            if (teste)
                if (area.IsInside(_center, teste.transform.position))
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.blue;
            else
                Gizmos.color = Color.blue;


            Gizmos.DrawWireCube(area.GetPosition(_center), area.Size);
        }



    }

}

