using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectBlock : BTNode
{

    private TeamGroup _teamGroup;

    private TeamMember _teamMember;

    private TeamArea _teamArea;

    private TeamAreaController _teamAreaController;

    private AreaBase _currentArea;

    public BTSelectBlock(string _name, TeamGroup teamGroup, TeamMember teamMember)
    {
        name = _name;

        _teamGroup = teamGroup;

        _teamArea = _teamGroup.GetComponent<TeamArea>();

        _teamAreaController = _teamGroup.GetComponent<TeamAreaController>();

        _teamMember = teamMember;

    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (_teamAreaController)
        {
            _currentArea = _teamAreaController.GetArea(_teamMember);

            Vector3 currentPoint = bt.aICharacter.GetTarget();

            if (CanUpdateTarget(currentPoint))
            {
                UpdateTarget(bt);

                status = Status.SUCCESS;
            }


        }

        yield break;
    }

    private void UpdateTarget(BehaviourTree bt)
    {
        Vector3 point = _teamAreaController.GetRandomPointOnArea(_teamMember);

        bt.aICharacter.ChangeTarget(point);
    }

    private bool CanUpdateTarget(Vector3 point)
    {
        Vector3 center = _teamAreaController.TeamArea.GetCenter();

        if (BlockLimits.Instance)
            if (!BlockLimits.Instance.IsInside(point))
                return true;

        if (_currentArea == null)
            return false;

        if (!_currentArea.IsInside(center, point))
            return true;

        if (point.Equals(center))
            return true;

        return false;
    }

}
