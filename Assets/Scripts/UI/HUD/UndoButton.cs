using UnityEngine;
using UnityEngine.EventSystems;

public class UndoButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private UndoController controller;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        controller.OnUndoButtonClicked();
    }
}