using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton : InputBase
{
    public ButtonActions button;

    [Header("Input Info"), SerializeField]
    private InputSO InputName;

    public override InputSO Name { get => InputName; }

    public override Vector2 ButtomAxis()
    {
        return Vector2.zero;
    }

    public override bool ButtomDown()
    {
        return button.buttonDown;
    }

    public override bool ButtomHold()
    {
        return button.buttonHold;
    }

    public override bool ButtomUp()
    {
        return button.buttonHold;
    }

    public override Vector2 ButtomAxisRaw()
    {
        return Vector2.zero;
    }

}
