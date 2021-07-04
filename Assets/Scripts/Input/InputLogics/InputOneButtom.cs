using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOneButtom : InputBase
{
    [Header("Input Info"), SerializeField]
    private InputSO InputName;

    public override InputSO Name { get => InputName; }

    [Header("Input Cofig"), Space]
    public string ButtonName = "Fire";
    public override Vector2 ButtomAxis()
    {
        return Vector2.zero;
    }

    public override bool ButtomDown()
    {
        return Input.GetButtonDown(ButtonName);
    }

    public override bool ButtomHold()
    {
        return Input.GetButton(ButtonName);
    }

    public override bool ButtomUp()
    {
        return Input.GetButtonUp(ButtonName);
    }

    public override Vector2 ButtomAxisRaw()
    {
        return Vector2.zero;
    }

}
