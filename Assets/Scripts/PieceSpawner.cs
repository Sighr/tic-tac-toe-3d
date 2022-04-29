using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PiecesListVariable pieces;
    public CommonGameEvent turnPerformed;
    private int _currentId;

    public void OnSelected(Cell cell)
    {
        cell.Fill(pieces.value[_currentId]);
        if (!cell.isFilled)
        {
            return;
        }
        _currentId = (_currentId + 1) % pieces.value.Count;
        turnPerformed.RaiseEvent();
    }
}