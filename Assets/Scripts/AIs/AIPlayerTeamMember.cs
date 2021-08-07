using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerTeamMember : AICharacterBase
{

    public float distanceToTargets = 0.3f;

    public override BTNode GetBranch()
    {
        BTSequence sequence = new BTSequence("PLAYER TEAM");

        BTPlayerTeam playerTeam = new BTPlayerTeam("IS PLAYER TEAM", _teamMember);

        BTNode sq_checkplayercontrol = GetBranchCheckPlayerControl();


        BTSelector selector = new BTSelector();
        selector.SetNode(GetFollowBallBranch());
        selector.SetNode(GetBranchMove());


        sequence.SetNode(playerTeam);
        sequence.SetNode(sq_checkplayercontrol);
        sequence.SetNode(selector);

        return sequence;
    }

    private BTSequence GetBranchMove()
    {
        BTSequence sequence_move = new BTSequence("PLAYER MOVE");

        BTSelectBlock selectBlock = new BTSelectBlock("Select block", _teamMember.group, _teamMember);

        BTParallelSelector parallel_selector = new BTParallelSelector();
        #region Move parallel selector

        BTMove move = new BTMove(distanceToTargets);
        BTHasBall hasBall = new BTHasBall("HAS BALL", _character.BallPossession);
        BTPlayerControl playerControl = new BTPlayerControl("Player Control", _teamMember);

        BTBallHasPossession ballHasPossession = new BTBallHasPossession("Ball has possesion", Ball.Instance);
        BTInverter inverterBallHas = new BTInverter();

        inverterBallHas.SetNode(ballHasPossession);

        parallel_selector.SetNode(move);
        parallel_selector.SetNode(hasBall);
        parallel_selector.SetNode(playerControl);
        parallel_selector.SetNode(inverterBallHas);
        #endregion

        sequence_move.SetNode(selectBlock);
        sequence_move.SetNode(parallel_selector);

        return sequence_move;
    }

    private BTNode GetBranchCheckPlayerControl()
    {
        BTSequence sq_checkplayercontrol = new BTSequence("Check player control");

        BTPlayerControl playerControl = new BTPlayerControl("Player Control", _teamMember);

        BTInverter inverter = new BTInverter();

        inverter.SetNode(playerControl);

        sq_checkplayercontrol.SetNode(inverter);

        return sq_checkplayercontrol;
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
}
