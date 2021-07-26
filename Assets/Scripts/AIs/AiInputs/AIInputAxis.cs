using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.Linq;
[System.Serializable]
public class AIInputAxis : AIInputBase<Vector2>
{
    private Vector3 _lastTarget;

    private NavMeshPath _path;

    private List<Vector3> _lastCorners;

    private bool _started;

    private bool clean;

    public AIInputAxis(NavMeshPath path)
    {
        _path = path;
    }


    private void UpdatePath(Vector3 current, Vector3 target)
    {
        if (clean)
            if (target == _lastTarget && _started)
                return;

        NavMesh.CalculatePath(current, target, NavMesh.AllAreas, _path);

        _lastTarget = target;

        _lastCorners = _path.corners.ToList();


        if (_lastCorners.Count > 0)
            _lastCorners.RemoveAt(0);


        _started = true;
    }

    public override Vector2 GetValue(AICharacterBase AI)
    {
        Vector2 value = Vector2.zero;
        
        if (CanPerformInput)
        {
            UpdatePath(AI.transform.position, AI.GetTarget());

            if (clean)
                CheckAchieve(AI.transform.position);

            value = GetDirection(AI.transform.position);
        }

        return value;
    }

    private Vector2 GetDirection(Vector3 current)
    {
        Vector2 dir = Vector2.zero;

        Vector3 _current = current;
        current.y = 0;

        if (_lastCorners.Count > 0)
        {

            Vector3 goTo = _lastCorners[0];
            goTo.y = 0;

            Vector3 normalized = (goTo - _current).normalized;

            dir.x = normalized.x;
            dir.y = normalized.z;
        }

        return dir;
    }

    private void CheckAchieve(Vector3 current)
    {
        if (_lastCorners.Count == 0)
            return;

        if (Vector3.Distance(current, _lastCorners[0]) < 0.2f)
        {
            _lastCorners.RemoveAt(0);
        }
    }

}
