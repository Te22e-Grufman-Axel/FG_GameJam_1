using UnityEngine;
using UnityEngine.InputSystem;

public class CamraMovment : MonoBehaviour
{
    [Header("Speeds")]
    public float keyboardSpeed = 500f;
    public float edgeScrollSpeed = 500f;
    [Header("Edge scrolling")]
    public float edgeSize = 20f; 
    [Header("Smoothing")]
    public float smoothFactor = 0.15f;

    [Header("Optional bounds (XY plane)")]
    public bool useBounds = false;
    public Vector2 minXY = new Vector2(-50f, -50f);
    public Vector2 maxXY = new Vector2(50f, 50f);

    [Header("Zoom")]
    public float zoomSpeed = 200f;   
    public float minZoom = 10f;      
    public float maxZoom = 100f;  

    PlayerInput playerInput;
    InputAction moveAction;

    Camera cam;
    float targetOrthoSize;
    float targetHeight;
    float fixedZ;

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
        }

        cam = GetComponent<Camera>();
        if (cam == null) cam = GetComponentInChildren<Camera>();

        fixedZ = transform.position.z;

        if (cam != null && cam.orthographic)
            targetOrthoSize = cam.orthographicSize;
        else
            targetHeight = transform.position.y;
    }

    void Update()
    {
        Vector2 kb = Vector2.zero;
        if (moveAction != null) kb = moveAction.ReadValue<Vector2>();

        Vector2 edge = GetEdgeInput();

        Vector3 right = Vector3.right;
        Vector3 up = Vector3.up;

        Vector3 targetDelta =
            right * (kb.x * keyboardSpeed + edge.x * edgeScrollSpeed) +
            up    * (kb.y * keyboardSpeed + edge.y * edgeScrollSpeed);

        Vector3 targetPos = transform.position + targetDelta * Time.deltaTime;
        targetPos.z = fixedZ;

        if (useBounds)
        {
            targetPos.x = Mathf.Clamp(targetPos.x, minXY.x, maxXY.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minXY.y, maxXY.y);
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, 1f - Mathf.Exp(-smoothFactor * Time.deltaTime * 60f));

        HandleZoom();
    }

    void HandleZoom()
    {
        if (cam == null) return;

        float scroll = 0f;
        if (Mouse.current != null)
            scroll = Mouse.current.scroll.ReadValue().y;


        float zoomInput = scroll;
        if (Mathf.Approximately(zoomInput, 0f)) 
        {
            if (cam.orthographic)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, 1f - Mathf.Exp(-smoothFactor * Time.deltaTime * 60f));
            else
            {
                Vector3 p = transform.position;
                p.y = Mathf.Lerp(p.y, targetHeight, 1f - Mathf.Exp(-smoothFactor * Time.deltaTime * 60f));
                p.z = fixedZ;
                transform.position = p;
            }
            return;
        }

        float delta = zoomInput * zoomSpeed * Time.deltaTime;

        if (cam.orthographic)
        {
            targetOrthoSize = Mathf.Clamp(targetOrthoSize - delta, minZoom, maxZoom);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, 1f - Mathf.Exp(-smoothFactor * Time.deltaTime * 60f));
        }
        else
        {
            targetHeight = Mathf.Clamp(targetHeight - delta, minZoom, maxZoom);
            Vector3 p = transform.position;
            p.y = Mathf.Lerp(p.y, targetHeight, 1f - Mathf.Exp(-smoothFactor * Time.deltaTime * 60f));
            p.z = fixedZ;
            transform.position = p;
        }
    }

    Vector2 GetEdgeInput()
    {
        if (Mouse.current == null) return Vector2.zero;

        Vector2 mpos = Mouse.current.position.ReadValue();
        Vector2 edge = Vector2.zero;

        if (mpos.x <= edgeSize) edge.x = -1f;
        else if (mpos.x >= Screen.width - edgeSize) edge.x = 1f;

        if (mpos.y <= edgeSize) edge.y = -1f; 
        else if (mpos.y >= Screen.height - edgeSize) edge.y = 1f;

        return edge;
    }
}
