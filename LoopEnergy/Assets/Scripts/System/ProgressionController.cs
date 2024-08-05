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

    public bool IsLevelUnlocked(LevelData level)
    {
        return true;
    }

    public bool IsLevelComplete(LevelData level)
    {
        return SaveController.IsLevelComplete(level.id);
    }

    public void CompleteLevel(LevelData level)
    {
        SaveController.SetLevelComplete(level.id);
    }
}
