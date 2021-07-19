using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{

    public CharacterStatusSO status;

    [HideInInspector]
    public Balance balance;

    public enum ControlType { PLAYER, AI, NULL }

    public ControlType control = ControlType.PLAYER;

    public CharacterInfo.Status _currentStatus = CharacterInfo.Status.NORMAL;
    public BallPossession BallPossession { get { return _ballPossession; } }

    public TeamMember Team => _team;

    private TeamMember _team;

    private BallPossession _ballPossession;

    private CharacterInfo.CharacterStates _currentState = CharacterInfo.CharacterStates.None;


    private CharacterController _characterController;

    private Rigidbody _rigibody;

    private List<AbilityBase> Abilities = new List<AbilityBase>();

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _rigibody = GetComponent<Rigidbody>();

        SetAbilities(GetComponents<AbilityBase>().ToList());
        SetAbilities(GetComponentsInParent<AbilityBase>().ToList());
        SetAbilities(GetComponentsInChildren<AbilityBase>().ToList());


        _ballPossession = GetComponent<BallPossession>();
        if (!_ballPossession)
            _ballPossession = GetComponentInChildren<BallPossession>();
        if (!_ballPossession)
            _ballPossession = GetComponentInParent<BallPossession>();


        balance = GetComponent<Balance>();
        if (!balance)
            balance = GetComponentInChildren<Balance>();
        if (!balance)
            balance = GetComponentInParent<Balance>();

        _team = GetComponent<TeamMember>();

    }


    private void Update()
    {
        ProcessAllAbilitiesUpdate();
    }

    public void SetCharacterState(CharacterInfo.CharacterStates _state)
    {
        _currentState = _state;
    }

    public CharacterInfo.CharacterStates GetCharacterState()
    {
        return _currentState;
    }

    public void SetStatus(CharacterInfo.Status _status)
    {
        _currentStatus = _status;
    }

    public CharacterInfo.Status GetStatus()
    {
        return _currentStatus;
    }

    private void SetAbilities(List<AbilityBase> _abilities)
    {
        if (_abilities == null)
            return;

        foreach (var ability in _abilities)
        {
            Abilities.Add(ability);
        }
    }

    private void ProcessAllAbilitiesUpdate()
    {
        foreach (var ability in Abilities)
        {
            ability.PerformAbility();
        }
    }


}
