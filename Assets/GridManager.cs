using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int GridSize { get; private set; }

    private Dictionary<GridCell, CellData> board =
        new Dictionary<GridCell, CellData>();

    public void LoadBoard(LevelData levelData)
    {
        GridSize = levelData.gridSize;
        board.Clear();

        foreach (var pair in levelData.cells)
        {
            board[pair.Key] = pair.Value;
        }

        Debug.Log($"Loaded board: {GridSize}x{GridSize}, cells: {board.Count}");
    }

    public bool TryRemoveArrow(GridCell clickedCell)
    {
        if (!board.TryGetValue(clickedCell, out CellData clickedData))
        {
            Debug.Log("Clicked empty cell.");
            return false;
        }

        if (!clickedData.HasArrow())
        {
            Debug.Log("Clicked removed/empty cell.");
            return false;
        }

        int arrowId = clickedData.arrowId;

        if (!TryFindHeadCell(arrowId, out GridCell headCell, out CellData headData))
        {
            Debug.LogWarning($"Arrow {arrowId} has no head.");
            return false;
        }

        if (!IsPathClear(headCell, headData.direction, arrowId))
        {
            Debug.Log($"Arrow {arrowId} is blocked.");
            return false;
        }

        RemoveArrowById(arrowId);

        Debug.Log($"Removed arrow {arrowId}");
        return true;
    }

    private bool TryFindHeadCell(int arrowId, out GridCell headCell, out CellData headData)
    {
        foreach (var pair in board)
        {
            if (pair.Value.arrowId == arrowId &&
                pair.Value.state == CellState.Active &&
                pair.Value.segmentType == SegmentType.Head)
            {
                headCell = pair.Key;
                headData = pair.Value;
                return true;
            }
        }

        headCell = default;
        headData = null;
        return false;
    }

    private bool IsPathClear(GridCell headCell, Direction direction, int movingArrowId)
    {
        Vector2Int dir = GetDirectionVector(direction);
        Vector2Int checkPos = headCell.position + dir;

        while (IsInsideGrid(checkPos))
        {
            GridCell checkCell = new GridCell(headCell.face, checkPos);

            if (board.TryGetValue(checkCell, out CellData data))
            {
                if (data.HasArrow() && data.arrowId != movingArrowId)
                {
                    return false;
                }
            }

            checkPos += dir;
        }

        return true;
    }

    private void RemoveArrowById(int arrowId)
    {
        List<GridCell> cellsToRemove = new List<GridCell>();

        foreach (var pair in board)
        {
            if (pair.Value.arrowId == arrowId)
            {
                cellsToRemove.Add(pair.Key);
            }
        }

        foreach (GridCell cell in cellsToRemove)
        {
            board[cell].state = CellState.Removed;
            board[cell].direction = Direction.None;
            board[cell].arrowId = -1;
        }
    }

    public bool IsWin()
    {
        foreach (var pair in board)
        {
            if (pair.Value.HasArrow())
                return false;
        }

        return true;
    }

    private bool IsInsideGrid(Vector2Int position)
    {
        return position.x >= 0 &&
               position.x < GridSize &&
               position.y >= 0 &&
               position.y < GridSize;
    }

    private Vector2Int GetDirectionVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2Int.up;
            case Direction.Down:
                return Vector2Int.down;
            case Direction.Left:
                return Vector2Int.left;
            case Direction.Right:
                return Vector2Int.right;
            default:
                return Vector2Int.zero;
        }
    }

    public bool TryGetCellData(GridCell cell, out CellData data)
    {
        return board.TryGetValue(cell, out data);
    }

    public Dictionary<GridCell, CellData> GetBoard()
    {
        return board;
    }
}