using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBallHasPossession : BTNode
{

    private Ball _ball;

    public BTBallHasPossession(string _name, Ball ball)
    {
        name = _name;
        _ball = ball;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!_ball)
            yield break;

        if (_ball.onPlayer)
        {
            status = Status.SUCCESS;
        }


        yield break;
    }
}
