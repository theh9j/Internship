using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Zoom")]
    public float zoomSpeed = 2f;
    public float minZoom = 4f;
    public float maxZoom = 12f;

    private float rotationSmoothness = 10f;
    private float zoomSmoothness = 10f;

    private float rotationSpeed = 0.2f;
    private float minVerticalAngle = -80f;
    private float maxVerticalAngle = 80f;

    private Mouse mouse;

    private Vector2 currentRotation;
    private Vector2 targetRotation;

    private float currentZoom;
    private float targetZoom;

    private void Awake()
    {
        mouse = Mouse.current;
    }

    private void Start()
    {
        currentRotation = transform.eulerAngles;
        targetRotation = currentRotation;

        currentZoom = Mathf.Abs(cameraTransform.localPosition.z);
        targetZoom = currentZoom;
    }

    private void Update()
    {
        if (mouse == null)
            return;

        HandleRotation();
        HandleZoom();

        SmoothMovement();
    }

    private void HandleRotation()
    {
        if (mouse.rightButton.isPressed)
        {
            Vector2 delta = mouse.delta.ReadValue();

            targetRotation.x -= delta.y * rotationSpeed;
            targetRotation.y += delta.x * rotationSpeed;

            targetRotation.x = Mathf.Clamp(
                targetRotation.x,
                minVerticalAngle,
                maxVerticalAngle
            );
        }
    }

    private void HandleZoom()
    {
        float scroll = mouse.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetZoom -= scroll * zoomSpeed;

            targetZoom = Mathf.Clamp(
                targetZoom,
                minZoom,
                maxZoom
            );
        }
    }

    private void SmoothMovement()
    {
        currentRotation = Vector2.Lerp(
            currentRotation,
            targetRotation,
            rotationSmoothness * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(
            currentRotation.x,
            currentRotation.y,
            0f
        );

        currentZoom = Mathf.Lerp(
            currentZoom,
            targetZoom,
            zoomSmoothness * Time.deltaTime
        );

        cameraTransform.localPosition =
            new Vector3(0f, 0f, -currentZoom);
    }
}