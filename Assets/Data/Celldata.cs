using UnityEngine;

[System.Serializable]
public class CellData
{
    public int arrowId;
    public Direction direction;
    public SegmentType segmentType;
    public CellState state;

    public CellData(int arrowId, Direction direction, SegmentType segmentType, CellState state)
    {
        this.arrowId = arrowId;
        this.direction = direction;
        this.segmentType = segmentType;
        this.state = state;
    }

    public bool HasArrow()
    {
        return arrowId >= 0 && state == CellState.Active;
    }

    public bool IsHead()
    {
        return segmentType == SegmentType.Head;
    }
}