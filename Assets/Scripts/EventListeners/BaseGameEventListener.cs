using UnityEngine;
using UnityEngine.Events;

public class BaseGameEventListener<TArg, TEvent> : MonoBehaviour, IGameEventListener<TArg>
    where TEvent : BaseGameEvent<TArg>
{
    public TEvent gameEvent;
    public UnityEvent<TArg> unityEvent;

    public void OnEventRaised(TArg argument)
    {
        unityEvent.Invoke(argument);
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