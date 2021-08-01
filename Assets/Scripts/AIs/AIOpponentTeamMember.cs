using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponentTeamMember : AICharacterBase
{

    [SerializeField]
    private TeamArea AreasToKick;

    public override BTNode GetBranch()
    {
        BTSelector _base = new BTSelector("OPPONENT TEAM");

        _base.SetNode(GetFollowBallBranch());

        return _base;
    }

    private BTNode GetFollowBallBranch()
    {
        BTSequence sequence = new BTSequence("FollowBallBranch");

        BTBallHasPossession ballHasPossession = new BTBallHasPossession("Ball has possesion", Ball.Instance);
        BTInverter inverterBallHas = new BTInverter();

        BTParallelSelector SCParallel = new BTParallelSelector("Move to ball");

        BTMove move = new BTMove();

        BTUpdateBallAsTarget updateBall = new BTUpdateBallAsTarget("Update ball", Ball.Instance);
        BTInverter inverterUpdateBall = new BTInverter();

        sequence.SetNode(inverterBallHas);
        inverterBallHas.SetNode(ballHasPossession);

        sequence.SetNode(updateBall);
        sequence.SetNode(SCParallel);


        SCParallel.SetNode(inverterUpdateBall);
        inverterUpdateBall.SetNode(updateBall);

        SCParallel.SetNode(move);
        SCParallel.SetNode(ballHasPossession);

        return sequence;
    }

    private BTNode GetAttackOnGol()
    {
        
        return null;
    }


}
