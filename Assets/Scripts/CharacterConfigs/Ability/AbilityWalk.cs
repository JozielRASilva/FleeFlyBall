using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWalk : AbilityBase
{

    public float lookSpeed = 5;

    [Header("Speed Multipliers")]

    [SerializeField]
    private FloatSO defaultMultiplier;
    [SerializeField]
    private FloatSO sprintMultiplier;
    [SerializeField]
    private FloatSO ballPossessionMultiplier;

    [Header("Balance Movement Cost"), SerializeField]
    private FloatSO sprintBalanceCost;

    [SerializeField]
    private FloatSO embassyBalanceCost;


    public enum SpeedType { DEFAULT, SPRINT, EMBASSY }
    private float _speed = 5;

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();

    public List<InputSO> sprintMovementInput = new List<InputSO>();

    private Vector2 LastLookDirection;

    private SpeedType _currentSpeedType = SpeedType.DEFAULT;

    protected override bool Authorized
    {
        get
        {
            if (!AbilityPermitted)
                return false;

            if (BlockOnStates.Contains(_character.GetCharacterState()))
                return false;

            if (_character.GetStatus() == CharacterInfo.Status.STUNNED)
                return false;

            return true;
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
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
        if (!Authorized)
        {
            if (_character.GetCharacterState() == CharacterInfo.CharacterStates.Walking)
                _character.SetCharacterState(CharacterInfo.CharacterStates.Idle);

            return;
        }

        // Input
        Vector2 inputDirection = ExecuteAction();

        Vector3 direction = new Vector3(1 * inputDirection.x, 0, 1 * inputDirection.y);

        // Movement

        float multiplier = GetMultiplier();

        Vector3 resultSpeed = direction * _speed * multiplier * Time.deltaTime;

        // Check speed to remove balance
        ApplyBalanceCost();

        _characterController.Move(resultSpeed);

        if (!inputDirection.Equals(Vector2.zero))
        {
            _character.SetCharacterState(CharacterInfo.CharacterStates.Walking);
        }
        else
        {
            if (_character.GetCharacterState() == CharacterInfo.CharacterStates.Walking)
                _character.SetCharacterState(CharacterInfo.CharacterStates.Idle);
        }

        // Look at direction
        Vector3 inputVector = new Vector3(inputDirection.x, 0, inputDirection.y);

        LookAtDirection(inputVector);

    }

    public void LookAtDirection(Vector3 _inputVector)
    {
        Vector3 inputVector = _inputVector;
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

    public void LookAtDirection(Vector3 _inputVector, float lookSpeed)
    {
        Vector3 inputVector = _inputVector;
        
        Debug.Log($"Input {inputVector}");
        if (inputVector.Equals(Vector3.zero))
        {
            inputVector.x = LastLookDirection.x;
            inputVector.y = 0;
            inputVector.z = LastLookDirection.y;
        }

        Debug.Log($"Input {inputVector}");

        LastLookDirection.x = inputVector.x;
        LastLookDirection.y = inputVector.z;

        if (inputVector.Equals(Vector2.zero))
            return;

        Debug.DrawLine(_character.transform.position, _character.transform.position + inputVector * 5, Color.red);

        Quaternion newRotation = Quaternion.LookRotation(inputVector, _character.transform.up);

        _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, newRotation, Time.deltaTime * lookSpeed);

    }

    private void ApplyBalanceCost()
    {
        float value = 0;
        if (_currentSpeedType == SpeedType.SPRINT)
        {
            value = sprintBalanceCost ? sprintBalanceCost.value / 10 : 0.01f;
        }
        else if (_currentSpeedType == SpeedType.EMBASSY)
        {
            value = embassyBalanceCost ? embassyBalanceCost.value / 10 : 0.01f;
        }
        _character.balance.UseBalance(value);
    }

    private float GetMultiplier()
    {
        SpeedType speedType = SpeedType.DEFAULT;

        if (_character.BallPossession.HasBall())
            speedType = SpeedType.EMBASSY;
        else if (ExecuteSprintAction() && CanSprint())
            speedType = SpeedType.SPRINT;

        float value;

        if (speedType == SpeedType.EMBASSY)
            value = ballPossessionMultiplier ? ballPossessionMultiplier.value : 1;
        else if (speedType == SpeedType.SPRINT)
            value = sprintMultiplier ? sprintMultiplier.value : 1;
        else
            value = defaultMultiplier ? defaultMultiplier.value : 1;


        _currentSpeedType = speedType;

        return value;
    }

    public bool CanSprint()
    {
        if (_character.balance.HasBalance())
            return true;

        return false;
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
                if (characterBase)
                {
                    Vector2 _value = characterBase.inputAxis.GetValue(characterBase);

                    if (_value != Vector2.zero)
                        return _value;
                }
                break;
        }

        return Vector2.zero;
    }

    private bool ExecuteSprintAction()
    {
        switch (_character.control)
        {
            case Character.ControlType.PLAYER:

                if (InputController.Instance)
                {
                    var inputBases = InputController.Instance.GetInput(sprintMovementInput);
                    for (int i = 0; i < inputBases.Count; i++)
                    {
                        if (inputBases[i].ButtomHold())
                            return true;
                    }
                }

                break;

            case Character.ControlType.AI:
                // Set AI input here
                break;
        }

        return false;
    }
}
