using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEvent<TArg> : ScriptableObject
{
    private readonly List<IGameEventListener<TArg>> _eventListeners = new List<IGameEventListener<TArg>>();
    
    public void RaiseEvent(TArg argument)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnEventRaised(argument);
        }
    }
    
    public void RegisterListener(IGameEventListener<TArg> listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }
    
    public void UnregisterListener(IGameEventListener<TArg> listener)
    {
        _eventListeners.Remove(listener);
    }
}