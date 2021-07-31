using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLimits : Singleton<BlockLimits>
{

    public Vector3 Size = Vector3.one;

    [Header("Gizmos")]

    public bool ShowGismos = true;

    public Color color = Color.yellow;

    public bool IsInside(Vector3 target)
    {
        Bounds bounds = new Bounds(transform.position, Size);

        if (!bounds.Contains(target))
            return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        if (!ShowGismos)
            return;

        Gizmos.color = color;

        Gizmos.DrawWireCube(transform.position, Size);
    }
}
