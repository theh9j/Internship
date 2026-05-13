using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class JsonLevelData
{
    public int gridSize;
    public int optimalSteps;
    public List<JsonCellData> cells;
}