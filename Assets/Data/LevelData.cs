using UnityEngine;
using System.Collections.Generic;

public class LevelData
{
    public int gridSize;
    public int optimalSteps;

    public Dictionary<GridCell, CellData> cells = new Dictionary<GridCell, CellData>();
}