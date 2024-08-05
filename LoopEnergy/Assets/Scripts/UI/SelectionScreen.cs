using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private LevelSelectionCard selectCardPrefab;
    [SerializeField] private RectTransform cardsListTransform;

    private void Start()
    {
        InstatiateCards();
    }

    private void InstatiateCards()
    {
        foreach(var level in gameConfig.LevelConfigs)
        {
            LevelSelectionCard cardInstance = Instantiate(selectCardPrefab, cardsListTransform);
            cardInstance.Init(level, gameConfig);
        }
    }

    public void ResetSave()
    {
        ProgressionController.Instance.ResetSave();
    }
}
