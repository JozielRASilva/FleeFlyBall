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

    private void OnDrawGizmos()
    {
        if (!ShowGismos)
            return;

        if (Application.isEditor)
            InitAreas();

        foreach (var area in areas)
        {

        }

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


public abstract class AreaBase
{
    protected virtual bool top { get; }
    protected virtual bool left { get; }

    private Vector3 _positionAjust;

    private Vector3 _size;

    public Vector3 Size { get => _size; }

    protected AreaBase(Vector3 size)
    {
        if (top)
            _positionAjust.z = Mathf.Abs(size.z / 2);
        else
            _positionAjust.z = -Mathf.Abs(size.z / 2);

        if (left)
            _positionAjust.x = Mathf.Abs(size.x / 2);
        else
            _positionAjust.x = -Mathf.Abs(size.x / 2);

        _size = size;
    }

    public Vector3 GetPosition(Vector3 center)
    {
        Vector3 value = center;

        value.x += _positionAjust.x;
        value.z += _positionAjust.z;

        return value;
    }

    public bool IsInside(Vector3 center, Vector3 target)
    {
        Bounds bounds = new Bounds(GetPosition(center), _size);

        if (!bounds.Contains(target))
            return false;

        return true;
    }

    public float GetDistance(Vector3 center, Vector3 target)
    {
        Vector3 position = GetPosition(center);

        float distance = Vector3.Distance(position, target);

        return distance;
    }


}

public class FirstArea : AreaBase
{
    public FirstArea(Vector3 _size) : base(_size)
    {
    }

    protected override bool top { get => true; }
    protected override bool left { get => true; }

}


public class SecondArea : AreaBase
{
    public SecondArea(Vector3 _size) : base(_size)
    {
    }

    protected override bool top { get => true; }
    protected override bool left { get => false; }
}


public class ThirdArea : AreaBase
{
    public ThirdArea(Vector3 _size) : base(_size)
    {
    }

    protected override bool top { get => false; }
    protected override bool left { get => true; }
}


public class FourthArea : AreaBase
{
    public FourthArea(Vector3 _size) : base(_size)
    {
    }

    protected override bool top { get => false; }
    protected override bool left { get => false; }
}