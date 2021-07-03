using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IInput
{
    InputSO Name { get; set; }
    bool ButtomDown();
    bool ButtomUp();
    bool ButtomHold();
    Vector2 ButtomAxis();
    Vector2Int ButtomAxisRaw();

}


