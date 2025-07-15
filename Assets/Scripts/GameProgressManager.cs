using UnityEngine;

public static class GameProgressManager
{
    private const string LevelPrefix = "Level_";
    private const string LevelCoinsPrefix = "LevelCoins_";
    private const string LevelTimePrefix = "LevelTime_";

    public static void SetLevelCompleted(int levelIndex, int coins, int time, bool isCompleted = true)
    {
        PlayerPrefs.SetInt(LevelPrefix + levelIndex, isCompleted ? 1 : 0);
        PlayerPrefs.SetInt(LevelCoinsPrefix + levelIndex, coins);
        PlayerPrefs.SetInt(LevelTimePrefix + levelIndex, time);
        PlayerPrefs.Save();
    }

    public static bool IsLevelCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt(LevelPrefix + levelIndex, 0) == 1;
    }

    public static int GetLevelCoins(int levelIndex)
    {
        return PlayerPrefs.GetInt(LevelCoinsPrefix + levelIndex, 0);
    }

    public static int GetLevelTime(int levelIndex)
    {
        return PlayerPrefs.GetInt(LevelTimePrefix + levelIndex, 0);
    }

    public static void ResetProgress()
    {
        for(int i = 1; i <= 3; i++)
        {
            PlayerPrefs.DeleteKey(LevelPrefix + i);
            PlayerPrefs.DeleteKey(LevelCoinsPrefix + i);
            PlayerPrefs.DeleteKey(LevelTimePrefix + i);
        }
        PlayerPrefs.Save();
    }
}
