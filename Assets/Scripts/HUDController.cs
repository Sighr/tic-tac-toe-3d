using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public WinCondition winCondition;
    
    [SerializeField]
    private GameObject HUD;
    
    [SerializeField]
    private Text text;

    private void Start()
    {
        HUD ??= transform.GetChild(0).gameObject;
    }

    public void OnTurnPerformed()
    {
        var scores = winCondition.GetScores();
        StringBuilder sb = new StringBuilder();
        foreach (var score in scores)
        {
            sb.Append($"{score.Key.name} {score.Value}    ");
        }
        text.text = sb.ToString();
    }
    
    public void OnGameEnded()
    {
        HUD.SetActive(false);
    }
    
    public void OnGameStarted()
    {
        HUD.SetActive(true);
    }
}