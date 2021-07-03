using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShoot : AbilityBase
{
    public Vector3 shootDirection = Vector3.up + Vector3.forward;
    public ForceMode forceMode = ForceMode.Force;
    private float _shoot;
    protected override void InitStatus()
    {
        CharacterStatusSO statusSO = _character.status;

        if (!statusSO)
            return;

        _shoot = _character.status.Shoot;
    }

    protected override void ProcessAbility()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rigidbody _ballRB = _character.ball.GetComponent<Rigidbody>();
            _character.ball.transform.parent = _character.ballTrack;
            _ballRB.isKinematic = true;
            _ballRB.velocity = Vector3.zero;

            _character.ball.transform.localPosition = Vector3.zero;

        }

        // Input
        bool pressed = Input.GetKeyDown(KeyCode.Z);

        Vector3 dir = _character.transform.forward * shootDirection.z;
        dir += Vector3.up * shootDirection.y;


        Debug.DrawLine(_character.ballTrack.position, _character.ballTrack.position + (dir * _shoot).normalized * 3, Color.red);


        if (!pressed || !CanShoot())
            return;



        _character.ball.transform.parent = null;

        Rigidbody ballRB = _character.ball.GetComponent<Rigidbody>();

        ballRB.isKinematic = false;



        ballRB.AddForce(dir * _shoot, forceMode);

    }

    public bool CanShoot()
    {
        if (!_character.ball)
            return false;
        return true;
    }
}
