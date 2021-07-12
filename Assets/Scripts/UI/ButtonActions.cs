using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonActions : MonoBehaviour
{
    public UnityAction OnButtonDown;
    public UnityAction OnButtonHold;
    public UnityAction OnButtonUp;

    public bool buttonDown;
    public bool buttonHold;
    public bool buttonUp;

    void Start()
    {
        OnButtonDown = new UnityAction(Nothing);
        OnButtonHold = new UnityAction(Nothing);
        OnButtonUp = new UnityAction(Nothing);
    }

    void Update()
    {
        if (buttonHold)
        {
            OnButtonHold.Invoke();
        }
    }

    private void LateUpdate()
    {
        buttonDown = false;
        buttonUp = false;
    }

    public void ButtonDown()
    {        
        OnButtonDown.Invoke();

        buttonDown = true;
        buttonHold = true;
    }

    public void ButtonUp()
    {
        OnButtonUp.Invoke();

        buttonUp = true;
        buttonHold = false;
    }

    void Nothing()
    {
        
    }
}
