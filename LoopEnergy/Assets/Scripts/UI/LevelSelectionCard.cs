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
    [SerializeField] private TMP_Text isLockedText;
    [SerializeField] private string locked;
    [SerializeField] private string open;

    private GameConfig gameConfig;
    private int levelIndex;

    public void Init(string levelName, bool isLocked, int levelIndex, GameConfig gameConfig)
    {
        nameText.text = levelName;
        button.enabled = !isLocked;
        isLockedText.text = isLocked ? locked : open;

        this.gameConfig = gameConfig;
        this.levelIndex = levelIndex;
    }

    public void OnCardClicked()
    {
        gameConfig.currentLevel = gameConfig.Levels[levelIndex];
        SceneManager.LoadScene("GameScene");
    }
}
