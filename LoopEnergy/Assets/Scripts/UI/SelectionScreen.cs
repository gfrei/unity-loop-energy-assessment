using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private LevelSelectionCard selectCardPrefab;
    [SerializeField] private RectTransform cardsListTransform;
    [SerializeField] private AudioController audioControllerPrefab;

    private AudioController audioController;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        if (audioController == null)
        {
            audioController = Instantiate(audioControllerPrefab);
        }

        InstatiateCards();
    }

    private void InstatiateCards()
    {
        foreach(var level in gameConfig.LevelConfigs)
        {
            LevelSelectionCard cardInstance = Instantiate(selectCardPrefab, cardsListTransform);
            cardInstance.Init(level, gameConfig, audioController);
        }
    }

    public void ResetSave()
    {
        audioController.PlayButtonSfx();
        ProgressionController.Instance.ResetSave();
    }
}
