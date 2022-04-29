using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/WinConditions/ScoreLength")]
public class ScoreLengthWinCondition : WinCondition
{
    public GridCellsVariable cells;
    public PiecesListVariable pieces;
    public List<ScoreTemplateEntry> scoreTemplates;
    
    [System.Serializable]
    public struct ScoreTemplateEntry
    {
        public int length;
        public int score;
    }
    private Dictionary<int, int> _scoreTemplatesDict;

    private static readonly Vector3[] CardinalDirections = {
        Vector3.up,
        Vector3.up + Vector3.forward,
        Vector3.up + Vector3.back,
        Vector3.up + Vector3.right,
        Vector3.up + Vector3.left,
        Vector3.up + Vector3.forward + Vector3.right,
        Vector3.up + Vector3.forward + Vector3.left,
        Vector3.up + Vector3.back + Vector3.right,
        Vector3.up + Vector3.back + Vector3.left,
        Vector3.forward,
        Vector3.forward + Vector3.right,
        Vector3.forward + Vector3.left,
        Vector3.right,
    };

    private void OnEnable()
    {
        _scoreTemplatesDict = new Dictionary<int, int>();
        foreach (ScoreTemplateEntry entry in scoreTemplates)
        {
            _scoreTemplatesDict[entry.length] = entry.score;
        }
    }

    public override string ShortDescription => "Gain score points by creating long lines";
    public override string Description => ShortDescription;

    public override bool HasGameEnded => cells.value.Cast<Cell>().All(cell => cell != null);


    public override Dictionary<Piece, int> GetScores()
    {
        var result = new Dictionary<Piece, int>();
        foreach (var piece in pieces.value)
        {
            result[piece] = 0;
        }
        for (var i = 0; i < cells.dimension.value; i++)
        {
            for (var j = 0; j < cells.dimension.value; j++)
            {
                for (var k = 0; k < cells.dimension.value; k++)
                {
                    var cell = cells.value[i, j, k];
                    if (!cell.isFilled)
                    {
                        continue;
                    }

                    // checking half of possible directions to avoid mirroring
                    int score = 0;
                    
                    foreach (Vector3 direction in CardinalDirections)
                    {
                        int length = CheckDirectionSequenceFromStart(i, j, k, direction);
                        score += ApplyScoreTemplate(length);
                    }
                    result[cell.pieceFilled] += score;
                }
            }
        }
        return result;
    }
    
    [ContextMenu("DebugScores")]
    public void DebugScores()
    {
        var result = new Dictionary<Piece, int>();
        foreach (var piece in pieces.value)
        {
            result[piece] = 0;
        }
        for (var i = 0; i < cells.dimension.value; i++)
        {
            for (var j = 0; j < cells.dimension.value; j++)
            {
                for (var k = 0; k < cells.dimension.value; k++)
                {
                    var cell = cells.value[i, j, k];
                    Debug.Log($"Cell {i} {j} {k} {(cell.pieceFilled == null ? "Empty" : cell.pieceFilled.name)}");
                    if (!cell.isFilled)
                    {
                        continue;
                    }

                    // checking half of possible directions to avoid mirroring
                    int score = 0;
                    
                    foreach (Vector3 direction in CardinalDirections)
                    {
                        int length = CheckDirectionSequenceFromStart(i, j, k, direction);
                        Debug.Log($"{direction} {length}");
                        score += ApplyScoreTemplate(length);
                    }
                    result[cell.pieceFilled] += score;
                }
            }
        }
    }

    private int ApplyScoreTemplate(int length)
    {
        return _scoreTemplatesDict.ContainsKey(length) ? _scoreTemplatesDict[length] : 0;
    }

    private int CheckDirectionSequenceFromStart(int startX, int startY, int startZ, Vector3 direction)
    {
        int dirX = (int) direction.x;
        int dirY = (int) direction.y;
        int dirZ = (int) direction.z;

        Piece startPiece = cells.value[startX, startY, startZ].pieceFilled;
        int prevX = startX - dirX;
        int prevY = startY - dirY;
        int prevZ = startZ - dirZ;

        // check if it's sequence start - there must be no piece of the same type in the opposite direction
        if (!IsIndexOutOfBounds(prevX, prevY, prevZ) && cells.value[prevX, prevY, prevZ].pieceFilled == startPiece)
        {
            return 0;
        }
        
        // starting piece is always same type with itself. so if there's a chain it's at least 1 element long
        int length = 1;
        for (int i = 1; i < cells.dimension.value; i++)
        {
            int x = startX + dirX * i;
            int y = startY + dirY * i;
            int z = startZ + dirZ * i;
            // stop counting if we've reached border or hit different piece
            if (IsIndexOutOfBounds(x, y, z) || cells.value[x, y, z].pieceFilled != startPiece)
            {
                break;
            }
            length += 1;
        }
        return length;
    }

    private bool IsIndexOutOfBounds(int x, int y, int z)
    {
        return x < 0
               || y < 0
               || z < 0
               || x >= cells.dimension.value
               || y >= cells.dimension.value
               || z >= cells.dimension.value;
    }
}