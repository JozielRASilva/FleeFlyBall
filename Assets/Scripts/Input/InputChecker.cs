using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public bool OneInput = true;

    public InputSO inputSO;

    public List<InputSO> inputSOs = new List<InputSO>();
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (!InputController.Instance)
            return;

        List<InputBase> inputs = InputController.Instance.GetInput(inputSOs);

        foreach (var input in inputs)
        {
            Print(input);
        }

    }

    private void Print(InputBase input)
    {
        if (!input.ButtomHold())
            return;
        Debug.Log($"Name: {input.Name.name}");
    }

}
