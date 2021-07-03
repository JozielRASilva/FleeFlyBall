using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class InputUnit: MonoBehaviour
{
    public abstract InputSO Name { get;}
    protected abstract bool ButtomDown();
    protected abstract bool ButtomUp();
    protected abstract bool ButtomHold();
    protected abstract Vector2 ButtomAxis();
    protected abstract Vector2 ButtomAxisRaw();
    
}


