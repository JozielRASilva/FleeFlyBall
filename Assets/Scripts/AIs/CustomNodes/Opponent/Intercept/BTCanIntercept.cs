using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCanIntercept : BTNode
{

    private TeamMember _member;

    private AbilityDomainBall domainBall;

    private AbilityToTakeBall takeBall;

    private Ball _ball;

    public BTCanIntercept(string name, TeamMember member)
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


        yield break;
    }
}
