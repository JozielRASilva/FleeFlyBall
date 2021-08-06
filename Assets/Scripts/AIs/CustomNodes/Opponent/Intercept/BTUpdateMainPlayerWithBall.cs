using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTUpdateMainPlayerWithBall : BTNode
{
    private TeamMember _teamMember;

    private TeamGroup _opponentGroup;

    private float _distanceToIntercept = 3;

    public BTUpdateMainPlayerWithBall(string _name, TeamMember teamMember)
    {
        name = _name;

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

        TeamMember main = _opponentGroup.GetMain();
        if (!main)
            yield break;

        if (!main.BallPossession.HasBall())
            yield break;

        UpdateTarget(bt, main.transform.position);

        status = Status.SUCCESS;

        yield break;
    }

    private void UpdateTarget(BehaviourTree bt, Vector3 target)
    {
        Vector3 point = target;

        bt.aICharacter.ChangeTarget(point);
    }
}
