using UnityEngine;

public class FaceGridGenerator : MonoBehaviour
{
    [Header("References")]
    public Transform cubeTransform;
    public GameObject tilePrefab;
    public CubeFace face;

    [Header("Grid Settings")]
    public int gridSize = 5;
    public float tileThickness = 0.02f;
    public float tileGap = 0.04f;
    public bool generateOnStart = true;

    private void Start()
    {
        if (generateOnStart)
        {
            GenerateGrid();
        }
    }

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        if (cubeTransform == null || tilePrefab == null)
        {
            Debug.LogError($"{name}: Missing cubeTransform or tilePrefab.");
            return;
        }

        ClearGrid();

        float cubeSize = cubeTransform.localScale.x;
        float cellSize = cubeSize / gridSize;
        float offset = (gridSize - 1) / 2f;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.name = $"Tile_{x}_{y}";

                float localX = (x - offset) * cellSize;
                float localZ = (y - offset) * cellSize;

                tile.transform.localPosition = new Vector3(localX, 0f, localZ);
                tile.transform.localRotation = Quaternion.identity;

                float tileSize = Mathf.Max(0.01f, cellSize - tileGap);
                tile.transform.localScale = new Vector3(
                    tileSize,
                    tileThickness,
                    tileSize
                );

                CellView cellView = tile.AddComponent<CellView>();
                cellView.cell = new GridCell(face, new Vector2Int(x, y));
            }
        }
    }

    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public void SetGridSize(int newGridSize)
    {
        gridSize = newGridSize;
        GenerateGrid();
    }
}