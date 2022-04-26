using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform rotationOrigin;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (rotationOrigin == null)
        {
            Debug.LogWarning("Camera script does not have origin set! Fallback to parent");
            rotationOrigin = transform.parent;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessRotation();
        ProcessZoom();
    }

    private void ProcessZoom()
    {
        float dzoom = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * dzoom;
    }

    private void ProcessRotation()
    {
        // TODO: remove magic constant into enum
        if (!Input.GetMouseButton(1))
        {
            return;
        }

        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");

        rotationOrigin.Rotate(transform.up, dx, Space.World);
        rotationOrigin.Rotate(-transform.right, dy, Space.World);
    }
}
