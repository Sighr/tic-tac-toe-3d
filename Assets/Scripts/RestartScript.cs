using UnityEngine;

public class RestartScript : MonoBehaviour
{
    public CommonGameEvent gameStarted;
    
    public void OnRestartButtonClicked()
    {
        gameStarted.RaiseEvent();
    }
}