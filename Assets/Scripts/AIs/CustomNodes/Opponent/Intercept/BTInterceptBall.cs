using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInterceptBall : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        yield break;
    }
}
