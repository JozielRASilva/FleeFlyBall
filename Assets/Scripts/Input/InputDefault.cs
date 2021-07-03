using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDefault : InputUnit
{
    [Header("Input Info")]
    public InputSO InputName;

    public override InputSO Name { get => InputName; }

    [Header("Input Cofig"), Space]
    public string XButtonName = "Horizontal";
    public string YButtonName = "Vertical";
    protected override Vector2 ButtomAxis()
    {
        float horizontal = Input.GetAxis(XButtonName);
        float vertical = Input.GetAxis(YButtonName);

        Vector2 value = new Vector2(horizontal, vertical);

        return value;
    }

    protected override bool ButtomDown()
    {
        return Input.GetButtonDown(XButtonName) || Input.GetButtonDown(YButtonName);
    }

    protected override bool ButtomHold()
    {
        return Input.GetButton(XButtonName) || Input.GetButton(YButtonName);
    }

    protected override bool ButtomUp()
    {
        return Input.GetButtonUp(XButtonName) || Input.GetButtonUp(YButtonName);
    }

    protected override Vector2 ButtomAxisRaw()
    {
        float horizontal = Input.GetAxisRaw(XButtonName);
        float vertical = Input.GetAxisRaw(YButtonName);

        Vector2 value = new Vector2(horizontal, vertical);

        return value;
    }

}
