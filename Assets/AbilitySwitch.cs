using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwitch : AbilityBase
{

    private TeamMember _member;

    protected override void Initialize()
    {
        base.Initialize();

        _member = _character.GetComponent<TeamMember>();
    }

    [Header("Inputs")]
    public List<InputSO> inputs = new List<InputSO>();

    protected override void ProcessAbility()
    {
         if (!ExecuteAction())
            return;

        _member.group.SetNextBallAsMain();
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

        }

        return false;
    }
}
