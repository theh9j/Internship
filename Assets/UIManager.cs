using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text moveText;
    public GameObject winScreen;

    public void UpdateLevel(int level)
    {
        levelText.text = $"LEVEL {level}";
    }

    public void UpdateMoves(int moves)
    {
        moveText.text = $"MOVES: {moves}";
    }

    public void ShowWinPanel(bool show)
    {
        winScreen.SetActive(show);
    }
}