using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{

    public GameObject canvas;

    public Image image;

    public Color main = Color.white;

    public Color selected = Color.gray;

    public bool debug;

    private TeamMember _teamMember;

    private void Awake()
    {
        _teamMember = GetComponentInParent<TeamMember>();

        _teamMember.OnSelected += Show;
        _teamMember.OnSelected += Selected;

        _teamMember.UnSelected += Hide;

        _teamMember.OnSetMain += Show;
        _teamMember.OnSetMain += Main;

        _teamMember.OnRemoveMain += Hide;



    }

    public void Show()
    {
        if (!_teamMember.group.isPlayerGroup)
            return;
        canvas.SetActive(true);

        if (!debug)
            return;

        Debug.Log($"Show selection of {_teamMember.name}");
    }

    public void Hide()
    {
        canvas.SetActive(false);


        if (!debug)
            return;

        Debug.Log($"Hide selection of {_teamMember.name}");
    }

    public void Main()
    {
        image.color = main;
    }

    public void Selected()
    {
        image.color = selected;
    }

}
