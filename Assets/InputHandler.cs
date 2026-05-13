using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public GameManager gameManager;

    [Header("Settings")]
    public LayerMask clickableLayer;

    private Mouse mouse;

    private void Awake()
    {
        mouse = Mouse.current;
    }

    private void Update()
    {
        if (mouse == null)
            return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            HandleClick(mouse.position.ReadValue());
        }
    }

    private void HandleClick(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickableLayer))
        {
            CellView cellView = hit.collider.GetComponent<CellView>();

            if (cellView == null)
            {
                cellView = hit.collider.GetComponentInParent<CellView>();
            }

            if (cellView == null)
            {
                Debug.Log("Clicked object has no CellView.");
                return;
            }

            gameManager.OnCellClicked(cellView.cell);
        }
    }
}