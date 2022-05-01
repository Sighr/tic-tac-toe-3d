using UnityEngine;

public class GameController : MonoBehaviour
{
    public WinCondition winCondition;
    
    public CommonGameEvent gameEnded;
    public CommonGameEvent gameStarted;

    public void OnTurnPerformed()
    {
        if (!winCondition.HasGameEnded)
        {
            return;
        }
        gameEnded.RaiseEvent();
    }
}

