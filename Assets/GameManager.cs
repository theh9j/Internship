using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public GridManager gridManager;
    public LevelManager levelManager;

    [Header("State")]
    public int moveCount = 0;
    public int currentLevelIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadCurrentLevel();
    }

    public void LoadCurrentLevel()
    {
        moveCount = 0;
        levelManager.LoadLevel(currentLevelIndex);
    }

    public void OnArrowClicked(ArrowPiece arrow)
    {
        bool removed = gridManager.TryRemoveArrow(arrow);

        if (!removed)
            return;

        moveCount++;

        if (gridManager.IsWin())
        {
            Debug.Log("Level Complete!");
        }
    }

    public void RestartLevel()
    {
        LoadCurrentLevel();
    }

    public void NextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelManager.levels.Length)
        {
            currentLevelIndex = 0;
        }

        LoadCurrentLevel();
    }
}