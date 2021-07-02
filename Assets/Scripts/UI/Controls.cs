using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Controls : MonoBehaviour
{
    public FloatingJoystick joystick;

    public Button skillButton;
    public Button passButton;
    public Button shootButton;
    public Button interceptButton;
    public Button switchButton;
    public Button sprintButton;

    public UnityAction OnMovedJoystick; 

    Vector2 joystickPosition;

    void Start()
    {
        
    }

    void Update()
    {
        joystickPosition = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (joystickPosition != Vector2.zero)
        {
            OnMovedJoystick.Invoke();
        }
    }

    public Vector2 GetJoystickPosition()
    {
        return joystickPosition;
    }

    public void ButtonDown(string buttonName)
    {
        OnButtonDown.Invoke(buttonName);
    }

    public void ButtonHold(string buttonName)
    {
        OnButtonHold.Invoke(buttonName);
    }

    public void ButtonUp(string buttonName)
    {
        OnButtonUp.Invoke(buttonName);
    }
}
