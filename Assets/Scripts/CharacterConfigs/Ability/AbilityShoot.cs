using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShoot : AbilityBase
{
    public Vector3 shootDirection = Vector3.up + Vector3.forward;
    public ForceMode forceMode = ForceMode.Force;


    public Ball.KickType kickType = Ball.KickType.NORMAL;

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();

    [Header("Shoot Cost"), SerializeField]
    private FloatSO shootCost;

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

        _character.BallPossession.ball.SetKickType(kickType);
        _character.BallPossession.ball.Chutar(dir * _shoot, forceMode);

        ApplyBalanceCost();

    }


    private void ApplyBalanceCost()
    {
        float value = 1;

        if (shootCost)
            value = shootCost.value;

        _character.balance.UseBalance(value);
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
        if (!_character.BallPossession.HasBall())
            return false;

        if (!_character.balance.HasBalance())
            return false;

        return true;
    }
}
