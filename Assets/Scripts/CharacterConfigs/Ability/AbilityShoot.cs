using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShoot : AbilityBase
{
    public Vector3 shootDirection = Vector3.up + Vector3.forward;
    public ForceMode forceMode = ForceMode.Force;

    [Header("Inputs")]
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

        if (!ExecuteAction() || !CanShoot())
            return;

        _character.BallPossession.ball.Chutar(dir * _shoot, forceMode);

    }

    private bool ExecuteAction()
    {
        switch (_character.control)
        {
            case Character.ControlType.PLAYER:

                if (InputController.Instance)
                {
                    var inputBases = InputController.Instance.GetInput(inputs);
                    for (int i = 0; i < inputBases.Count; i++)
                    {
                        if (inputBases[i].ButtomDown())
                            return true;
                    }
                }

                break;

            case Character.ControlType.AI:
                // Set AI input here
                break;
        }

        return false;
    }

    public bool CanShoot()
    {
        if (_character.BallPossession.HasBall())
            return true;

        return false;
    }
}
