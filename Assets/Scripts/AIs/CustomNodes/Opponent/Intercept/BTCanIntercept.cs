using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCanIntercept : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        yield break;
    }
}
