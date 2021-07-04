using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAxis : InputBase
{
    [Header("Input Info"), SerializeField]
    private InputSO InputName;

    public override InputSO Name { get { return InputName; } }


    [Header("Input Cofig"), Space]
    public string XButtonName = "Horizontal";
    public string YButtonName = "Vertical";
    public override Vector2 ButtomAxis()
    {
        float horizontal = Input.GetAxis(XButtonName);
        float vertical = Input.GetAxis(YButtonName);

        Vector2 value = new Vector2(horizontal, vertical);

        return value;
    }

    public override bool ButtomDown()
    {
        return Input.GetButtonDown(XButtonName) || Input.GetButtonDown(YButtonName);
    }

    public override bool ButtomHold()
    {
        return Input.GetButton(XButtonName) || Input.GetButton(YButtonName);
    }

    public override bool ButtomUp()
    {
        return Input.GetButtonUp(XButtonName) || Input.GetButtonUp(YButtonName);
    }

    public override Vector2 ButtomAxisRaw()
    {
        float horizontal = Input.GetAxisRaw(XButtonName);
        float vertical = Input.GetAxisRaw(YButtonName);

        Vector2 value = new Vector2(horizontal, vertical);

        return value;
    }

}
