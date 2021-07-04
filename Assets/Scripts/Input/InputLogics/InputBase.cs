using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class InputBase: MonoBehaviour
{
    public abstract InputSO Name { get;}
    public abstract bool ButtomDown();
    public abstract bool ButtomUp();
    public abstract bool ButtomHold();
    public abstract Vector2 ButtomAxis();
    public abstract Vector2 ButtomAxisRaw();
    
}


