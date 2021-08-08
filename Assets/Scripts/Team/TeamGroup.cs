using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class TeamGroup : MonoBehaviour
{

    public Goal thisGoal;

    public CinemachineVirtualCamera CVCamera;

    public bool isPlayerGroup = true;

    public bool nextToBall = true;

    public enum Focus { BALLDOMAIN, NEXTTOBALL, NONE }

    private Focus _currentFocus = Focus.NONE;

    [SerializeField]
    private List<TeamMember> teamMembers = new List<TeamMember>();

    public List<TeamMember> TeamMembers => teamMembers;

    private Ball ball;

    public Action OnGetBall;
    public Action OnLoseBall;

    private bool _teamHasBall = true;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();

        foreach (var member in teamMembers)
        {
            member.group = this;
        }

    }

    public void ResetMain()
    {
        Debug.Log("Reset");
        TeamMember member = teamMembers[0];
        if (!member)
            return;

        if (isPlayerGroup)
        {
            Debug.Log("Is main setted");
            member.SetAsMain();
            if (CVCamera)
                CVCamera.Follow = member.transform;
        }
        else
            member.SetAsMainAI();


        foreach (var m in teamMembers)
        {
            if (m.Equals(member))
                continue;

            m.SetAsAI();
        }


    }

    public TeamMember GetMain()
    {
        foreach (var member in teamMembers)
        {
            if (member.IsMain)
                return member;
        }

        return null;
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
                if (member.BallPossession.HasBall())
                    member.SetAsMainAI();
                else
                    member.SetAsAI();
            }
        }

        TeamPossessionChecker();

    }

    private void TeamPossessionChecker()
    {
        if (HasBall())
        {
            if (!_teamHasBall)
            {
                OnGetBall?.Invoke();
                _teamHasBall = true;
            }
        }
        else
        {
            if (_teamHasBall)
            {
                OnLoseBall?.Invoke();
                _teamHasBall = false;
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

    public bool HasBall()
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
