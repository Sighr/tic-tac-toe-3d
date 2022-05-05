using System.Collections.Generic;
using UnityEngine;

public class GridSelector : MonoBehaviour
{
    public CellSelectedGameEvent cellSelected;
    public GridCellsVariable cells;
    
    private List<Cell> _selected = new List<Cell>();
    private List<Cell> _highlighted = new List<Cell>();
    private bool isPlaying = true;
    
    public void OnGameStarted()
    {
        isPlaying = true;
    }
    
    public void OnGameEnded()
    {
        isPlaying = false;
        _highlighted.Clear();
        _selected.Clear();
        SetCellsColors();
    }

    public void OnRaycast(RaycastHit[] hits)
    {
        if (!isPlaying)
        {
            return;
        }
        _highlighted.Clear();
        // TODO: remove magic constant into enum
        if (Input.GetMouseButtonDown(0))
        {
            if (_selected.Count == 0)
            {
                SelectRow(hits);
            }
            else if (_selected.Count > 1)
            {
                SelectParticular(hits);
            }
            else
            {
                TryFill(hits);
            }
        }
        else
        {
            Highlight(hits);
        }
        SetCellsColors();
    }

    private void SetCellsColors()
    {
        foreach (var cell in cells.value)
        {
            cell.Deselect();
        }
        foreach (var cell in _highlighted)
        {
            cell.Highlight();
        }
        foreach (var cell in _selected)
        {
            cell.Select();
        }
    }

    private void TryFill(RaycastHit[] hits)
    {
        bool isHit = false;
        foreach (var hit in hits)
        {
            if (_selected[0].gameObject == hit.collider.gameObject)
            {
                isHit = true;
                break;
            }
        }
        if (!isHit)
        {
            _selected.Clear();
            SelectRow(hits);
            return;
        }
        cellSelected.RaiseEvent(_selected[0]);
        _selected.Clear();
    }

    private void Highlight(RaycastHit[] hits)
    {
        foreach (var hit in hits)
        {
            foreach (var cell in cells.value)
            {
                if (cell.gameObject != hit.collider.gameObject
                    || cell.isFilled)
                {
                    continue;
                }
                _highlighted.Add(cell);
            }
        }
    }

    private void SelectParticular(RaycastHit[] hits)
    {
        bool isHit = false;
        foreach (var hit in hits)
        {
            foreach (var cell in _selected)
            {
                if (cell.gameObject != hit.collider.gameObject
                    || cell.isFilled)
                {
                    continue;
                }
                isHit = true;
                
                _selected.Clear();
                _selected.Add(cell);
                
                break;
            }
            if (isHit)
            {
                break;
            }
        }
        if (!isHit)
        {
            _selected.Clear();
            SelectRow(hits);
        }
    }

    private void SelectRow(RaycastHit[] hits)
    {
        foreach (var hit in hits)
        {
            foreach (var cell in cells.value)
            {
                if (cell.gameObject != hit.collider.gameObject
                    || cell.isFilled)
                {
                    continue;
                }
                _selected.Add(cell);
            }
        }
    }
}