using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : Singleton<InputController>
{
    public Action<IInput> OnInputRaise;
    public Action<IInput> OnInputListen;
    private void Awake()
    {
        OnInputRaise += InputListen;
    }

    private void InputListen(IInput input)
    {
        OnInputListen?.Invoke(input);
    }


}
