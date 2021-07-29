using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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