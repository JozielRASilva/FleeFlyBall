using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{

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


}
