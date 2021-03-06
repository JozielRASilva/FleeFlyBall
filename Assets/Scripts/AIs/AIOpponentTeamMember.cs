using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponentTeamMember : AICharacterBase
{

    [Header("AI Shoot Time")]
    public float timeToLook = 0.5f;
    public float timeAfterShoot = 0.1f;

    [Header("Pass")]
    public float timeAfterPass = 0.1f;

    [Header("Intercept")]
    public float _distanceToIntercept = 3;

    [Header("Marked")]
    public float makedDistance = 3;
    public int maxMarked = 3;

    [SerializeField]
    protected TeamArea AreasToKick;

    public override BTNode GetBranch()
    {
        BTSelector _base = new BTSelector("OPPONENT TEAM");

        _base.SetNode(GetFollowBallBranch());

        _base.SetNode(GetAttackOnGoal());

        _base.SetNode(GetBranchMove());

        _base.SetNode(GetBranchMoveAndIntercept());

        _base.SetNode(GetBranchSelectIntercept());

        _base.SetNode(MainBlockMainPlayer());

        return _base;
    }


    private BTNode GetFollowBallBranch()
    {
        BTSequence sequence = new BTSequence("FollowBallBranch");

        BTBallHasPossession ballHasPossession = new BTBallHasPossession("Ball has possesion", Ball.Instance);
        BTInverter inverterBallHas = new BTInverter();

        BTParallelSelector SCParallel = new BTParallelSelector("Move to ball");

        BTMove move = new BTMove();

        BTUpdateBallAsTarget updateBall = new BTUpdateBallAsTarget("Update ball", Ball.Instance, _teamMember);
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
        BTVeryMarked marked = new BTVeryMarked("Marked", _teamMember, makedDistance, maxMarked);

        BTSelector selector = new BTSelector("Pass or Shoot");

        BTSequence sequenceToPass = new BTSequence("Pass ball");
        BTPass pass = new BTPass("Pass", _teamMember, timeToLook, timeAfterPass);

        BTSequence sequenceToShoot = new BTSequence("Shoot ball");
        BTShootToGoal shootToGol = new BTShootToGoal("Shooting to goal", _teamMember.group, timeToLook, timeAfterShoot, _teamMember);


        sequence.SetNode(hasBall);
        sequence.SetNode(shootStrategy);

        sequence.SetNode(SCParallel);
        SCParallel.SetNode(move);
        SCParallel.SetNode(marked);


        sequenceToPass.SetNode(marked);
        sequenceToPass.SetNode(pass);

        sequenceToShoot.SetNode(hasBall);
        sequenceToShoot.SetNode(shootToGol);

        selector.SetNode(sequenceToPass);
        selector.SetNode(sequenceToShoot);

        sequence.SetNode(selector);

        return sequence;
    }

    private BTNode GetBranchMove()
    {
        BTSequence sequence_move = new BTSequence("Move next to who has ball");

        BTTeamHasBall teamHasBall = new BTTeamHasBall("TeamHasBall", _teamMember);

        BTSelectBlock selectBlock = new BTSelectBlock("Select block", _teamMember.group, _teamMember);

        BTParallelSelector parallel_selector = new BTParallelSelector();

        #region Move parallel selector
        BTMove move = new BTMove();
        BTHasBall hasBall = new BTHasBall("HAS BALL", _character.BallPossession);
        BTInverter inverter = new BTInverter("Check team has ball");
        inverter.SetNode(teamHasBall);

        parallel_selector.SetNode(move);
        parallel_selector.SetNode(hasBall);
        parallel_selector.SetNode(inverter);
        #endregion

        sequence_move.SetNode(teamHasBall);
        sequence_move.SetNode(selectBlock);
        sequence_move.SetNode(parallel_selector);

        return sequence_move;
    }

    private BTNode GetBranchMoveAndIntercept()
    {
        BTSequence sequence_move = new BTSequence("Move to mark players");


        BTSelectBlock selectBlock = new BTSelectBlock("Select block", _teamMember.group, _teamMember);

        BTParallelSelector parallel_selector = new BTParallelSelector();


        BTMove move = new BTMove();
        BTTeamHasBall teamHasBall = new BTTeamHasBall("TeamHasBall", _teamMember);

        BTAnyoneToIntercept anyoneToIntercept = new BTAnyoneToIntercept("Anyone to Intercept?", _teamMember);
        BTCanIntercept canIntercept = new BTCanIntercept("Can Intercept Ball", _teamMember);

        parallel_selector.SetNode(move);
        parallel_selector.SetNode(teamHasBall);
        parallel_selector.SetNode(canIntercept);
        parallel_selector.SetNode(anyoneToIntercept);



        sequence_move.SetNode(selectBlock);
        sequence_move.SetNode(parallel_selector);

        return sequence_move;
    }


    private BTSelector GetBranchSelectIntercept()
    {
        BTSelector Intercept = new BTSelector("Intercept");
        Intercept.SetNode(GetBranchIntercept());
        Intercept.SetNode(GetBranchMoveToIntercept());
        return Intercept;
    }

    private BTNode GetBranchIntercept()
    {
        BTSequence sequence_intercept = new BTSequence("Intercept");

        BTCanIntercept canIntercept = new BTCanIntercept("Can Intercept?", _teamMember);
        BTInterceptBall interceptBall = new BTInterceptBall("Intercept", _teamMember);

        sequence_intercept.SetNode(canIntercept);
        sequence_intercept.SetNode(interceptBall);

        return sequence_intercept;
    }

    private BTNode GetBranchMoveToIntercept()
    {
        BTSequence sequence_move_intercept = new BTSequence("Move to intercept");

        BTAnyoneToIntercept anyoneToIntercept = new BTAnyoneToIntercept("Anyone to Intercept?", _teamMember);
        BTUpdateWhoToIntercept updateWhoToIntercept = new BTUpdateWhoToIntercept("Update Who intercept", _teamMember, _distanceToIntercept);

        BTParallelSelector parallel_selector = new BTParallelSelector();

        BTMove move = new BTMove();
        BTTeamHasBall teamHasBall = new BTTeamHasBall("TeamHasBall", _teamMember);
        BTCanIntercept canIntercept = new BTCanIntercept("Can Intercept?", _teamMember);

        parallel_selector.SetNode(move);
        parallel_selector.SetNode(teamHasBall);
        parallel_selector.SetNode(canIntercept);


        sequence_move_intercept.SetNode(anyoneToIntercept);
        sequence_move_intercept.SetNode(updateWhoToIntercept);
        sequence_move_intercept.SetNode(parallel_selector);

        return sequence_move_intercept;
    }

    private BTNode MainBlockMainPlayer()
    {
        BTSequence sequence_block_main = new BTSequence();

        BTUpdateMainPlayerWithBall updateMainPlayer = new BTUpdateMainPlayerWithBall("Update main opponent player", _teamMember);

        BTParallelSelector parallel_selector = new BTParallelSelector();

        BTMove move = new BTMove(1);
        BTTeamHasBall teamHasBall = new BTTeamHasBall("TeamHasBall", _teamMember);
        BTCanIntercept canIntercept = new BTCanIntercept("Can Intercept?", _teamMember);
        BTSelectBlock selectBlock = new BTSelectBlock("Select block", _teamMember.group, _teamMember);


        BTInverter inverter = new BTInverter();
        inverter.SetNode(updateMainPlayer);

        parallel_selector.SetNode(move);
        parallel_selector.SetNode(teamHasBall);
        parallel_selector.SetNode(canIntercept);
        parallel_selector.SetNode(inverter);
        parallel_selector.SetNode(selectBlock);

        sequence_block_main.SetNode(updateMainPlayer);
        sequence_block_main.SetNode(parallel_selector);

        return sequence_block_main;
    }
}
