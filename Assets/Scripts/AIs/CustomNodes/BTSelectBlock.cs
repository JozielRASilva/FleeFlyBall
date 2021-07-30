using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectBlock : BTNode
{

    private TeamGroup _teamGroup;

    private TeamMember _teamMember;

    private TeamArea _teamArea;

    private TeamAreaController _teamAreaController;

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
            Vector3 point = _teamAreaController.GetRandomPointOnArea(_teamMember);

            bt.aICharacter.ChangeTarget(point);

            status = Status.SUCCESS;
        }

        yield break;
    }
}
