using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeamGroup : MonoBehaviour
{

    public CinemachineVirtualCamera CVCamera;

    public bool isPlayerGroup = true;

    public bool nextToBall = true;

    public enum Focus { BALLDOMAIN, NEXTTOBALL, NONE }

    private Focus _currentFocus = Focus.NONE;

    [SerializeField]
    private List<TeamMember> teamMembers = new List<TeamMember>();

    public List<TeamMember> TeamMembers => teamMembers;

    private Ball ball;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();

        foreach (var member in teamMembers)
        {
            member.group = this;
        }

    }


    public bool IsMember(TeamMember member)
    {
        return teamMembers.Contains(member);
    }

    private void Update()
    {

        if (isPlayerGroup)
            SetControl();
        else
        {
            foreach (var member in teamMembers)
            {
                member.SetAsAI();
            }
        }
    }

    private void SetControl()
    {
        if (HasBall() && _currentFocus != Focus.BALLDOMAIN)
        {
            foreach (var member in teamMembers)
            {
                if (member.BallPossession.HasBall())
                {
                    member.SetAsMain();
                    if (CVCamera)
                        CVCamera.Follow = member.transform;

                    _currentFocus = Focus.BALLDOMAIN;
                }
                else
                {
                    member.SetAsAI();
                }
            }
        }
        else if (_currentFocus != Focus.NEXTTOBALL && !HasBall())
        {
            TeamMember selected = SelectByLowDistance();

            foreach (var member in teamMembers)
            {
                if (member != selected)
                {
                    member.SetAsAI();

                    _currentFocus = Focus.NEXTTOBALL;
                }
                else
                {
                    member.SetAsMain();

                    if (CVCamera)
                        CVCamera.Follow = member.transform;
                }
            }

        }
    }

    private bool HasBall()
    {
        foreach (var member in teamMembers)
        {
            if (member)
                if (member.BallPossession.HasBall())
                    return true;
        }
        return false;
    }

    private TeamMember SelectByLowDistance()
    {
        TeamMember selected = null;
        float selectedDistance = 0;

        foreach (var member in teamMembers)
        {
            float distance = Vector3.Distance(member.transform.position, ball.transform.position);
            if (!selected)
            {
                selected = member;

                selectedDistance = distance;
            }
            else
            {
                if (distance < selectedDistance)
                {
                    selected = member;
                    selectedDistance = distance;
                }
            }

        }

        return selected;

    }

}
