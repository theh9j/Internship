using UnityEngine;

public class FaceGridGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridSize = 5;
    public float tileThickness = 0.02f;
    public float tileGap = 0.05f;
    public Transform cubeTrans;

    [Header("Prefab")]
    public GameObject tilePrefab;

    [Header("Debug")]
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
        ClearGrid();

        float cellSize = (cubeTrans.localScale.x / gridSize);

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

                float tileSize = cellSize - tileGap;
                tile.transform.localScale = new Vector3(tileSize, tileThickness, tileSize);
            }
        }
    }

    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}