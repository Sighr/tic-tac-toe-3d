using UnityEngine;

[CreateAssetMenu]
public class GridCellsVariable : ScriptableObject
{
    public Cell[,,] value;
    public IntVariable dimension;
    
    void OnEnable()
    {
        value = new Cell[dimension.value, dimension.value, dimension.value];
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
    }
}