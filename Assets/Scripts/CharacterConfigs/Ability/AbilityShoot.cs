using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShoot : AbilityBase
{
    public Vector3 shootDirection = Vector3.up + Vector3.forward;
    public ForceMode forceMode = ForceMode.Force;

    public List<InputSO> inputs = new List<InputSO>();
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

        Vector3 track = _character.BallPossession.ballTrack.position;

        Vector3 dir = _character.transform.forward * shootDirection.z;
        dir += Vector3.up * shootDirection.y;

        Debug.DrawLine(track, track + (dir * _shoot).normalized * 3, Color.red);

        if (!ExecuteAction() && !CanShoot())
            return;

        _character.BallPossession.ball.Chutar(dir * _shoot, forceMode);

    }

    private bool ExecuteAction()
    {

        if (InputController.Instance)
        {
            var inputBases = InputController.Instance.GetInput(inputs);
            for (int i = 0; i < inputBases.Count; i++)
            {
                if (inputBases[i].ButtomDown())
                    return true;
            }
        }

        return false;
    }

    public bool CanShoot()
    {
        if (!_character.BallPossession.HasBall())
            return false;
        return true;
    }
}
