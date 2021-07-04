using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InputController : Singleton<InputController>
{
    private List<InputBase> _inputs = new List<InputBase>();
    protected override void Awake()
    {
        base.Awake();
        _inputs = FindObjectsOfType<InputBase>().ToList();
    }

    public List<InputBase> GetInput(List<InputSO> inputs)
    {
        List<InputBase> input = _inputs.FindAll(i => inputs.Exists(o => o.Equals(i.Name)));

        return input;
    }

}
