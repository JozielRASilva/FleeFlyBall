using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputButton : AIInputBase<bool>
{
    public override bool GetValue(AICharacterBase AI)
    {
        if (_PerformFixed)
            return _fixedValue;

        return CanPerformInput;
    }
}
