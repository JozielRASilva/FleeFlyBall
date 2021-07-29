using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectBlock : BTNode
{

    private TeamGroup _teamGroup;

    private TeamArea _teamArea;

    public BTSelectBlock(string _name, TeamGroup teamGroup)
    {
        name = _name;

        _teamGroup = teamGroup;

        _teamArea = _teamGroup.GetComponent<TeamArea>();

    }

    public override IEnumerator Run(BehaviourTree bt)
    {

        yield break;
    }
}
