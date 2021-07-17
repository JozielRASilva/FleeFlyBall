using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPass : AbilityBase
{

    public float angle = 60;

    public float Speed = 5;
    public float height = 5;

    public Transform test;

    [Header("Preview")]
    public Transform main;
    public Transform middle;
    public Transform target;

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();

    protected float Animation;

    private TeamMember _teamMember;

    private BallPossession _ballPossession;

    private float _pass;

    protected override bool Authorized
    {
        get
        {
            if (!AbilityPermitted)
                return false;

            if (BlockOnStates.Contains(_character.GetCharacterState()))
                return false;

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

        if (!selectedMember)
            return;

        // Get input
        if (!ExecuteAction() || !CanPass())
            return;

        // Execute pass

        // Process Pass? Set on Ball?
    }

    private void PassBall()
    {
        Animation += Time.deltaTime;

        Animation = Animation % Speed;

        test.position = MathParabola.Parabola(main.transform.position, target.transform.position, height, Animation / Speed);

    }

    private TeamMember SelectMember()
    {
        TeamMember selected = null;
        float selectedAngle = 0;
        foreach (var member in _teamMember.group.TeamMembers)
        {
            if (member.Equals(_teamMember))
                continue;

            float _angle = GetAngle(member.transform, _character.transform);

            if (_angle < angle)
                continue;

            if (!selected || _angle > selectedAngle)
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
                            return true;
                    }
                }

                break;

            case Character.ControlType.AI:
                // Set AI input here
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
