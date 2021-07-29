using System.Collections;
using UnityEngine;

public class BTSelector : BTNode
{

    public BTSelector()
    {
        name = "SELECTOR";
    }

    public BTSelector(string _name)
    {
        name = _name;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {

        status = Status.RUNNING;

        foreach (var node in children)
        {

            yield return bt.StartCoroutine(node.Run(bt));

            if (node.status.Equals(Status.SUCCESS))
            {
                status = Status.SUCCESS;
                break;
            }

        }

        if (status.Equals(Status.RUNNING))
        {
            status = Status.FAILURE;
        }

    }
}