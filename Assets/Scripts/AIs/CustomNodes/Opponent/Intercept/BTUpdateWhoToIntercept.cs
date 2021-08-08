using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTUpdateWhoToIntercept : BTNode
{
    private TeamMember _teamMember;

    private TeamArea _teamArea;

    private TeamAreaController _teamAreaController;

    private AreaBase _currentArea;

    private TeamGroup _opponentGroup;

    private float _distanceToIntercept = 3;

    public BTUpdateWhoToIntercept(string _name, TeamMember teamMember, float distanceToIntercept)
    {
        name = _name;

        _teamArea = teamMember.group.GetComponent<TeamArea>();

        _teamAreaController = teamMember.group.GetComponent<TeamAreaController>();

        _teamMember = teamMember;

        _distanceToIntercept = distanceToIntercept;

        if (TeamController.Instance.GetPlayerGroup().Equals(teamMember.group))
        {
            _opponentGroup = TeamController.Instance.GetOpponentGroup();
        }
        else
        {
            _opponentGroup = TeamController.Instance.GetPlayerGroup();
        }

    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (_teamAreaController)
        {
            _currentArea = _teamAreaController.GetArea(_teamMember);

            if (_currentArea == null)
                yield break;

            Vector3 center = _teamAreaController.TeamArea.GetCenter();

            foreach (var member in _opponentGroup.TeamMembers)
            {
                if (member.Equals(_opponentGroup.GetMain()))
                    continue;
                if (_currentArea.IsInside(center, member.transform.position))
                {
                    UpdateTarget(bt, member.transform.position);
                    status = Status.SUCCESS;
                    yield break;
                }
            }

        }

        yield break;
    }

    private void UpdateTarget(BehaviourTree bt, Vector3 target)
    {
        Vector3 point = target;
        point.y = 0;

        TeamMember main = _opponentGroup.GetMain();

        if (!main)
            return;

        Vector3 mainOpponentPos = main.transform.position;
        mainOpponentPos.y = 0;

        Vector3 direction = (mainOpponentPos - point).normalized * _distanceToIntercept;

        Vector3 finalPosition = target + direction;

        bt.aICharacter.ChangeTarget(finalPosition);
    }
}
