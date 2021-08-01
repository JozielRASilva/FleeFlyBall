using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTUpdateBallAsTarget : BTNode
{

    private Ball _ball;


    public BTUpdateBallAsTarget(string _name, Ball ball)
    {
        name = _name;
        _ball = ball;
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

}
