using UnityEngine;
using UnityEngine.Events;

public class CommonGameEventListener : MonoBehaviour
{
    public CommonGameEvent gameEvent;
    public UnityEvent unityEvent;

    public void OnEventRaised()
    {
        unityEvent.Invoke();
    }

    public void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    
    public void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
}