using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWalk : AbilityBase
{

    private bool Authorized
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

    protected override void ProcessAbility()
    {
        throw new System.NotImplementedException();
    }
}
