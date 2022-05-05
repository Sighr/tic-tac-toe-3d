using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideArrowsController : MonoBehaviour, IPointerClickHandler
{
    public Camera hideArrowCamera;
    public GridCellsVariable cells;
    
    [SerializeField]
    private List<ArrowData> arrows;
    private Dictionary<ArrowData.Direction, Dictionary<ArrowData.Mode, ArrowData>> _arrowsDict;
    
    [SerializeField]
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    
    public class HiddenIndices {
        public int up ;
        public int down;
        public int left;
        public int right;
        public int forward;
        public int back;
        
        public void Reset()
        {
            up = 0;
            down = 0;
            right = 0;
            left = 0;
            forward = 0;
            back = 0;
        }
    }
    
    private readonly HiddenIndices _indices = new HiddenIndices();
    private RaycastHit _outHit;

    private void Awake()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _arrowsDict = new Dictionary<ArrowData.Direction, Dictionary<ArrowData.Mode, ArrowData>>();
        foreach (ArrowData arrow in arrows)
        {
            if (!_arrowsDict.ContainsKey(arrow.direction))
            {
                _arrowsDict[arrow.direction] = new Dictionary<ArrowData.Mode, ArrowData>();
            }
            _arrowsDict[arrow.direction][arrow.mode] = arrow;
        }
    }
    
    public void OnTurnPerformed()
    {
        _indices.Reset();
        AdjustCellVisibility();
        AdjustArrowVisibility();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        var anchoredPosition = rectTransform.anchoredPosition;
        var rect = rectTransform.rect;
        var point = new Vector3(
            (eventData.position.x - anchoredPosition.x) / rect.width,
            (eventData.position.y - parentRectTransform.rect.height + rect.height) / rect.height, 
            0);
        Ray ray = hideArrowCamera.ViewportPointToRay(point);
        
        if(!Physics.Raycast(ray, out _outHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("HideArrows")))
        {
            return;
        }
        
        var arrow = _outHit.collider.gameObject;
        var arrowData = arrow.GetComponent<ArrowData>();
        if (arrowData == null)
        {
            return;
        }
        arrowData.ModifyHiddenIndices(_indices);
        AdjustCellVisibility();
        AdjustArrowVisibility();
    }

    private void AdjustArrowVisibility()
    {
        foreach (var arrowsDirectionsDict in _arrowsDict)
        {
            foreach (var arrowData in arrowsDirectionsDict.Value)
            {
                arrowData.Value.gameObject.SetActive(true);
            }
        }

        bool left = _indices.left == 0;
        bool right = _indices.right == 0;
        bool up = _indices.up == 0;
        bool down = _indices.down == 0;
        bool forward = _indices.forward == 0;
        bool back = _indices.back == 0;
        if (left)
        {
            _arrowsDict[ArrowData.Direction.Left][ArrowData.Mode.Out].gameObject.SetActive(false);
        }
        if (right)
        {
            _arrowsDict[ArrowData.Direction.Right][ArrowData.Mode.Out].gameObject.SetActive(false);
        }
        if (up)
        {
            _arrowsDict[ArrowData.Direction.Up][ArrowData.Mode.Out].gameObject.SetActive(false);
        }
        if (down)
        {
            _arrowsDict[ArrowData.Direction.Down][ArrowData.Mode.Out].gameObject.SetActive(false);
        }
        if (forward)
        {
            _arrowsDict[ArrowData.Direction.Forward][ArrowData.Mode.Out].gameObject.SetActive(false);
        }
        if (back)
        {
            _arrowsDict[ArrowData.Direction.Back][ArrowData.Mode.Out].gameObject.SetActive(false);
        }
        if (left
            && right
            && up
            && down
            && forward
            && back
           )
        {
            _arrowsDict[ArrowData.Direction.All][ArrowData.Mode.Reset].gameObject.SetActive(false);
        }

        var lastIndex = cells.dimension.value - 1;
        if (_indices.left - _indices.right == lastIndex)
        {
            _arrowsDict[ArrowData.Direction.Right][ArrowData.Mode.In].gameObject.SetActive(false);
            _arrowsDict[ArrowData.Direction.Left][ArrowData.Mode.In].gameObject.SetActive(false);
        }
        if (_indices.down - _indices.up == lastIndex)
        {
            _arrowsDict[ArrowData.Direction.Up][ArrowData.Mode.In].gameObject.SetActive(false);
            _arrowsDict[ArrowData.Direction.Down][ArrowData.Mode.In].gameObject.SetActive(false);
        }
        if (_indices.back - _indices.forward == lastIndex)
        {
            _arrowsDict[ArrowData.Direction.Forward][ArrowData.Mode.In].gameObject.SetActive(false);
            _arrowsDict[ArrowData.Direction.Back][ArrowData.Mode.In].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _indices.Reset();
        AdjustCellVisibility();
        AdjustArrowVisibility();
    }

    private void AdjustCellVisibility()
    {
        for (int i = 0; i < cells.dimension.value; i++)
        {
            for (int j = 0; j < cells.dimension.value; j++)
            {
                for (int k = 0; k < cells.dimension.value; k++)
                {
                    cells.value[i, j, k].gameObject.SetActive(false);
                }
            }
        }
        
        for (int i = _indices.left; i < cells.dimension.value + _indices.right; i++)
        {
            for (int j = _indices.down; j < cells.dimension.value + _indices.up; j++)
            {
                for (int k = _indices.back; k < cells.dimension.value + _indices.forward; k++)
                {
                    cells.value[i, j, k].gameObject.SetActive(true);
                }
            }
        }
    }
}