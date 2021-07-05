using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{

    public CharacterStatusSO status;

    public enum ControlType { PLAYER, AI, NULL }
    public ControlType control = ControlType.PLAYER;
    public BallPossession BallPossession { get { return _ballPossession; } }
    private float _currentBalance;
    private float CurrentBalance { get => _currentBalance; }
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


    private void InitStatus()
    {
        _currentBalance = status.Balance;
    }

    public bool UseBalance(float cost)
    {
        if (_currentBalance - cost < 0)
            return false;

        _currentBalance -= cost;

        return true;
    }

    public void RecoverBalance(float value)
    {
        _currentBalance = value;
    }
    public void RecoverAllBalance(float value)
    {
        _currentBalance = value;
    }

}
