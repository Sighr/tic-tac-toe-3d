using UnityEngine;

public class EndGameButton : MonoBehaviour
{
    public CommonGameEvent gameEnded;
    
    public void OnGameEndedButtonClicked()
    {
        gameEnded.RaiseEvent();
    }
}