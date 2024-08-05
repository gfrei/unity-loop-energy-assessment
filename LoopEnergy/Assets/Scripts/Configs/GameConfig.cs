using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    public List<LevelData> LevelConfigs;

    [HideInInspector] public LevelData currentLevel;
}

[System.Serializable]
public struct LevelData
{
    public LevelPrefab prefab;
    public string name;
    public string id => prefab.name;
}