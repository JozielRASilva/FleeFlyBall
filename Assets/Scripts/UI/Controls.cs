using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Controls : MonoBehaviour
{
    public FloatingJoystick joystick;

    public ButtonActions skillButton;
    public ButtonActions passButton;
    public ButtonActions shootButton;
    public ButtonActions interceptButton;
    public ButtonActions switchButton;
    public ButtonActions sprintButton;

    public UnityAction OnMovedJoystick;

    Vector2 joystickPosition;

    void Start()
    {
        OnMovedJoystick = new UnityAction(Nothing);
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

    public void SwitchControls(bool withBall)
    {
        skillButton.gameObject.SetActive(withBall);
        passButton.gameObject.SetActive(withBall);
        shootButton.gameObject.SetActive(withBall);
        interceptButton.gameObject.SetActive(!withBall);
        switchButton.gameObject.SetActive(!withBall);
        sprintButton.gameObject.SetActive(!withBall);
    }

    void Nothing()
    {
        
    }
}
