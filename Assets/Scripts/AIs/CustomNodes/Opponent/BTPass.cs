using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPass : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        yield break;
    }
}
