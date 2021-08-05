using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTeamHasBall : BTNode
{

    private TeamMember member;

    public BTTeamHasBall(string _name, TeamMember _teamMember)
    {
        name = _name;

        member = _teamMember;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!TeamController.Instance)
            yield break;

        if (member.group.HasBall())
        {
            status = Status.SUCCESS;
        }

        yield return null;
    }
}
