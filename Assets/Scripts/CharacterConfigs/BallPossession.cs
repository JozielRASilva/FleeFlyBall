using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPossession : MonoBehaviour
{
    public Transform ballTrack;

    public Ball ball;

    public Vector3 DirectionWhenLost;
    public float delayToReattach = 0.2f;

    [Header("Lost Ball")]
    public Vector3 direction = Vector3.up + Vector3.forward;
    public ForceMode forceMode = ForceMode.Force;
    public FloatSO movementForce;
    private float _shoot => movementForce == null ? 4 : movementForce.value;

    private float _timeStampToReattach;
    private Character _character;

    private CollisionAndTrigger _collisionAndTrigger;

    private bool _touching;

    private void Awake()
    {
        _character = GetComponent<Character>();

        if (!_character)
            _character = GetComponentInChildren<Character>();

        if (!_character)
            _character = GetComponentInParent<Character>();

        _collisionAndTrigger = GetComponent<CollisionAndTrigger>();
        if (!_collisionAndTrigger)
            _collisionAndTrigger = GetComponentInChildren<CollisionAndTrigger>();

        _collisionAndTrigger.OnEnter += TouchBall;
        _collisionAndTrigger.OnExit += UnTouchBall;

    }

    private void Start()
    {

        _character.balance.OnLossAllBalance += LostBallAndStun;

    }

    public bool HasBall()
    {
        if (ball)
            return true;

        return false;
    }

    public bool CanAttachBall()
    {
        if (_character.GetStatus() == CharacterInfo.Status.STUNNED)
            return false;

        if (_character.balance.CurrentBalance <= 0)
            return false;

        return true;
    }

    public bool TouchingBall()
    {
        return false;
    }

    private void TouchBall(GameObject touchedBall)
    {
        _touching = true;

        Ball _ball = touchedBall.GetComponent<Ball>();

        if (!_ball.Avaliable())
            return;

        if (!CanAttachBall())
            return;

        AttachBall(_ball);
    }

    public void AttachBall(Ball _ball)
    {
        if (_timeStampToReattach > Time.time)
            return;

        _ball.AttachOnPlayer(_character);

        _ball.transform.parent = ballTrack;

        _ball.transform.localPosition = Vector3.zero;

        ball = _ball;

        ball.OnDeattach += DeattachBall;
    }

    public void RemoveBall()
    {
        if (!ball)
            return;
        ball.Deattach();
    }
    private void DeattachBall()
    {
        _timeStampToReattach = Time.time + delayToReattach;

        ball.transform.parent = null;

        ball.OnDeattach -= DeattachBall;
        ball = null;
    }

    private void UnTouchBall(GameObject touchedBall)
    {
        _touching = false;
    }

    private void LostBallAndStun()
    {
        if (!HasBall())
            return;


        _character.SetStatus(CharacterInfo.Status.STUNNED);

        Vector3 dir = _character.GetKickDirection(direction, _shoot);
        _character.BallPossession.ball.Chutar(dir * _shoot, forceMode);

        RemoveBall();

        _character.balance.ReBreath();

    }

}
