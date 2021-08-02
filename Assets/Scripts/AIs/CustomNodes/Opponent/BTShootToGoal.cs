using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTShootToGoal : BTNode
{

    protected TeamGroup _teamGroup;

    protected float _waitTimeToLook = 0.5f;

    protected float _waitTimeAfterShoot = 0.1f;

    public BTShootToGoal(string _name, TeamGroup teamGroup)
    {
        name = _name;
        _teamGroup = teamGroup;
    }

    public BTShootToGoal(string _name, TeamGroup teamGroup, float waitTimeToLook, float waitTimeAfterShoot)
    {
        name = _name;

        _teamGroup = teamGroup;

        _waitTimeToLook = waitTimeToLook;
        _waitTimeAfterShoot = waitTimeAfterShoot;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        if (!TeamController.Instance)
        {
            status = Status.FAILURE;
            yield break;
        }

        yield return bt.StartCoroutine(LookAtGol(bt));

        // Perform Input
        bt.aICharacter.inputShoot.Perform();

        yield return new WaitForSeconds(_waitTimeAfterShoot);

        bt.aICharacter.inputShoot.StopPerform();

        status = Status.SUCCESS;
        yield break;

    }

    private IEnumerator LookAtGol(BehaviourTree bt)
    {
        Goal goal = null;

        if (_teamGroup.isPlayerGroup)
            goal = TeamController.Instance.GetOpponentGroup().thisGoal;
        else
            goal = TeamController.Instance.GetPlayerGroup().thisGoal;

        Vector3 vectorBetweenPoints = (goal.transform.position - bt.transform.position).normalized;

        Vector2 direction = new Vector2(vectorBetweenPoints.x, vectorBetweenPoints.z);

        bt.aICharacter.inputAxis.PerformFixed(direction);

        yield return new WaitForSeconds(_waitTimeToLook);

        bt.aICharacter.inputAxis.StopPerform();

        yield return null;
    }
}
