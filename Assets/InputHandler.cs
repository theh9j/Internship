using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameManager gameManager;
    public LayerMask clickableLayer;

    private void Update()
    {
        if (Touchscreen.current != null)
        {
            TouchControl touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasReleasedThisFrame)
            {
                HandleClick(touch.position.ReadValue());
            }
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleClick(Mouse.current.position.ReadValue());
        }
    }

    private void HandleClick(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickableLayer))
        {
            CellView cellView = hit.collider.GetComponent<CellView>();

            if (cellView == null)
                cellView = hit.collider.GetComponentInParent<CellView>();

            if (cellView == null)
                return;

            gameManager.OnCellClicked(cellView.cell);
        }
    }
}