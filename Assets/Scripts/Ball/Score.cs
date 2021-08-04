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

    private void Update()
    {
        redTextScore.text = redScore.ToString();
        greenTextScore.text = greenScore.ToString();
    }
}
