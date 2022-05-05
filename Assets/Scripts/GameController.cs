using UnityEngine;

public class GameController : MonoBehaviour
{
    public WinCondition winCondition;
    
    public CommonGameEvent gameEnded;

    public void OnTurnPerformed()
    {
        if (!winCondition.HasGameEnded)
        {
            return;
        }
        gameEnded.RaiseEvent();
    }
}

