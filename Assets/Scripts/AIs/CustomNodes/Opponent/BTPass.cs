using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPass : BTNode
{
    private TeamMember _teamMember;

    protected float _waitTimeToLook = 0.5f;

    protected float _waitTimeAfterPass = 0.1f;

    protected AbilityWalk abilityWalk;


    public BTPass(string _name, TeamMember member, float waitTimeToLook = 0.5f, float waitTimeAfterPass = 0.1f)
    {
        name = _name;

        _teamMember = member;

        _waitTimeToLook = waitTimeToLook;

        _waitTimeAfterPass = waitTimeAfterPass;

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

        Transform target = SelectTarget(bt);

        yield return bt.StartCoroutine(LookAt(bt, target));

        bt.aICharacter.inputPass.Perform();

        yield return new WaitForSeconds(_waitTimeAfterPass);

        bt.aICharacter.inputPass.StopPerform();

        status = Status.SUCCESS;
        yield break;

    }

    private Transform SelectTarget(BehaviourTree bt)
    {
        Transform selected = null;
        float lastDistance = 0;
        foreach (var member in _teamMember.group.TeamMembers)
        {
            if (member.Equals(_teamMember))
                continue;

            float distance = Vector3.Distance(bt.transform.position, member.transform.position);

            if (!selected)
            {
                selected = member.transform;
                lastDistance = distance;
            }
            else if (distance > lastDistance)
            {
                selected = member.transform;
                lastDistance = distance;
            }

        }

        return selected;
    }

    private IEnumerator LookAt(BehaviourTree bt, Transform _target)
    {
        Transform target = _target;


        Vector3 vectorBetweenPoints = (target.position - bt.transform.position).normalized;

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
