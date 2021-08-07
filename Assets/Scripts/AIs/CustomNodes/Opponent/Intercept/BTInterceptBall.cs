using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInterceptBall : BTNode
{
    private TeamMember _member;

    private AbilityDomainBall domainBall;

    private AbilityToTakeBall takeBall;

    private Ball _ball;

    public BTInterceptBall(string name, TeamMember member)
    {
        this.name = name;

        _member = member;

        domainBall = member.GetComponentInChildren<AbilityDomainBall>();

        takeBall = member.GetComponentInChildren<AbilityToTakeBall>();
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;


        if (domainBall)
        {
            if (domainBall.CanDomain(ref _ball))
            {
                status = Status.SUCCESS;
            }
        }

        if (takeBall)
        {
            if (takeBall.CanTakeBall(ref _ball))
            {
                status = Status.SUCCESS;
            }
        }

        if (status != Status.SUCCESS)
            yield break;

        var wait = new WaitForSeconds(0.1f);

        bt.aICharacter.inputIntercept.Perform();

        yield return wait;

        bt.aICharacter.inputIntercept.StopPerform();


    }
}
