using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;
    public GridView gridView;

    [Header("Level Files")]
    public string[] levelFiles =
    {
        "level_01",
        "level_02",
        "level_03"
    };

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levelFiles.Length)
        {
            Debug.LogError("Invalid level index.");
            return;
        }

        string path = $"Levels/{levelFiles[index]}";
        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        if (jsonFile == null)
        {
            Debug.LogError($"Level JSON not found: {path}");
            return;
        }

        JsonLevelData jsonLevel = JsonUtility.FromJson<JsonLevelData>(jsonFile.text);

        LevelData levelData = ConvertJsonToLevelData(jsonLevel);

        gridManager.LoadBoard(levelData);
        gridView.Rebuild(gridManager.GetBoard(), levelData.gridSize);
    }

    private LevelData ConvertJsonToLevelData(JsonLevelData json)
    {
        LevelData levelData = new LevelData
        {
            gridSize = json.gridSize,
            optimalSteps = json.optimalSteps
        };

        foreach (JsonCellData jsonCell in json.cells)
        {
            CubeFace face = ParseEnum<CubeFace>(jsonCell.face);
            Direction direction = ParseEnum<Direction>(jsonCell.direction);
            SegmentType segmentType = ParseEnum<SegmentType>(jsonCell.segmentType);

            GridCell cell = new GridCell(
                face,
                new Vector2Int(jsonCell.x, jsonCell.y)
            );

            CellData data = new CellData(
                jsonCell.arrowId,
                direction,
                segmentType,
                CellState.Active
            );

            levelData.cells[cell] = data;
        }

        return levelData;
    }

    private T ParseEnum<T>(string value) where T : struct
    {
        if (Enum.TryParse(value, true, out T result))
            return result;

        Debug.LogError($"Invalid enum value: {value}");
        return default;
    }

    public int LevelCount
    {
        get { return levelFiles.Length; }
    }

    public void RefreshView()
    {
        gridView.Rebuild(gridManager.GetBoard(), gridManager.GridSize);
    }
}