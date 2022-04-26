using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public List<Piece> pieces;
    private int currentId = 0;

    public void OnSelected(Cell cell)
    {
        cell.Fill(GetCurrentPiece());
    }
    
    private Piece GetCurrentPiece()
    {
        Piece current = pieces[currentId];
        currentId = (currentId + 1) % pieces.Count;
        return current;
    }
}