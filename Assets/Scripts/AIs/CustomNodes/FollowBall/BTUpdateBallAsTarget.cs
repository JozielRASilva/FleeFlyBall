using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTUpdateBallAsTarget : BTNode
{

    private Ball _ball;

    private TeamMember _member;

    public BTUpdateBallAsTarget(string _name, Ball ball, TeamMember member)
    {
        name = _name;
        _ball = ball;

        _member = member;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        if (!_ball)
        {
            status = Status.FAILURE;
            yield break;
        }

        if (_ball.onPlayer)
        {
            status = Status.FAILURE;
            yield break;
        }

        TeamMember selected = SelecNextToBall();

        if (!selected)
        {
            status = Status.FAILURE;
            yield break;
        }

        if (!selected.Equals(_member))
        {
            status = Status.FAILURE;
            yield break;
        }

        UpdateTarget(bt);

        status = Status.SUCCESS;

        yield break;
    }


    private void UpdateTarget(BehaviourTree bt)
    {
        Vector3 point = _ball.transform.position;
        point.y = 0;

        bt.aICharacter.ChangeTarget(point);
    }

    private TeamMember SelecNextToBall()
    {
        TeamMember selected = null;
        float lastDistance = 0;
        foreach (var member in _member.group.TeamMembers)
        {
            if (member.group.isPlayerGroup && member.IsMain)
                continue;

            float distance = Vector3.Distance(_ball.transform.position, member.transform.position);

            if (!selected)
            {
                selected = member;
                lastDistance = distance;
            }
            else if (distance < lastDistance)
            {
                selected = member;
                lastDistance = distance;
            }

        }

        return selected;
    }

}
