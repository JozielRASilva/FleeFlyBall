using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIButtonControls : MonoBehaviour
{

    public GameObject DefaultButtons;
    private List<UIButton> defaultUiButtons = new List<UIButton>();

    public GameObject BallPossesionButtons;
    private List<UIButton> possesionUiButtons = new List<UIButton>();

    private TeamGroup playerGroup;

    private void Start()
    {
        playerGroup = TeamController.Instance.GetPlayerGroup();
        
        if (!playerGroup)
            return;

        playerGroup.OnLoseBall += ShowFirstButtons;

        playerGroup.OnGetBall += ShowSecondButtons;

        defaultUiButtons = DefaultButtons?.GetComponentsInChildren<UIButton>().ToList();

        possesionUiButtons = BallPossesionButtons?.GetComponentsInChildren<UIButton>().ToList();

        DefaultButtons?.SetActive(true);

        BallPossesionButtons?.SetActive(true);

    }

    private void ShowFirstButtons()
    {
        foreach (var button in defaultUiButtons)
        {
            button.Show();
        }

        foreach (var button in possesionUiButtons)
        {
            button.Hide();
        }
    }

    private void ShowSecondButtons()
    {
        foreach (var button in defaultUiButtons)
        {
            button.Hide();
        }

        foreach (var button in possesionUiButtons)
        {
            button.Show();
        }
    }



}
