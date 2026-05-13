using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public GridManager gridManager;
    public LevelManager levelManager;

    [Header("State")]
    public int currentLevelIndex = 0;
    public int moveCount = 0;
    public bool isLevelComplete = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        moveCount = 0;
        isLevelComplete = false;

        levelManager.LoadLevel(currentLevelIndex);

        Debug.Log($"Loaded level {currentLevelIndex + 1}");
    }

    public void OnCellClicked(GridCell cell)
    {
        if (isLevelComplete)
            return;

        bool removed = gridManager.TryRemoveArrow(cell);

        if (!removed)
            return;

        moveCount++;

        Debug.Log($"Move Count: {moveCount}");

        if (gridManager.IsWin())
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        isLevelComplete = true;

        Debug.Log("Level Complete!");
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void NextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex >= levelManager.LevelCount)
        {
            Debug.Log("No more levels. Restarting from level 1.");
            nextLevelIndex = 0;
        }

        LoadLevel(nextLevelIndex);
    }
}