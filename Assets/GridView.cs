using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [Header("References")]
    public Transform arrowRoot;
    public FaceGridData[] faceGrids;

    [Header("Prefabs")]
    public GameObject bodyPrefab;
    public GameObject cornerPrefab;
    public GameObject headPrefab;

    private readonly List<GameObject> spawnedVisuals = new List<GameObject>();

    public void Rebuild(Dictionary<GridCell, CellData> board, int gridSize)
    {
        Clear();

        foreach (FaceGridData faceGrid in faceGrids)
        {
            faceGrid.gridSize = gridSize;
        }

        foreach (var pair in board)
        {
            GridCell cell = pair.Key;
            CellData data = pair.Value;

            if (!data.HasArrow())
                continue;

            SpawnCellVisual(cell, data);
        }
    }

    public void Clear()
    {
        foreach (GameObject visual in spawnedVisuals)
        {
            Destroy(visual);
        }

        spawnedVisuals.Clear();
    }

    private void SpawnCellVisual(GridCell cell, CellData data)
    {
        FaceGridData faceGrid = GetFaceGrid(cell.face);

        if (faceGrid == null)
        {
            Debug.LogError($"No FaceGridData found for {cell.face}");
            return;
        }

        GameObject prefab = GetPrefab(data.segmentType);

        Vector3 position = faceGrid.GetWorldPosition(cell.position);

        Quaternion faceRotation = faceGrid.GetWorldRotation();

        Quaternion quadFixRotation = Quaternion.Euler(90f, 0f, 0f);

        Quaternion directionRotation =
            Quaternion.Euler(0f, GetRotationAngle(data.direction), 0f);

        Quaternion finalRotation =
            faceRotation * directionRotation * quadFixRotation;

        GameObject visual = Instantiate(
            prefab,
            position,
            finalRotation,
            arrowRoot
        );

        float cubeSize = faceGrid.cubeTransform.localScale.x;
        float cellSize = cubeSize / faceGrid.gridSize;

        visual.transform.localScale = Vector3.one * (cellSize * 0.9f);

        visual.name = $"Arrow_{data.arrowId}_{data.segmentType}_{cell.face}_{cell.position.x}_{cell.position.y}";

        CellView cellView = visual.GetComponent<CellView>();
        if (cellView == null)
        {
            cellView = visual.AddComponent<CellView>();
        }
        cellView.cell = cell;

        BoxCollider boxCollider = visual.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = visual.AddComponent<BoxCollider>();
        }

        visual.layer = LayerMask.NameToLayer("Clickable");

        spawnedVisuals.Add(visual);
    }

    private FaceGridData GetFaceGrid(CubeFace face)
    {
        foreach (FaceGridData faceGrid in faceGrids)
        {
            if (faceGrid.face == face)
                return faceGrid;
        }

        return null;
    }

    private GameObject GetPrefab(SegmentType segmentType)
    {
        switch (segmentType)
        {
            case SegmentType.Body:
                return bodyPrefab;

            case SegmentType.Corner:
                return cornerPrefab;

            case SegmentType.Head:
                return headPrefab;

            default:
                return bodyPrefab;
        }
    }

    private float GetRotationAngle(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return 0f;

            case Direction.Right:
                return 90f;

            case Direction.Down:
                return 180f;

            case Direction.Left:
                return 270f;

            default:
                return 0f;
        }
    }
}