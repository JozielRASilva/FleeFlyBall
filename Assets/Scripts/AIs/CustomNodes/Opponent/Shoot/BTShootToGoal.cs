using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTShootToGoal : BTNode
{

    protected TeamGroup _teamGroup;

    protected float _waitTimeToLook = 0.5f;

    protected float _waitTimeAfterShoot = 0.1f;

    protected AbilityWalk abilityWalk;

    public BTShootToGoal(string _name, TeamGroup teamGroup, TeamMember member)
    {
        name = _name;
        _teamGroup = teamGroup;

        abilityWalk = member.GetComponentInChildren<AbilityWalk>();
    }

    public BTShootToGoal(string _name, TeamGroup teamGroup, float waitTimeToLook, float waitTimeAfterShoot, TeamMember member)
    {
        name = _name;

        _teamGroup = teamGroup;

        _waitTimeToLook = waitTimeToLook;
        _waitTimeAfterShoot = waitTimeAfterShoot;

        abilityWalk = member.GetComponentInChildren<AbilityWalk>();
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

        float timeStamp = Time.time + _waitTimeToLook;

        Vector3 _direction = new Vector3(vectorBetweenPoints.x, 0, vectorBetweenPoints.z);

        while (Time.time < timeStamp)
        {

            abilityWalk?.LookAtDirection(_direction, 10);

            yield return null;
        }

    }
}
