using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerTeamMember : AICharacterBase
{
    public override BTNode GetBranch()
    {
        BTSequence sequence = new BTSequence("PLAYER TEAM");

        BTPlayerTeam playerTeam = new BTPlayerTeam("IS PLAYER TEAM", _teamMember);

        BTNode sq_checkplayercontrol = GetBranchCheckPlayerControl();

        #region MOVE
        BTSequence sequence_move = new BTSequence("PLAYER MOVE");

        BTSelectBlock selectBlock = new BTSelectBlock("Select block", _teamMember.group, _teamMember);

        BTParallelSelector parallel_selector = new BTParallelSelector();
        #region Move parallel selector
        BTMove move = new BTMove();
        BTHasBall hasBall = new BTHasBall("HAS BALL", _character.BallPossession);
        BTPlayerControl playerControl = new BTPlayerControl("Player Control", _teamMember);

        parallel_selector.SetNode(move);
        parallel_selector.SetNode(hasBall);
        parallel_selector.SetNode(playerControl);
        #endregion

        sequence_move.SetNode(selectBlock);
        sequence_move.SetNode(parallel_selector);
        #endregion

        sequence.SetNode(playerTeam);
        sequence.SetNode(sq_checkplayercontrol);
        sequence.SetNode(sequence_move);

        return sequence;
    }

    private BTNode GetBranchCheckPlayerControl()
    {
        BTSequence sq_checkplayercontrol = new BTSequence("Check player control");

        BTPlayerControl playerControl = new BTPlayerControl("Player Control", _teamMember);

        //BTParallelSelector parallelSelector = new BTParallelSelector("Checking control");
        BTInverter inverter = new BTInverter();
        //BTPlayerControl _playerControl = new BTPlayerControl("Player Control", _teamMember);

        inverter.SetNode(playerControl);
        //parallelSelector.SetNode(inverter);

        sq_checkplayercontrol.SetNode(inverter);
        //sq_checkplayercontrol.SetNode(parallelSelector);
        return sq_checkplayercontrol;
    }

}
