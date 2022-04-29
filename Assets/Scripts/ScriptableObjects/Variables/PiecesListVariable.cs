using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Variables/PiecesList")]
public class PiecesListVariable : ScriptableObject
{
    public List<Piece> value;
    
    private void OnEnable()
    {
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
    }
}