using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private Animator _animator;

    [Header("Triggers")]
    public string OnKick = "OnKick";
    public string OnPass = "OnPass";

    [Header("Bools")]
    public string idle = "Idle";
    public string walk = "Walk";
    public string run = "Run";

    public string idleBall = "IdleBall";
    public string walkBall = "WalkBall";

    private void Awake()
    {
        if (!_character)
            _character = GetComponent<Character>();
        if (!_character)
            _character = GetComponentInParent<Character>();


        if (!_animator)
            _animator = GetComponent<Animator>();
        if (!_animator)
            _animator = GetComponentInParent<Animator>();
    }


    private void Update()
    {
        ControlBools();
    }

    private void ControlBools()
    {

        IdleConfig();

        WalkConfig();

    }

    private void IdleConfig()
    {
        bool idle_bool = _character.GetCharacterState() == CharacterInfo.CharacterStates.Idle;
        _animator.SetBool(idle, idle_bool);

        bool idle_ball = idle_bool && _character.BallPossession.HasBall();
        _animator.SetBool(idleBall, idle_ball);
    }

    private void WalkConfig()
    {
        bool walk_bool = _character.GetCharacterState() == CharacterInfo.CharacterStates.Walking || _character.GetCharacterState() == CharacterInfo.CharacterStates.Running;
        _animator.SetBool(walk, walk_bool);

        bool walk_ball = walk_bool && _character.BallPossession.HasBall();
        _animator.SetBool(walkBall, walk_ball);

        bool run_bool = walk_bool && _character.GetCharacterState() == CharacterInfo.CharacterStates.Running;
        _animator.SetBool(run, run_bool);
    }


}
