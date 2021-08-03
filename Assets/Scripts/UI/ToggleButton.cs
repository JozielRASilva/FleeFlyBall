using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour
{
    public RectTransform switchTransform;

    bool isOn;

    Vector2 leftPosition;
    Vector2 rightPosition;

    public UnityAction<bool> OnSwitch;

    void Start()
    {
        leftPosition = switchTransform.localPosition;
        rightPosition = switchTransform.localPosition * Vector2.left;
    }

    void Update()
    {
        
    }

    public void SwitchInstantly()
    {
        isOn = !isOn;

        AjdustPosition();

        OnSwitch.Invoke(isOn);
    }

    public void Set(bool set)
    {
        isOn = set;

        AjdustPosition();
    }

    void AjdustPosition()
    {
        if (isOn)
        {
            switchTransform.localPosition = leftPosition;
        }
        else
        {
            switchTransform.localPosition = rightPosition;
        }
    }
}
