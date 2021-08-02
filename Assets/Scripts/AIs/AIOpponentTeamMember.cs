using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponentTeamMember : AICharacterBase
{

    [Header("AI Shoot Time")]
    public float timeToLook = 0.5f;
    public float timeAfterShoot = 0.1f;

    [SerializeField]
    protected TeamArea AreasToKick;

    public override BTNode GetBranch()
    {
        BTSelector _base = new BTSelector("OPPONENT TEAM");

        _base.SetNode(GetFollowBallBranch());

        _base.SetNode(GetAttackOnGoal());

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

    private BTNode GetAttackOnGoal()
    {
        BTSequence sequence = new BTSequence("AttackOnGoal");

        BTHasBall hasBall = new BTHasBall("Has ball", _character.BallPossession);

        BTSelectShootStrategy shootStrategy = new BTSelectShootStrategy("Select Shoot Strategy", _teamMember.group, _teamMember, AreasToKick);

        BTParallelSelector SCParallel = new BTParallelSelector();

        BTMove move = new BTMove();
        // Check marker here

        BTSequence sequenceToPass = new BTSequence("Pass ball");
        // Check marker here
        // Pass ball

        BTSequence sequenceToShoot = new BTSequence("Shoot ball");

        BTShootToGoal shootToGol = new BTShootToGoal("Shooting to goal", _teamMember.group, timeToLook, timeAfterShoot);


        sequence.SetNode(hasBall);
        sequence.SetNode(shootStrategy);

        sequence.SetNode(SCParallel);
        SCParallel.SetNode(move);

        // sequence to pass

        sequenceToShoot.SetNode(hasBall);
        sequenceToShoot.SetNode(shootToGol);

        sequence.SetNode(sequenceToShoot);

        return sequence;
    }


}
