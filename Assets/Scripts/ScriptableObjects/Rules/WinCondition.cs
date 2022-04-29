using System.Collections.Generic;
using UnityEngine;

public abstract class WinCondition : ScriptableObject
{
    public abstract string ShortDescription {get;}
    public abstract string Description {get;}

    public abstract bool HasGameEnded {get;}
    public virtual Piece GetWinner()
    {
        var scores = GetScores();
        KeyValuePair<Piece, int> max = new KeyValuePair<Piece, int>();
        foreach (var score in scores)
        {
            if (score.Value >= max.Value)
            {
                max = score;
            }
        }
        return max.Key;
    }
    public abstract Dictionary<Piece, int> GetScores();
}