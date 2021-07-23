using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRecoverBalance : AbilityBase
{

    public FloatSO DelayToRecover;

    [SerializeField]
    private bool _alreadyRecover;

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void ProcessAbility()
    {
        ResetRecover();

        if (!Authorized)
            return;

        if (!CanRecover())
            return;


        Recover();


    }

    private void ResetRecover()
    {
        if (!_character.BallPossession.HasBall())
        {
            if (_alreadyRecover)
                _alreadyRecover = false;
        }
    }

    public bool CanRecover()
    {
        if (_alreadyRecover)
            return false;

        if (!_character.BallPossession.HasBall())
            return false;

        if (_character.GetStatus() == CharacterInfo.Status.STUNNED)
            return false;

        if (_character.GetCharacterState() == CharacterInfo.CharacterStates.Walking)
            return false;

        return true;
    }

    private void Recover()
    {
        Debug.Log("Recover");
        _alreadyRecover = true;
        StartCoroutine(RecoverSO());
    }

    private IEnumerator RecoverSO()
    {
        float delay = DelayToRecover == null ? 1 : DelayToRecover.value;

        WaitForSeconds wait = new WaitForSeconds(delay);

        _character.balance.RecoverAllBalance();

        _character.balance.BlockCost();

        _character.SetCharacterState(CharacterInfo.CharacterStates.RecoveringBalance);

        yield return wait;

        _character.balance.UnBlockCost();

        _character.SetCharacterState(CharacterInfo.CharacterStates.Idle);
    }

}
