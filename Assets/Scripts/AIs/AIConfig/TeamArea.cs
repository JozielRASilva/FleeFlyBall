using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamArea : MonoBehaviour
{
    [SerializeField]
    private GameObject center;


    public Vector3 areasSize = Vector3.one;

    [Header("Gizmos")]

    public bool ShowGismos = true;

    public Color color = Color.blue;

    private const int AreaNumber = 4;

    [SerializeField]
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

    public void OverrideSizes(Vector3 newSize)
    {
        foreach (var area in areas)
        {
            area.ApplySize(newSize);
        }
    }

    public void ResetOverrideSize()
    {
        foreach (var area in areas)
        {
            area.ApplySize(areasSize);
        }
    }

    public void ChangeCenter(GameObject newCenter = null)
    {
        center = newCenter;
    }

    public Vector3 GetCenter()
    {
        return center ? center.transform.position : Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (!ShowGismos)
            return;

        Vector3 _center = center ? center.transform.position : this.transform.position;

        foreach (var area in areas)
        {

            Gizmos.color = color;


            Gizmos.DrawWireCube(area.GetPosition(_center), area.Size);
        }



    }

    private void OnValidate()
    {
        InitAreas();
    }

}

