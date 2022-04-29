using UnityEngine;

[CreateAssetMenu(menuName = "RuntimeVariables/GridCells")]
public class GridCellsVariable : ScriptableObject
{
    public Cell[,,] value;
    public IntVariable dimension;
    
    private void OnEnable()
    {
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
    }
}