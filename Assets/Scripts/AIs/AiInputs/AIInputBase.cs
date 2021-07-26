using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIInputBase<T> where T : new()
{
    public bool CanPerformInput;

    public void Perform()
    {
        CanPerformInput = true;
    }

    public void StopPerform()
    {
        CanPerformInput = false;
    }

    public abstract T GetValue(AICharacterBase AI);
}
