using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAnyoneToIntercept : BTNode
{

    private TeamMember _teamMember;

    private TeamArea _teamArea;

    private TeamAreaController _teamAreaController;

    private AreaBase _currentArea;

    private TeamGroup _opponentGroup;

    public BTAnyoneToIntercept(string _name, TeamMember teamMember)
    {
        name = _name;

        _teamArea = teamMember.group.GetComponent<TeamArea>();

        _teamAreaController = teamMember.group.GetComponent<TeamAreaController>();

        _teamMember = teamMember;


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

                    status = Status.SUCCESS;
                    yield break;
                }
            }


        }

        yield break;
    }

}
