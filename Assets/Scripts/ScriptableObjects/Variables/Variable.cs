using UnityEngine;

public abstract class Variable<T> : ScriptableObject
{
    public T defaultValue;
    public T value;
    
    protected void OnEnable()
    {
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
        value = defaultValue;
    }
}