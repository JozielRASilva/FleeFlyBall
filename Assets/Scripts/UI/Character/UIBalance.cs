using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBalance : MonoBehaviour
{
    public Image Fill;

    public bool FillDelay = true;
    public Image DelayedFill;
    public float delay = 0.5f;
    private Balance _balance;

    private float _currentDelayValue;
    private void Awake()
    {
        _balance = GetComponent<Balance>();

        if (!_balance)
            _balance = GetComponentInParent<Balance>();

        if (!_balance)
            _balance = GetComponentInChildren<Balance>();

        if (!_balance)
            return;

        _balance.OnUpdateBalance += UpdateBalance;

        _balance.OnLossAllBalance += LossAllBalance;

        _balance.OnRecoverAllBalance += RecoverAllBalance;
    }


    private void UpdateBalance()
    {
        if (!Fill)
            return;

        float value = Mathf.InverseLerp(0, _balance.MaxBalance, _balance.CurrentBalance);

        Fill.fillAmount = value;

    }

    private void LossAllBalance()
    {

        UpdateBalance();

    }

    private void RecoverAllBalance()
    {

        UpdateBalance();

    }

    private void LateUpdate()
    {
        UpdateDelayedFill();
    }

    public void UpdateDelayedFill()
    {
        if (!DelayedFill)
            return;

        _currentDelayValue = Mathf.Lerp(_currentDelayValue, Fill.fillAmount, delay * Time.deltaTime);

        DelayedFill.fillAmount = _currentDelayValue;
    }

}
