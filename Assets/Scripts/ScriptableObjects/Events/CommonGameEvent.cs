using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Common")]
public class CommonGameEvent : ScriptableObject
{
    private readonly List<CommonGameEventListener> _eventListeners = new List<CommonGameEventListener>();
    
    public void RaiseEvent()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            if (_eventListeners.Count <= i)
            {
                // for the case of deleting more than one listener in one iteration
                continue;
            }
            _eventListeners[i].OnEventRaised();
        }
    }
    
    public void RegisterListener(CommonGameEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }
    
    public void UnregisterListener(CommonGameEventListener listener)
    {
        _eventListeners.Remove(listener);
    }
}