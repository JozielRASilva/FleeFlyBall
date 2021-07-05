using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonActions : MonoBehaviour
{
    public UnityAction OnButtonDown;
    public UnityAction OnButtonHold;
    public UnityAction OnButtonUp;

    bool holding;

    void Start()
    {
        OnButtonDown = new UnityAction(Nothing);
        OnButtonHold = new UnityAction(Nothing);
        OnButtonUp = new UnityAction(Nothing);
    }

    void Update()
    {
        if (holding)
        {
            OnButtonHold.Invoke();
        }
    }

    public void ButtonDown()
    {
        OnButtonDown.Invoke();

        holding = true;
    }

    public void ButtonUp()
    {
        OnButtonUp.Invoke();

        holding = false;
    }

    void Nothing()
    {
        
    }
}
