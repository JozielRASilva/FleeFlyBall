using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InputController : Singleton<InputController>
{
    private List<InputUnit> _inputUnits = new List<InputUnit>();
    private void Awake()
    {
        _inputUnits = FindObjectsOfType<InputUnit>().ToList();
    }

    public InputUnit GetInput(InputSO input)
    {
        InputUnit inputUnit = _inputUnits.Find(i => i.Equals(input));

        return inputUnit;
    }

}
