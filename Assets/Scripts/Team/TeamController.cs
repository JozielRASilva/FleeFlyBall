using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TeamController : Singleton<TeamController>
{

    private List<TeamGroup> teamGroup = new List<TeamGroup>();

    protected override void Awake()
    {
        base.Awake();

        teamGroup = GetComponentsInChildren<TeamGroup>().ToList();
    }

    public TeamGroup GetPlayerGroup()
    {
        return teamGroup.Find(t => t.isPlayerGroup);
    }

    public TeamGroup GetOpponentGroup()
    {
        return teamGroup.Find(t => !t.isPlayerGroup);
    }


}
