using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeamAreaController : MonoBehaviour
{

    public bool isDebug = true;

    public TeamMember teamMember;

    private TeamArea _teamArea;

    private TeamGroup _teamGroup;

    private Dictionary<AreaBase, TeamMember> cover = new Dictionary<AreaBase, TeamMember>();

    private List<AreaBase> areas = new List<AreaBase>();

    private void Awake()
    {
        _teamArea = GetComponent<TeamArea>();

        _teamGroup = GetComponent<TeamGroup>();
    }

    private void Start()
    {
        areas = _teamArea.GetAreas();

        foreach (var area in areas)
        {
            cover.Add(area, null);
        }
    }

    public Vector3 point;

    public int interval = 1;

    private void Update()
    {
        DistributeAreas();

        if (!isDebug)
            return;

        Vector3 center = _teamArea.GetCenter();

        foreach (var c in cover)
        {
            if (!c.Value)
                continue;

            Debug.DrawLine(c.Key.GetPosition(center), c.Value.transform.position, Color.blue);

            Debug.DrawLine(c.Key.GetPosition(center), c.Key.GetPosition(center) + Vector3.up * 2, Color.red);

            Debug.DrawLine(c.Value.transform.position, c.Value.transform.position + Vector3.up * 2, Color.red);
        }

        if (!teamMember)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            point = GetRandomPointOnArea(teamMember);
        }

        Debug.DrawLine(
            new Vector3(point.x, 0, point.y),
            new Vector3(point.x, 0, point.y) + Vector3.up * 2, Color.green);

    }


    public void DistributeAreas()
    {
        Vector3 center = _teamArea.GetCenter();

        List<TeamMember> _alreadySelected = new List<TeamMember>();

        foreach (var area in areas)
        {

            TeamMember selected = null;
            float lastdistance = 0;

            SelectMember(center, _alreadySelected, area, ref selected, ref lastdistance);

            if (selected)
            {
                cover[area] = selected;
                _alreadySelected.Add(selected);
            }
            else
                cover[area] = null;

        }
    }

    private void SelectMember(Vector3 center, List<TeamMember> _alreadySelected, AreaBase area, ref TeamMember selected, ref float lastdistance)
    {
        foreach (var member in _teamGroup.TeamMembers)
        {
            if (!member.IsMain && !_alreadySelected.Contains(member))
            {
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

    public Vector3 GetRandomPointOnArea(TeamMember member)
    {
        AreaBase area = null;

        foreach (var c in cover)
        {
            if (!c.Value.Equals(member))
                continue;
            area = c.Key;
        }

        if (area == null)
            return Vector3.zero;


        Vector3 position = area.GetPosition(_teamArea.GetCenter());

        Vector3 size = area.Size;

        Vector2 weight = new Vector2(-(size.z / 2), size.z / 2);

        int Z = GetRandomInRange(weight, interval);

        Vector2 lenght = new Vector2(-(size.x / 2), size.x / 2);

        int X = GetRandomInRange(lenght, interval);

        return new Vector3(X, 0, Z);
    }

    public int GetRandomInRange(Vector2 range, int interval = 1)
    {
        Vector2Int rangeInt = new Vector2Int((int)range.x, (int)range.y);

        int value = Random.Range(rangeInt.x, rangeInt.y);

        if (value % interval != 0)
        {
            Debug.Log("Invalid value");
        }

        return value;

    }

}
