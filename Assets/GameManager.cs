using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public GridManager gridManager;
    public LevelManager levelManager;
    public UIManager uiManager;

    [Header("State")]
    public int currentLevelIndex = 0;
    public int moveCount = 0;
    public bool isLevelComplete = false;

    private void Awake()
    {
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

        uiManager.ShowWinPanel(false);
        uiManager.UpdateLevel(currentLevelIndex + 1);
        uiManager.UpdateMoves(moveCount);

        levelManager.LoadLevel(currentLevelIndex);
    }

    public void OnCellClicked(GridCell cell)
    {
        if (isLevelComplete)
            return;

        bool removed = gridManager.TryRemoveArrow(cell);

        if (!removed)
            return;

        moveCount++;
        uiManager.UpdateMoves(moveCount);

        if (gridManager.IsWin())
        {
            StartCoroutine(CompleteLevelRoutine());
        }
    }

    private IEnumerator CompleteLevelRoutine()
    {
        isLevelComplete = true;

        uiManager.ShowWinPanel(true);

        yield return new WaitForSeconds(1.5f);

        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex >= levelManager.LevelCount)
        {
            nextLevelIndex = 0;
        }

        LoadLevel(nextLevelIndex);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }
}