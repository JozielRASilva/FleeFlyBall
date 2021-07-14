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

    public Vector2 joystickPosition;
    public bool joystickDown;
    public bool joystickHold;
    public bool joystickUp;

    void Start()
    {

    }

    void Update()
    {        
        joystickPosition = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    private void LateUpdate()
    {
        joystickDown = false;
        joystickUp = false;
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

    public void JoystickDown()
    {
        joystickDown = true;
        joystickHold = true;
    }

    public void JoystickUp()
    {
        joystickUp = true;
        joystickHold = false;
    }
}
