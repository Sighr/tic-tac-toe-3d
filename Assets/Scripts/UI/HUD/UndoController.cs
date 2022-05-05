using System.Collections.Generic;
using UnityEngine;

public class UndoController : MonoBehaviour
{
    public GridCellsVariable cells;
    public IntVariable nextPieceId;
    public PieceVariable nextPiece;
    public int undoHistoryLength;
    public CommonGameEvent turnPerformed;
    
    [SerializeField]
    private GameObject button;
    
    private class GameFrame
    {
        public Piece[,,] pieces;
        public int nextPieceId;
        public Piece nextPiece;
    }
    
    private readonly LinkedList<GameFrame> frames = new LinkedList<GameFrame>();
    
    public void OnGameEnded()
    {
        frames.Clear();
        button.SetActive(false);
    }

    public void OnPreTurnPerformed()
    {
        PushFrame();
        button.SetActive(true);
    }
    
    public void OnUndoButtonClicked()
    {
        PopFrame();
        if (frames.Count == 0)
        {
            button.SetActive(false);
        }
    }

    private void PushFrame()
    {
        var frame = new GameFrame();
        frame.pieces = new Piece[cells.dimension.value, cells.dimension.value, cells.dimension.value];
        for (int i = 0; i < cells.dimension.value; i++)
        {
            for (int j = 0; j < cells.dimension.value; j++)
            {
                for (int k = 0; k < cells.dimension.value; k++)
                {
                    frame.pieces[i, j, k] = cells.value[i, j, k].pieceFilled;
                }
            }
        }
        frame.nextPieceId = nextPieceId.value;
        frame.nextPiece = nextPiece.value;
        frames.AddLast(frame);
        if (frames.Count > undoHistoryLength)
        {
            frames.RemoveFirst();
        }
    }
    
    private void PopFrame()
    {
        var frame = frames.Last.Value;
        frames.RemoveLast();
        nextPieceId.value = frame.nextPieceId;
        nextPiece.value = frame.nextPiece;
        for (int i = 0; i < cells.dimension.value; i++)
        {
            for (int j = 0; j < cells.dimension.value; j++)
            {
                for (int k = 0; k < cells.dimension.value; k++)
                {
                    if (cells.value[i, j, k].pieceFilled != frame.pieces[i, j, k])
                    {
                        cells.value[i, j, k].Empty();
                        if (frame.pieces[i, j, k] != null)
                        {
                            cells.value[i, j, k].Fill(frame.pieces[i, j, k]);
                        }
                    }
                }
            }
        }
        turnPerformed.RaiseEvent();
    }
}