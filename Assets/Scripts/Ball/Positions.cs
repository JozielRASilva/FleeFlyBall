using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour
{
    public Transform ballPosition;
    public Transform ball;

    private Ball _ball;
        
    public TeamController _teamController;

    private TeamGroup _redTeam;
    private TeamGroup _greenTeam;

    public List<Transform> CharactersGreenStart;
    public List<Transform> CharactersRedStart;

    private void Awake()
    {
        _ball = ball.GetComponent<Ball>();
    }

    private void Start()
    {
        _redTeam = _teamController.GetOpponentGroup();
        _greenTeam = _teamController.GetPlayerGroup();
    }
    public void GolPosition()
    {
        _ball.Deattach();
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.position = ballPosition.transform.position;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        for (int i = 0; i < _greenTeam.TeamMembers.Count; i++)
        {
            
            GameObject greenObject = _greenTeam.TeamMembers[i].gameObject;

            greenObject.GetComponent<CharacterController>().enabled = false;
            greenObject.transform.position = CharactersGreenStart[i].transform.position;
            greenObject.transform.rotation = CharactersGreenStart[i].transform.rotation;
            greenObject.GetComponent<CharacterController>().enabled = true ;

        }

        for (int i = 0; i < _greenTeam.TeamMembers.Count; i++)
        {
            GameObject redObject = _redTeam.TeamMembers[i].gameObject;
            redObject.GetComponent<CharacterController>().enabled = false;
            redObject.transform.position = CharactersRedStart[i].transform.position;
            redObject.transform.rotation = CharactersRedStart[i].transform.rotation;
            redObject.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GolPosition();
        }
    }



}
