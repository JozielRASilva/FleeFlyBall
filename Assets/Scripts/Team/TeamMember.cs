using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Character))]
public class TeamMember : MonoBehaviour
{

    public BallPossession BallPossession { get => character.BallPossession; }

    [HideInInspector]
    public TeamGroup group;

    public bool IsMain;

    public Action OnSetMain;
    public Action OnRemoveMain;


    private Character character;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void SetAsMain()
    {
        character.control = Character.ControlType.PLAYER;

        IsMain = true;

        OnSetMain?.Invoke();
    }

    public void SetAsAI()
    {
        character.control = Character.ControlType.AI;

        IsMain = false;

        OnRemoveMain?.Invoke();
    }

}
