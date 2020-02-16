using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData 
{
    private const string SAVEDATA = "SAVEDATA";

    public int PlayerLevel = 1;
    public int HighestScore = 0;
    public int TotalKillCount = 0;

    public string GetSaveData()
    {
        return JsonUtility.ToJson(this);
    }
    public static GameSaveData CreateSaveData(string dataJson)
    {
       return JsonUtility.FromJson<GameSaveData>(dataJson);
    }

    public static GameSaveData CreateSaveDataFromPref()
    {
        
        string jsonString = PlayerPrefs.GetString(SAVEDATA,string.Empty);
        Debug.Log(jsonString);
        if (string.IsNullOrEmpty(jsonString))
        {
            return new GameSaveData();
        }
        else
        {
             return JsonUtility.FromJson<GameSaveData>(jsonString);
        }
    }

    public void Save(GameSessionData data)
    {
        PlayerLevel = data.playerLevel;
        HighestScore = data.highScore;
        TotalKillCount += data.killCount;
        PlayerPrefs.SetString(SAVEDATA, JsonUtility.ToJson(this));
    }
    public void Save()
    {
        PlayerPrefs.SetString(SAVEDATA, JsonUtility.ToJson(this));
    }
    public void Delete()
    {
        PlayerPrefs.DeleteKey(SAVEDATA);
    }

}
