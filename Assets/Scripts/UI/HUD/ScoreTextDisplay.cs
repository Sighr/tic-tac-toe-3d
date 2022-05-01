using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextDisplay : MonoBehaviour
{
    public WinCondition winCondition;
    
    [SerializeField]
    private Text scoreText;
    
    private void OnEnable()
    {
        scoreText.text = winCondition.ShortDescription;
    }

    public void OnTurnPerformed()
    {
        var scores = winCondition.GetScores();
        StringBuilder sb = new StringBuilder();
        foreach (var score in scores)
        {
            sb.Append($"{score.Key.name} {score.Value}    ");
        }
        scoreText.text = sb.ToString();
    }
}