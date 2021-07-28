using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerTeam : BTNode
{

    private TeamMember member;

    public BTPlayerTeam(string _name, TeamMember _teamMember)
    {
        name = _name;

        member = _teamMember;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!TeamController.Instance)
            yield break;

        if (TeamController.Instance.GetPlayerGroup().IsMember(member))
        {
            status = Status.SUCCESS;
        }

        yield return null;
    }

}
