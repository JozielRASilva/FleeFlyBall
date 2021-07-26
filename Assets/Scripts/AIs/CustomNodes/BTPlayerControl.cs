using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerControl : BTNode
{
    private TeamMember member;

    public BTPlayerControl(string _name, TeamMember _teamMember)
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
            if (member.IsMain)
                status = Status.SUCCESS;
        }

        yield return null;
    }
}
