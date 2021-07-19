using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    [Header("Logic Control")]
    public bool onPlayer;

    public bool inField;

    public bool grounded;

    public bool AutoControl = true;

    public enum BallState { NONE, KICKED, PASSED }
    public BallState _ballState = BallState.NONE;

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

    private void Awake()
    {
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

            _ballState = BallState.NONE;
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


    }

    private IEnumerator PassCO(Vector3 main, Transform target, float height, float Speed)
    {
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

    public void Deattach()
    {
        transform.parent = null;

        OnDeattach?.Invoke();

        onPlayer = false;

        _rigidbody.isKinematic = false;
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

    public bool Avaliable()
    {
        if (onPlayer)
            return false;

        if (!AvaliableWhenPassing && _ballState.Equals(BallState.PASSED))
            return false;

        return true;
    }
}
