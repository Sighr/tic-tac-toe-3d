using UnityEngine;

public class Cell : MonoBehaviour
{
    public ColorVariable selectColor;
    public ColorVariable highlightColor;
    
    private Material _material;
    private Color _baseColor;
    
    public Piece pieceFilled {get; private set;}
    private GameObject _instantiated;

    private bool isSelected;
    
    public bool isFilled => pieceFilled != null;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _baseColor = _material.color;
    }

    public void Deselect()
    {
        isSelected = false;
        _material.color = _baseColor;
    }

    public void Select()
    {
        if (isFilled)
        {
            return;
        }
        isSelected = true;
        _material.color = selectColor.value;
    }

    public void Highlight()
    {
        if (isSelected || isFilled)
        {
            return;
        }
        _material.color = highlightColor.value;
    }
    
    public void Fill(Piece piece)
    {
        if (isFilled)
        {
            return;
        }
        pieceFilled = piece;
        _instantiated = Instantiate(piece.prefab, transform);
    }
    
    public void Empty()
    {
        if (!isFilled)
        {
            return;
        }
        pieceFilled = null;
        Destroy(_instantiated);
    }
}