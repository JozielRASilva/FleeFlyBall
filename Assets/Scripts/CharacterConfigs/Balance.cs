using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Balance : MonoBehaviour
{
    public FloatSO TimeToRecoverBalance;

    public FloatSO BalanceMultiplier;
    private float _currentBalance;

    private float multiplier => BalanceMultiplier ? BalanceMultiplier.value : 1;
    private float _maxBalance => status ? status.Balance * multiplier : 1 * multiplier;
    public float CurrentBalance { get => _currentBalance; }

    public Action OnUpdateBalance;

    public Action OnLossAllBalance;

    public Action OnRecoverAllBalance;

    private CharacterStatusSO status;

    private Character _character;
    private void Awake()
    {
        _character = GetComponent<Character>();

        if (!_character)
            _character = GetComponentInChildren<Character>();
        if (!_character)
            _character = GetComponentInParent<Character>();

        if (_character)
            status = _character.status;


        InitStatus();
    }

    private void InitStatus()
    {
        if (!status)
            return;

        _currentBalance = _maxBalance;
    }

    public bool UseBalance(float cost)
    {
        if (_currentBalance <= 0)
            return false;

        if (_currentBalance - cost < 0)
        {
            _currentBalance = 0;
            OnUpdateBalance?.Invoke();
            OnLossAllBalance?.Invoke();

            return true;
        }

        _currentBalance -= cost;
        OnUpdateBalance?.Invoke();
        return true;
    }

    public void RecoverBalance(float value)
    {
        if (_currentBalance + value < _maxBalance)
        {
            _currentBalance += value;
            OnUpdateBalance?.Invoke();
        }
        else
            RecoverAllBalance();
    }

    public void RecoverAllBalance()
    {
        _currentBalance = _maxBalance;
        OnUpdateBalance?.Invoke();
        OnRecoverAllBalance?.Invoke();

    }

    private IEnumerator RecoverBalance()
    {

        WaitForSeconds wait = new WaitForSeconds(TimeToRecoverBalance.value);

        yield return wait;

        RecoverAllBalance();
    }

}
