using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{

    public bool AbilityPermitted = true;

    public List<CharacterInfo.CharacterStates> BlockOnStates = new List<CharacterInfo.CharacterStates>();

    private bool Authorized
    {
        get
        {
            bool value = true;



            return value;
        }
    }

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        if (_character)
            _character = GetComponentInParent<Character>();
        if (_character)
            _character = GetComponentInChildren<Character>();
    }

    public void PerformAbility()
    {
        ProcessAbility();
    }

    protected abstract void ProcessAbility();

}
