using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJoystick : InputBase
{
    public Controls controlsScript;
    
    [Header("Input Info"), SerializeField]
    private InputSO InputName;

    public override InputSO Name { get { return InputName; } }

    public override Vector2 ButtomAxis()
    {
        return controlsScript.joystickPosition;
    }

    public override bool ButtomDown()
    {
        return controlsScript.joystickDown;
    }

    public override bool ButtomHold()
    {
        return controlsScript.joystickHold;
    }

    public override bool ButtomUp()
    {
        return controlsScript.joystickUp;
    }

    public override Vector2 ButtomAxisRaw()
    {
        return Vector2.zero;
    }

}
