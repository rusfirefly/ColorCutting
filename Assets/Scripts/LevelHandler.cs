using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int CurrentSeason;
    public int ScoreAll;
    public List<LevelInformation> LevelInformation = new List<LevelInformation>();
}

[Serializable]
public class LevelInformation
{
    public int LevelNumber;
    public int CountStarCollected;
    public int Score;
    public bool IsActive;
    
    [NonSerialized] public LevelView LevelView;
}

public static class LevelHandler
{
    private static string _pathData = $"{Application.persistentDataPath}/GameData.json";

    public static void SaveData(LevelData levelData)
    {
        string json = JsonUtility.ToJson(levelData);
        File.WriteAllText(_pathData, json);
    }

    public static LevelData LoadData()
    {
        if (!File.Exists(_pathData)) return null;
        string json = File.ReadAllText(_pathData);
        return JsonUtility.FromJson<LevelData>(json);
    }
}
