using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{

    public bool AbilityPermitted = true;

    public List<CharacterInfo.CharacterStates> BlockOnStates = new List<CharacterInfo.CharacterStates>();

    protected virtual bool Authorized
    {
        get
        {
            if (!AbilityPermitted)
                return false;

            if (BlockOnStates.Contains(_character.GetCharacterState()))
                return false;

            return true;
        }
    }

    protected Character _character;
    protected CharacterController _characterController;
    protected Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _character = GetComponent<Character>();
        if (!_character)
            _character = GetComponentInParent<Character>();
        if (!_character)
            _character = GetComponentInChildren<Character>();

        Initialize();
        InitStatus();
    }

    protected virtual void InitStatus()
    {
        CharacterStatusSO statusSO = _character.status;

        if (!statusSO)
            return;
    }

    public void PerformAbility()
    {
        ProcessAbility();
    }

    protected virtual void Initialize()
    {
        _characterController = _character.GetComponent<CharacterController>();
        _rigidbody = _character.GetComponent<Rigidbody>();
    }

    protected abstract void ProcessAbility();

}
