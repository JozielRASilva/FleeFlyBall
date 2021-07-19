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


    public Action OnTakeBall;
    public Action OnCanNotTakeBall;

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
        Debug.Log($"Authorized to take ball from a player");
        if (!CanTakeBall(ref _ball))
            return;

        Debug.Log($"Can take ball from a player");

        if (!ExecuteAction())
            return;

        _ball.Deattach();

        Debug.Log("Take ball");
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

    private bool CanTakeBall(ref Ball _ball)
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
