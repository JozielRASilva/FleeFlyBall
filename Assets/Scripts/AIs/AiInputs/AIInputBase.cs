using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIInputBase<T> where T : new()
{
    public bool CanPerformInput;

    protected bool _PerformFixed;

    protected T _fixedValue;

    public virtual void Perform()
    {
        CanPerformInput = true;
    }

    public virtual void PerformFixed(T value)
    {
        CanPerformInput = true;

        _PerformFixed = true;

        _fixedValue = value;
    }

    public void StopPerform()
    {
        CanPerformInput = false;

        _PerformFixed = false;
    }

    public abstract T GetValue(AICharacterBase AI);
}
