using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Balance : MonoBehaviour
{

    public FloatSO RecoverSpeed;
    public float timeToRecover;
    public FloatSO TimeToRecoverBalance;

    public FloatSO BalanceMultiplier;

    private float _currentBalance;


    private float multiplier => BalanceMultiplier ? BalanceMultiplier.value : 1;
    private float _maxBalance => status ? status.Balance * multiplier : 1 * multiplier;
    public float CurrentBalance { get => _currentBalance; }

    public float MaxBalance { get => _maxBalance; }

    public Action OnUpdateBalance;

    public Action OnLossAllBalance;

    public Action OnRecoverAllBalance;

    private CharacterStatusSO status;

    private Character _character;

    private Coroutine _recoverBalance;

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

        RecoverAllBalance();
    }

    public bool HasBalance()
    {
        return _currentBalance > 0;
    }
    public bool UseBalance(float cost)
    {
        if (cost == 0)
            return false;

        if (_currentBalance <= 0)
            return false;

        if (_currentBalance - cost < 0)
        {
            _currentBalance = 0;
            OnUpdateBalance?.Invoke();
            OnLossAllBalance?.Invoke();

            if (_recoverBalance != null)
                StopCoroutine(_recoverBalance);

            return true;
        }

        _currentBalance -= cost;
        OnUpdateBalance?.Invoke();

        if (_recoverBalance != null)
            StopCoroutine(_recoverBalance);

        _recoverBalance = StartCoroutine(RecoverByTime());

        return true;
    }

    public void RecoverBalance(float value)
    {
        if (value == 0)
            return;

        if (_currentBalance + value < _maxBalance)
        {

            _currentBalance += value;
            OnUpdateBalance?.Invoke();

        }
        else
        {

            RecoverAllBalance();

        }


    }

    public void RecoverAllBalance()
    {
        _currentBalance = _maxBalance;
        OnUpdateBalance?.Invoke();
        OnRecoverAllBalance?.Invoke();

        if (_recoverBalance != null)
            StopCoroutine(_recoverBalance);
    }

    public void ReBreath()
    {
        Debug.Log("Reabreath");
        if (_recoverBalance != null)
            StopCoroutine(_recoverBalance);

        StartCoroutine(RecoverBalance());
    }
    private IEnumerator RecoverBalance()
    {
       
        WaitForSeconds wait = new WaitForSeconds(TimeToRecoverBalance.value);

        yield return wait;

        RecoverAllBalance();

        if (_character.GetStatus() == CharacterInfo.Status.STUNNED)
        {
            _character.SetStatus(CharacterInfo.Status.NORMAL);
        }

    }

    private IEnumerator RecoverByTime()
    {
        WaitForSeconds wait = new WaitForSeconds(timeToRecover);

        yield return wait;

        while (_currentBalance < _maxBalance)
        {

            RecoverBalance(RecoverSpeed.value * Time.deltaTime);

            yield return new WaitForEndOfFrame();

        }

        RecoverAllBalance();
    }

}
