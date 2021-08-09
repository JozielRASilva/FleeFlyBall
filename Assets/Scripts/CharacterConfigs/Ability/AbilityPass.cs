using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityPass : AbilityBase
{

    public float angle = 60;

    public float Speed = 5;
    public float height = 5;

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();


    [Header("Pass Cost"), SerializeField]
    private FloatSO passCost;

    [Header("Audio"), SerializeField]
    public AudioPlayer audioPlayer;
    public string sound;

    protected float Animation;

    private TeamMember _teamMember;

    private TeamMember _lastSelectedMember;

    private BallPossession _ballPossession;

    private float _pass;

    public Action OnPass;

    protected override bool Authorized
    {
        get
        {
            if (!AbilityPermitted)
                return false;

            if (BlockOnStates.Contains(_character.GetCharacterState()))
                return false;

            if (_ballPossession)
                _ballPossession = _character.BallPossession;

            if (!_ballPossession)
                return false;

            if (!_ballPossession.HasBall())
            {
                return false;
            }

            return true;
        }
    }


    protected override void Initialize()
    {
        base.Initialize();

        _teamMember = _character.GetComponent<TeamMember>();



    }

    private void Start()
    {
        _ballPossession = _character.BallPossession;
    }

    protected override void InitStatus()
    {
        base.InitStatus();

        CharacterStatusSO statusSO = _character.status;

        if (!statusSO)
            return;

        _pass = statusSO.Pass;
    }

    protected override void ProcessAbility()
    {

        if (!Authorized)
            return;

        // Select Target

        TeamMember selectedMember = SelectMember();

        if (selectedMember)
            selectedMember.Selected(true);

        if (_lastSelectedMember)
            if (_lastSelectedMember != selectedMember)
                _lastSelectedMember.Selected(false);

        _lastSelectedMember = selectedMember;

        if (!selectedMember)
            return;

        // Get input
        if (!ExecuteAction() || !CanPass())
            return;

        // Execute pass
        _character.BallPossession.ball.Pass(transform.position, selectedMember.transform, height, _pass);

        ApplyBalanceCost();

        OnPass?.Invoke();

        audioPlayer.PlaySound(sound);
    }

    private void ApplyBalanceCost()
    {
        float value = 1;

        if (passCost)
            value = passCost.value;

        _character.balance.UseBalance(value);
    }



    private TeamMember SelectMember()
    {
        TeamMember selected = null;
        float selectedAngle = 0;
        foreach (var member in _teamMember.group.TeamMembers)
        {
            if (member.Equals(_teamMember))
                continue;

            float _angle = GetAngle(_character.transform, member.transform);


            if (_angle > angle)
                continue;

            if (!selected)
            {
                selected = member;
                selectedAngle = _angle;
            }

            if (_angle < selectedAngle)
            {
                selected = member;
                selectedAngle = _angle;
            }


        }

        return selected;
    }

    private float GetAngle(Transform current, Transform target)
    {
        Vector3 dir = target.position - current.position;
        float angle = Vector3.Angle(dir.normalized, current.forward);

        return angle;
    }


    private bool ExecuteAction()
    {        
        switch (_character.control)
        {
            case Character.ControlType.PLAYER:

                if (InputController.Instance)
                {
                    var inputBases = InputController.Instance.GetInput(inputs);
                    for (int i = 0; i < inputBases.Count; i++)
                    {
                        if (inputBases[i].ButtomDown())
                        {
                            return true;
                        }
                    }
                }

                break;

            case Character.ControlType.AI:
                // Set AI input here
                if (characterBase)
                {
                    bool _value = characterBase.inputPass.GetValue(characterBase);

                    return _value;
                }
                break;
        }

        return false;
    }

    public bool CanPass()
    {
        if (_character.BallPossession.HasBall())
            return true;

        return false;
    }


}
