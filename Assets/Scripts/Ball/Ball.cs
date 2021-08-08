using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : Singleton<Ball>
{


    [Header("Logic Control")]
    public bool onPlayer;

    public bool inField;

    public bool grounded;

    public bool AutoControl = true;

    public enum BallState { NONE, KICKED, PASSED }
    public BallState _ballState = BallState.NONE;


    public enum KickType { NONE, NORMAL, SPECIAL }
    [SerializeField]
    private KickType _currentKick = KickType.NONE;

    public float delayToGetBall = 0.02f;
    private float getBallTimeStamp;

    [Header("Detect field")]
    public GameObject pointDetect;
    private PointDetection _pointDetection;

    [Header("Pass behaviour info")]
    public bool AvaliableWhenPassing = true;
    [SerializeField]
    private float _pass_threshold = 0.1f;
    public float TimeToPass = 10f;

    private Rigidbody _rigidbody;


    private Character _currentPlayer;
    private Character _lastPlayer;

    public Character CurrentPlayer => _currentPlayer;

    public Action OnDeattach;

    private Coroutine _passCoroutine;

    public TeamGroup _teamGroup;

    [Header("Audio")]
    public AudioPlayer audioPlayer;
    public string bounceSound;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();

        if (pointDetect)
            _pointDetection = pointDetect.GetComponent<PointDetection>();
    }

    private void Update()
    {
        ControleFisica();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            print("tocou no chão");
            grounded = true;

            SetKickType(KickType.NONE);

            ResetTimeStamp();

            audioPlayer.PlaySound(bounceSound);
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Crossbar"))
        {
            audioPlayer.PlaySound(bounceSound);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InField"))
        {
            inField = false;
            print("Fora da Quadra");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InField"))
        {
            inField = true;
            print("Dentro da Quadra");
        }
    }

    public void Chutar(Vector3 force, ForceMode forceMode)
    {
        onPlayer = false;

        _rigidbody.isKinematic = false;

        _rigidbody.AddForce(force, forceMode);

        grounded = false;

        transform.parent = null;

        OnDeattach?.Invoke();

        if (_pointDetection)
            _pointDetection.DetectArea();

        _ballState = BallState.KICKED;

        if (_currentKick.Equals(KickType.NONE))
            SetKickType(KickType.NORMAL);

        SetTimeStamp();
    }


    public void Deattach()
    {
        transform.parent = null;

        OnDeattach?.Invoke();

        onPlayer = false;

        _rigidbody.isKinematic = false;



    }

    private void SetTimeStamp()
    {
        getBallTimeStamp = Time.time + delayToGetBall;
    }

    private void ResetTimeStamp()
    {
        getBallTimeStamp = Time.time;
    }

    public void ControleFisica()
    {
        if (!AutoControl)
            return;

        if (!onPlayer && !_ballState.Equals(BallState.PASSED))
        {
            _rigidbody.isKinematic = false;
        }
        else
        {
            _rigidbody.isKinematic = true;

        }
    }

    public void AttachOnPlayer(Character newPlayer)
    {
        _lastPlayer = _currentPlayer;

        _currentPlayer = newPlayer;

        onPlayer = true;

        if (_passCoroutine != null)
            StopCoroutine(_passCoroutine);

        _ballState = BallState.NONE;
    }




    public void Pass(Vector3 main, Transform target, float height, float Speed)
    {

        onPlayer = false;

        _rigidbody.isKinematic = false;

        _ballState = BallState.PASSED;
        _passCoroutine = StartCoroutine(PassCO(main, target, height, Speed));

        grounded = false;

        transform.parent = null;

        OnDeattach?.Invoke();

        SetTimeStamp();
    }

    private IEnumerator PassCO(Vector3 main, Transform target, float height, float Speed)
    {
        SetKickType(KickType.NORMAL);
        float Animation = 0;

        float speed = Speed;
        if (speed <= 0)
            speed = 1;


        float value = speed / 10;
        value = Mathf.Abs(value - 0.9f);

        float finalSpeed = TimeToPass * (value);

        while (Animation < finalSpeed - _pass_threshold)
        {

            Animation += Time.deltaTime;

            Animation = Animation % finalSpeed;

            Vector3 position = MathParabola.Parabola(main, target.position, height, Animation / finalSpeed);

            transform.position = position;

            yield return null;
        }

        if (_ballState.Equals(BallState.PASSED))
            _ballState = BallState.NONE;

        ControleFisica();
    }

    public bool Avaliable()
    {
        if (onPlayer)
            return false;

        if (!AvaliableWhenPassing && _ballState.Equals(BallState.PASSED))
            return false;

        if (Time.time < getBallTimeStamp)
            return false;

        return true;
    }

    public void SetKickType(KickType kick)
    {
        _currentKick = kick;
    }

    public KickType GetKickType()
    {
        return _currentKick;
    }
}
