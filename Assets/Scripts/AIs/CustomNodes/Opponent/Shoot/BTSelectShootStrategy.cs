using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectShootStrategy : BTNode
{
    private TeamGroup _teamGroup;

    private TeamMember _teamMember;

    private TeamArea _areaToKick;

    private TeamAreaController _teamAreaController;

    public BTSelectShootStrategy(string _name, TeamGroup teamGroup, TeamMember teamMember, TeamArea areaToKick)
    {
        name = _name;

        _teamGroup = teamGroup;

        _areaToKick = areaToKick;

        _teamAreaController = _teamGroup.GetComponent<TeamAreaController>();

        _teamMember = teamMember;

    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (_teamAreaController)
        {

            Vector3 currentPoint = bt.aICharacter.GetTarget();

            AreaBase area = SelectRandomArea();

            if (area == null)
                yield break;

            UpdateTarget(bt, area);

            status = Status.SUCCESS;
        }

        yield break;
    }

    private void UpdateTarget(BehaviourTree bt, AreaBase area)
    {
        Vector3 point = _teamAreaController.GetRandomPointOnArea(_teamMember, area, _areaToKick);

        bt.aICharacter.ChangeTarget(point);
    }

    private AreaBase SelectRandomArea()
    {

        List<AreaBase> areas = _areaToKick.GetAreas();

        if (areas.Count == 0)
            return null;

        int index = Random.Range(0, areas.Count);

        return areas[index];
    }


}
