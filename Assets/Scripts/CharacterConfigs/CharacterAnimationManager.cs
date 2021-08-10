using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private AbilityShoot abilityShoot;
    [SerializeField]
    private AbilityShoot abilitySkill;

    [SerializeField]
    private Animator _animator;

    [Header("Setup Meshes")]
    public CharacterVisualSO characterVisual;
    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer body;


    [Header("Triggers")]
    public string OnKick = "OnKick";
    public string OnSpecial = "OnSpecial";
    public string OnPass = "OnPass";
    public string OnReceive = "OnReceive";
    public string OnLostBalance = "OnLostBalance";
    public string OnIntercept = "OnIntercept";

    [Header("Bools")]
    public string idle = "Idle";
    public string walk = "Walk";
    public string run = "Run";

    public string idleBall = "IdleBall";
    public string walkBall = "WalkBall";

    private AbilityPass abilityPass;
    private AbilityDomainBall abilityDomain;
    private AbilityToTakeBall abilityToTake;

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
        if (hair)
            hair.materials = characterVisual.HairMaterial.ToArray();
        if (skin)
            skin.materials = characterVisual.SkinMaterial.ToArray();
        if (body)
            body.materials = characterVisual.BodyMaterial.ToArray();

        if (!abilityShoot)
            abilityShoot = _character.GetComponentInChildren<AbilityShoot>();

        abilityShoot.OnShoot += TriggerShoot;

        if (!abilitySkill)
            abilityShoot = _character.GetComponentsInChildren<AbilityShoot>()[1];

        abilitySkill.CallSpecial += TriggerSpecial;

        abilityPass = _character.GetComponentInChildren<AbilityPass>();
        abilityPass.OnPass += TriggerPass;

        _character.BallPossession.OnReceive += TriggerReceive;

        _character.balance.OnLossAllBalance += TriggerLostBalance;

        abilityDomain = _character.GetComponentInChildren<AbilityDomainBall>();
        abilityToTake = _character.GetComponentInChildren<AbilityToTakeBall>();

        abilityDomain.OnDomain += TriggerIntercept;
        abilityToTake.OnTakeBall += TriggerIntercept;

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

    public void TriggerSpecial()
    {
        _animator.SetTrigger(OnSpecial);
    }

    public void TriggerLostBalance()
    {
        _animator.SetTrigger(OnLostBalance);
    }

    public void TriggerIntercept()
    {
        _animator.SetTrigger(OnIntercept);
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
        bool idleBool = _character.GetCharacterState() == CharacterInfo.CharacterStates.Idle;
        _animator.SetBool(idle, idleBool);

        bool idleBall = idleBool && _character.BallPossession.HasBall();
        _animator.SetBool(this.idleBall, idleBall);
    }

    private void WalkConfig()
    {
        bool walkBool = _character.GetCharacterState() == CharacterInfo.CharacterStates.Walking || _character.GetCharacterState() == CharacterInfo.CharacterStates.Running;
        _animator.SetBool(walk, walkBool);

        bool walkBall = walkBool && _character.BallPossession.HasBall();
        _animator.SetBool(this.walkBall, walkBall);

        bool runBool = walkBool && _character.GetCharacterState() == CharacterInfo.CharacterStates.Running;
        _animator.SetBool(run, runBool);
    }


}
