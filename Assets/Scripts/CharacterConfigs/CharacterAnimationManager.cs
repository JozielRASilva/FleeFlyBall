using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    private AbilityShoot abilityShoot;
    private AbilityPass abilityPass;

    [SerializeField]
    private Animator _animator;

    [Header("Setup Meshes")]
    public CharacterVisualSO characterVisual;
    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer body;


    [Header("Triggers")]
    public string OnKick = "OnKick";
    public string OnPass = "OnPass";
    public string OnReceive = "OnReceive";

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

    private void Start()
    {
        skin.materials = characterVisual.SkinMaterial.ToArray();
        body.materials = characterVisual.BodyMaterial.ToArray();


        abilityShoot = _character.GetComponentInChildren<AbilityShoot>();
        abilityShoot.OnShoot += TriggerShoot;
        abilityPass = _character.GetComponentInChildren<AbilityPass>();
        abilityPass.OnPass += TriggerPass;

        _character.BallPossession.OnReceive += TriggerReceive;
    }

    public void TriggerShoot()
    {
        _animator.SetTrigger(OnKick);
    }
    public void TriggerPass()
    {
        _animator.SetTrigger(OnPass);
    }
    public void TriggerReceive()
    {
        _animator.SetTrigger(OnReceive);
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
