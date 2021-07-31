using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMove : BTNode
{

    private float distance = 0.2f;

    public BTMove()
    {
        name = "MOVE";
    }

    public BTMove(float _distance)
    {
        name = "MOVE";

        distance = _distance;
    }

    public BTMove(float _distance, string _name)
    {
        name = _name;

        distance = _distance;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        if (BlockLimits.Instance)
        {
            if (!BlockLimits.Instance.IsInside(bt.aICharacter.GetTarget()))
            {
                status = Status.FAILURE;
                yield break;
            }
        }

        if (bt.aICharacter.CanGetTarget())
        {
            while (Vector3.Distance(bt.aICharacter.GetTarget(), bt.transform.position) > distance)
            {

                bt.aICharacter.inputAxis.Perform();
                yield return null;
            }

        }

        bt.aICharacter.inputAxis.StopPerform();


        status = Status.SUCCESS;
    }

    public override void OnStop(BehaviourTree bt)
    {
        base.OnStop(bt);

        bt.aICharacter.inputAxis.StopPerform();
    }

}
