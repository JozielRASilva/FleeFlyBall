using System.Collections;
using UnityEngine;

public class BTSequence : BTNode
{

    public BTSequence()
    {
        name = "SEQUENCE";
    }

    public BTSequence(string _name)
    {
        name = _name;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {

        status = Status.RUNNING;

        foreach (var node in children)
        {

            yield return bt.StartCoroutine(node.Run(bt));

            if (node.status.Equals(Status.FAILURE))
            {
                status = Status.FAILURE;
                break;
            }

        }

        if (status.Equals(Status.RUNNING))
        {
            status = Status.SUCCESS;
        }

    }

}