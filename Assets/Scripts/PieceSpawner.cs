using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PiecesListVariable pieces;
    public PieceVariable nextPiece;
    public IntVariable currentPieceId;
    public CommonGameEvent preTurnPerformed;
    public CommonGameEvent turnPerformed;

    public void Start()
    {
        nextPiece.value = pieces.value[currentPieceId.value];
    }

    public void OnSelected(Cell cell)
    {
        preTurnPerformed.RaiseEvent();
        cell.Fill(nextPiece.value);
        if (!cell.isFilled)
        {
            return;
        }
        currentPieceId.value = (currentPieceId.value + 1) % pieces.value.Count;
        nextPiece.value = pieces.value[currentPieceId.value];
        turnPerformed.RaiseEvent();
    }
}