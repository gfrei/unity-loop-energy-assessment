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
    private AudioController audioController;
    private LevelData level;

    public void Init(LevelData level, GameConfig gameConfig, AudioController audioController)
    {
        bool isUnlocked = ProgressionController.Instance.IsLevelUnlocked(level);
        button.enabled = isUnlocked;
        isLockedText.text = isUnlocked ? open : locked;
        nameText.text = level.name;

        this.gameConfig = gameConfig;
        this.audioController = audioController;
        this.level = level;
    }

    public void OnCardClicked()
    {
        gameConfig.currentLevel = level;
        audioController.PlayButtonSfx();
        SceneManager.LoadScene("GameScene");
    }
}
