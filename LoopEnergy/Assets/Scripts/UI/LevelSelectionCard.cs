using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectionCard : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text isBlockedText;
    [SerializeField] private string blocked;
    [SerializeField] private string open;

    private GameConfig gameConfig;
    private int levelIndex;

    public void Init(string levelName, bool isBlocked, int levelIndex, GameConfig gameConfig)
    {
        nameText.text = levelName;
        button.enabled = isBlocked;
        isBlockedText.text = isBlocked ? blocked : open;

        this.gameConfig = gameConfig;
        this.levelIndex = levelIndex;
    }

    public void OnCardClicked()
    {
        gameConfig.currentLevel = gameConfig.Levels[levelIndex];
        SceneManager.LoadScene("GameScene");
    }
}
