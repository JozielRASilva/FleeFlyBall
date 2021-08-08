using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTVeryMarked : BTNode
{
    private TeamMember _teamMember;

    private TeamGroup _opponentGroup;

    private float _makedDistance = 4;

    private int _maxMarked = 2;

    public BTVeryMarked(string _name, TeamMember teamMember, float makedDistance = 4, int maxMarked = 2)
    {
        name = _name;

        _teamMember = teamMember;

        _makedDistance = makedDistance;

        _maxMarked = maxMarked;

        if (TeamController.Instance.GetPlayerGroup().Equals(teamMember.group))
        {
            _opponentGroup = TeamController.Instance.GetOpponentGroup();
        }
        else
        {
            _opponentGroup = TeamController.Instance.GetPlayerGroup();
        }
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        int count = 0;

        foreach (var member in _opponentGroup.TeamMembers)
        {
            float distance = Vector3.Distance(member.transform.position, bt.transform.position);
            if (distance < _makedDistance)
            {
                count++;
            }
        }

        if (count >= _maxMarked)
            status = Status.SUCCESS;

        yield break;
    }

}
