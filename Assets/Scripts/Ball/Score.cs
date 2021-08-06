using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public int redScore;
    public int greenScore;
    

    public TextMeshProUGUI redTextScore;
    public TextMeshProUGUI greenTextScore;
    public TextMeshProUGUI finalWinTextScore;
    public TextMeshProUGUI finalLoseTextScore;
    public TextMeshProUGUI finalDrawTextScore;


    private void Update()
    {
        redTextScore.text = redScore.ToString();
        greenTextScore.text = greenScore.ToString();

        finalWinTextScore.text = $"{greenScore} - {redScore}";
        finalLoseTextScore.text = $"{greenScore} - {redScore}";
        finalDrawTextScore.text = $"{greenScore} - {redScore}";

    }
}
