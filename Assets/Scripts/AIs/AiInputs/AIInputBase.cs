using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIInputBase<T> where T : new()
{
    public abstract T GetValue(AICharacterBase AI);
}
