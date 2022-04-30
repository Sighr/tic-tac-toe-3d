using System;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PiecesListVariable pieces;
    public PieceVariable nextPiece;
    public CommonGameEvent turnPerformed;
    private int _currentId;

    public void Start()
    {
        nextPiece.value = pieces.value[_currentId];
    }

    public void OnSelected(Cell cell)
    {
        cell.Fill(nextPiece.value);
        if (!cell.isFilled)
        {
            return;
        }
        _currentId = (_currentId + 1) % pieces.value.Count;
        nextPiece.value = pieces.value[_currentId];
        turnPerformed.RaiseEvent();
    }
}