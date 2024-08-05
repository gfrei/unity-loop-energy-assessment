using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveController
{
    public static void SetLevelComplete(string key)
    {
        PlayerPrefs.SetInt(key, 1);
    }
    public static bool IsLevelComplete(string key)
    {
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public static void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
