using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float rotationSpeed = 0.15f;
    public float zoomSpeed = 0.01f;

    public float minZoom = 4f;
    public float maxZoom = 12f;

    public float rotationSmoothness = 10f;
    public float zoomSmoothness = 10f;

    private Vector2 currentRotation;
    private Vector2 targetRotation;

    private float currentZoom;
    private float targetZoom;

    private float previousPinchDistance;

    private void Start()
    {
        currentRotation = transform.eulerAngles;
        targetRotation = currentRotation;

        currentZoom = Mathf.Abs(cameraTransform.localPosition.z);
        targetZoom = currentZoom;
    }

    private void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
        SmoothMovement();
    }

    private void HandleTouchInput()
    {
        if (Touchscreen.current == null)
            return;

        var touches = Touchscreen.current.touches;

        bool touch0Pressed = touches[0].press.isPressed;
        bool touch1Pressed = touches[1].press.isPressed;

        if (touch0Pressed && !touch1Pressed)
        {
            Vector2 delta = touches[0].delta.ReadValue();

            targetRotation.x -= delta.y * rotationSpeed;
            targetRotation.y += delta.x * rotationSpeed;
        }

        if (touch0Pressed && touch1Pressed)
        {
            Vector2 pos0 = touches[0].position.ReadValue();
            Vector2 pos1 = touches[1].position.ReadValue();

            float currentDistance = Vector2.Distance(pos0, pos1);

            if (previousPinchDistance > 0)
            {
                float difference = currentDistance - previousPinchDistance;

                targetZoom -= difference * zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            }

            previousPinchDistance = currentDistance;
        }
        else
        {
            previousPinchDistance = 0f;
        }
    }

    private void HandleMouseInput()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            targetRotation.x -= delta.y * rotationSpeed;
            targetRotation.y += delta.x * rotationSpeed;
        }

        float scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetZoom -= scroll * 0.05f;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
    }

    private void SmoothMovement()
    {
        targetRotation.x = Mathf.Clamp(targetRotation.x, -80f, 80f);

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

        cameraTransform.localPosition = new Vector3(0, 0, -currentZoom);
    }
}