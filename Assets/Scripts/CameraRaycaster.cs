using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public RaycastHitsGameEvent raycastEvent;

    private void Start()
    {
        if (_camera == null)
        {
            Debug.LogWarning("Camera is not set, fallback to component");
            _camera = GetComponent<Camera>();
        }
    }

    private void Update()
    {
        Vector3 rayTarget = _camera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1000));
        var position = transform.position;

        RaycastHit[] hits = Physics.RaycastAll(
            position,
            rayTarget,
            Mathf.Infinity,
            1 << LayerMask.NameToLayer("Cells"));
        
        raycastEvent.RaiseEvent(hits);
    }
}