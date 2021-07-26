using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeamAreaController : MonoBehaviour
{

    public bool isDebug = true;

    private TeamArea _teamArea;

    private TeamGroup _teamGroup;

    private Dictionary<AreaBase, TeamMember> cover = new Dictionary<AreaBase, TeamMember>();

    private void Awake()
    {
        _teamArea = GetComponent<TeamArea>();

        _teamGroup = GetComponent<TeamGroup>();
    }

    private void Start()
    {
        foreach (var area in _teamArea.GetAreas())
        {
            cover.Add(area, null);
        }
    }

    private void Update()
    {
        DistributeAreas();

        if (!isDebug)
            return;

        Vector3 center = _teamArea.GetCenter();

        foreach (var c in cover)
        {
            Debug.DrawLine(c.Key.GetPosition(center), c.Value.transform.position, Color.blue);

            Debug.DrawLine(c.Key.GetPosition(center), c.Key.GetPosition(center) + Vector3.up * 2, Color.red);

            Debug.DrawLine(c.Value.transform.position, c.Value.transform.position + Vector3.up * 2, Color.red);
        }

    }


    public void DistributeAreas()
    {
        Vector3 center = _teamArea.GetCenter();

        List<TeamMember> _alreadySelected = new List<TeamMember>();

        foreach (var area in _teamArea.GetAreas())
        {

            TeamMember selected = null;
            float lastdistance = 0;

            foreach (var member in _teamGroup.TeamMembers)
            {
                if (member.IsMain)
                    continue;

                if (_alreadySelected.Contains(member))
                    continue;

                float distance = area.GetDistance(center, member.transform.position);

                if (!selected)
                {
                    selected = member;
                    lastdistance = distance;
                }
                else if (distance < lastdistance)
                {
                    selected = member;
                    lastdistance = distance;
                }
            }

            if (selected)
            {
                cover[area] = selected;
                _alreadySelected.Add(selected);
            }
            else
                cover[area] = null;

        }
    }

    public AreaBase GetArea(TeamMember member)
    {
        foreach (var c in cover)
        {
            if (c.Value.Equals(member))
                return c.Key;
        }

        return null;
    }

    public void RemoveFromArea(TeamMember member)
    {
        AreaBase key = null;
        foreach (var c in cover)
        {
            if (c.Value.Equals(member))
                key = c.Key;
        }

        if (key == null)
            return;

        cover[key] = null;

    }

}
