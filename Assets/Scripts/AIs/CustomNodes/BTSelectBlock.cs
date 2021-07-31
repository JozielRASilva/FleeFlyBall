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
            Vector3 currentPoint = bt.aICharacter.GetTarget();

            if (CanUpdateTarget(currentPoint))
            {
                Vector3 point = _teamAreaController.GetRandomPointOnArea(_teamMember);

                bt.aICharacter.ChangeTarget(point);
                

                _currentArea = _teamAreaController.GetArea(_teamMember);
            }

            status = Status.SUCCESS;
        }

        yield break;
    }

    private bool CanUpdateTarget(Vector3 point)
    {
        if (_currentArea == null)
            return true;

        Vector3 center = _teamAreaController.TeamArea.GetCenter();

        if (!_currentArea.IsInside(center, point))
            return true;

        Debug.Log($"P: {point} C: {center}");
        if (point.Equals(center))
           return true;

        return false;
    }

}
