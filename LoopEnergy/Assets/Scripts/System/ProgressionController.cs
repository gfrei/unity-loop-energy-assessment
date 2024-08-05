using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ProgressionController
{
    public static ProgressionController Instance 
    { 
        get
        {
            if (instance == null)
            {
                instance = new ProgressionController();
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private static ProgressionController instance;

    private ProgressionController() { }
    private const string completeLevelsKey = "completeLevelsKey";

    public bool IsLevelUnlocked(LevelData level)
    {
        return PlayerPrefs.GetInt(completeLevelsKey, 0) >= level.unlockCost;
    }

    public bool IsLevelComplete(LevelData level)
    {
        return PlayerPrefs.GetInt(level.id, 0) == 1;
    }

    public void CompleteLevel(LevelData level)
    {
        bool isLevelAlreadyComplete = IsLevelComplete(level);

        PlayerPrefs.SetInt(level.id, 1);

        if (!isLevelAlreadyComplete)
        {
            int completeLevels = PlayerPrefs.GetInt(completeLevelsKey, 0);
            PlayerPrefs.SetInt(completeLevelsKey, completeLevels + 1);
        }
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
