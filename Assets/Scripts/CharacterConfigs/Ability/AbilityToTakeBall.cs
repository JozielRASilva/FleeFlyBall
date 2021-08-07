using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CollisionAndTrigger))]
public class AbilityToTakeBall : AbilityBase
{

    public FloatSO heightToGet;

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();

    [Header("To Take Ball Cost"), SerializeField]
    private FloatSO toTakeBallCost;

    public Action OnTakeBall;
    public Action OnCanNotTakeBall;

    [Header("Lost Ball")]
    public Vector3 direction = Vector3.up + Vector3.forward;
    public ForceMode forceMode = ForceMode.Force;
    public FloatSO movementForce;

    [Header("Audio")]
    public AudioPlayer audioPlayer;
    public string sound;

    private float _shoot => movementForce == null ? 4 : movementForce.value;

    private CollisionAndTrigger _collisionAndTrigger;

    private bool _touching;

    private GameObject _touchedBall;


    protected override void Initialize()
    {
        base.Initialize();

        _collisionAndTrigger = GetComponent<CollisionAndTrigger>();

        if (!_collisionAndTrigger)
            _collisionAndTrigger = GetComponentInChildren<CollisionAndTrigger>();


        _collisionAndTrigger.OnEnter += TouchBall;
        _collisionAndTrigger.OnExit += UnTouchBall;
    }


    protected override bool Authorized
    {
        get
        {
            if (!AbilityPermitted)
                return false;

            if (BlockOnStates.Contains(_character.GetCharacterState()))
                return false;

            if (!_touching)
                return false;

            if (!_touchedBall)
                return false;

            float height = _touchedBall.transform.position.y - _character.transform.position.y;

            if (height < heightToGet.value)
                return false;


            return true;
        }
    }

    protected override void ProcessAbility()
    {
        if (!Authorized)
            return;

        Ball _ball = null;

        if (!CanTakeBall(ref _ball))
            return;

        if (!ExecuteAction())
            return;

        TakeBall(_ball);

        audioPlayer.PlaySound(sound);
    }

    private void TakeBall(Ball _ball)
    {

        Vector3 dir = _character.GetKickDirection(direction, _shoot);
        _ball.Chutar(dir * _shoot, forceMode);

        _ball.Deattach();

        ApplyBalanceCost();
    }

    private void ApplyBalanceCost()
    {
        float value = 1;

        if (toTakeBallCost)
            value = toTakeBallCost.value;

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
                if (characterBase)
                {
                    bool _value = characterBase.inputIntercept.GetValue(characterBase);

                    return _value;
                }
                break;
        }

        return false;
    }

    public bool CanTakeBall(ref Ball _ball)
    {
        if (!_touchedBall)
            return false;

        _ball = _touchedBall.GetComponent<Ball>();

        if (!_ball.onPlayer)
            return false;

        if (_ball.CurrentPlayer.Equals(_character))
            return false;

        TeamMember other = _ball.CurrentPlayer.Team;

        if (_character.Team.group.IsMember(other))
            return false;

        if (!_character.BallPossession.CanAttachBall())
            return false;

        return true;
    }

    private void TouchBall(GameObject touchedBall)
    {
        _touching = true;

        _touchedBall = touchedBall;
    }

    private void UnTouchBall(GameObject touchedBall)
    {
        _touching = false;

        _touchedBall = null;
    }

}
