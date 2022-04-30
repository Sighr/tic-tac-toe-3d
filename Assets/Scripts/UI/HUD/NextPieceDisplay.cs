using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NextPieceDisplay : MonoBehaviour
{
    public PieceVariable nextPiece;
    
    [SerializeField]
    private Text text;

    private void OnEnable()
    {
        OnTurnPerformed();
    }

    public void OnTurnPerformed()
    {
        text.text = $"Next: {nextPiece.value.name}";
    }
}