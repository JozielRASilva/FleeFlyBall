using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTHasBall : BTNode
{
    private BallPossession ballPossession;

    public BTHasBall(string _name, BallPossession _ballPossession)
    {
        name = _name;

        ballPossession = _ballPossession;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!ballPossession)
            yield break;

        if (ballPossession.HasBall())
            status = Status.SUCCESS;

        yield return null;
    }
}
