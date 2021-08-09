using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBalance : MonoBehaviour
{

    public GameObject main;

    public Image Fill;

    public bool FillDelay = true;

    public Image DelayedFill;

    public float FillDelaySpeed = 3f;

    private Balance _balance;

    private Character _character;

    private float _currentDelayValue;

    [SerializeField]
    private TeamMember _teamMember;

    private void Awake()
    {
        _character = GetComponent<Character>();

        if (!_character)
            _character = GetComponentInParent<Character>();

        if (!_character)
            _character = GetComponentInChildren<Character>();

        if (!_character)
            return;

        _balance = _character.GetComponent<Balance>();
        if (!_balance)
            _balance = _character.GetComponentInParent<Balance>();
        if (!_balance)
            _balance = _character.GetComponentInChildren<Balance>();

        _balance.OnUpdateBalance += UpdateBalance;

        _balance.OnLossAllBalance += LossAllBalance;

        _balance.OnRecoverAllBalance += RecoverAllBalance;


        _teamMember = GetComponentInParent<TeamMember>();
       
        _teamMember.OnSelected += Show;

        _teamMember.UnSelected += Hide;

        _teamMember.OnSetMain += Show;

        _teamMember.OnRemoveMain += Hide;


        transform.parent = null;

    }

    public void Show()
    {
        main.SetActive(true);
        Debug.Log("Show");
    }

    public void Hide()
    {
        Debug.Log("Hide");
        main.SetActive(false);
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

        if (!_character)
            return;

        transform.position = _character.transform.position;
    }

    private void UpdateDelayedFill()
    {
        if (!DelayedFill)
            return;

        _currentDelayValue = Mathf.Lerp(_currentDelayValue, Fill.fillAmount, FillDelaySpeed * Time.deltaTime);

        DelayedFill.fillAmount = _currentDelayValue;
    }

}
