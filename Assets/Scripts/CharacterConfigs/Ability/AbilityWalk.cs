using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWalk : AbilityBase
{

    public float Speed = 5;

    public float LookSpeed = 5;

    protected CharacterController _characterController;
    protected Rigidbody _rigidbody;


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

    protected override void Initialize()
    {
        _characterController = _character.GetComponent<CharacterController>();
        _rigidbody = _character.GetComponent<Rigidbody>();



    }

    protected override void ProcessAbility()
    {
        // Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, vertical);

        Vector3 direction = new Vector3(1 * horizontal, 0, 1 * vertical);

        // Movement
        Vector3 resultSpeed = direction * Speed * Time.deltaTime;
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

        _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, newRotation, Time.deltaTime * LookSpeed);


    }
}
