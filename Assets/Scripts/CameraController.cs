using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float zoomSpeed;

    [SerializeField]
    private float zoomMin;

    [SerializeField]
    private float zoomMax;

    private GameInput gameInput;

    private Vector2 moveDirection;

    private float zoomValue;

    private Camera _camera;

    private void Awake()
    {
        gameInput = new();
        _camera = GetComponent<Camera>();

        gameInput.Camera.Move.performed += context =>
        {
            moveDirection = context.ReadValue<Vector2>();
        };
        gameInput.Camera.Move.canceled += context =>
        {
            moveDirection = context.ReadValue<Vector2>();
        };

        gameInput.Camera.Zoom.performed += context =>
        {
            zoomValue = context.ReadValue<Vector2>().y;
        };
        gameInput.Camera.Zoom.canceled += context =>
        {
            zoomValue = context.ReadValue<Vector2>().y;
        };
    }

    private void OnEnable()
    {
        gameInput.Enable();
    }

    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    private void OnDisable()
    {
        gameInput.Disable();
    }

    private void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }
    
    private void Zoom()
    {
        _camera.orthographicSize -= zoomValue * zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, zoomMin, zoomMax);
    }
}
