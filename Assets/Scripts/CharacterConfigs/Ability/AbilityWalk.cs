using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWalk : AbilityBase
{

    public float lookSpeed = 5;

    [Header("Speed Multipliers")]

    [SerializeField]
    private float defaultMultiplier = 1;

    [SerializeField]
    private float sprintMultiplier = 2;

    [SerializeField]
    private float ballPossessionMultiplier = 0.5f;

    private float _speed = 5;

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();
    private Vector2 LastLookDirection;
    private bool Authorized
    {
        get
        {
            if (!AbilityPermitted)
                return false;

            if (BlockOnStates.Contains(_character.GetCharacterState()))
                return false;

            return true;
        }
    }

    protected override void InitStatus()
    {
        CharacterStatusSO statusSO = _character.status;

        if (!statusSO)
            return;

        _speed = _character.status.Speed;
    }

    protected override void ProcessAbility()
    {
        // Input
        Vector2 inputDirection = ExecuteAction();

        Vector3 direction = new Vector3(1 * inputDirection.x, 0, 1 * inputDirection.y);

        // Movement
        Vector3 resultSpeed = direction * _speed * Time.deltaTime;
        _characterController.Move(resultSpeed);

        // Look at direction
        Vector3 inputVector = new Vector3(inputDirection.x, 0, inputDirection.y);

        if (inputVector.Equals(Vector3.zero))
        {
            inputVector.x = LastLookDirection.x;
            inputVector.y = 0;
            inputVector.z = LastLookDirection.y;
        }


        LastLookDirection.x = inputVector.x;
        LastLookDirection.y = inputVector.z;

        if (inputVector.Equals(Vector2.zero))
            return;

        Debug.DrawLine(_character.transform.position, _character.transform.position + inputVector * 5, Color.red);

        Quaternion newRotation = Quaternion.LookRotation(inputVector, _character.transform.up);

        _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, newRotation, Time.deltaTime * lookSpeed);


    }

    private Vector2 ExecuteAction()
    {
        switch (_character.control)
        {
            case Character.ControlType.PLAYER:

                if (InputController.Instance)
                {
                    var inputBases = InputController.Instance.GetInput(inputs);
                    for (int i = 0; i < inputBases.Count; i++)
                    {
                        Vector2 value = inputBases[i].ButtomAxis();
                        if (Vector2.zero != value)
                            return value;
                    }
                }
                break;


            case Character.ControlType.AI:
                // Set AI input here
                break;
        }

        return Vector2.zero;
    }
}
