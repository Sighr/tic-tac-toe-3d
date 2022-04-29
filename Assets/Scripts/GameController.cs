using UnityEngine;

public class GameController : MonoBehaviour
{
    public PiecesListVariable pieces;
    public WinCondition winCondition;
    public IntVariable gridDimension;
    
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

