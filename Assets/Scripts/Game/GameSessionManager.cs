using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSessionData
{
    public int playerLevel;
    public int highScore;
    public int lifes;
    public int playerMaxHealth;
    public int playerHealth;
    public int distanceTravelled;
    public int killCount;
    public LevelDesignDataSet levelDesignData { get; private set; }
    public Objective objective { get; private set; }

    public bool isSuccess = false;
    public bool isGameover = false;
    public GameSessionData(GameSaveData gameSaveData, LevelDesignDataSet levelDesignDataSet)
    {
        highScore       = gameSaveData.HighestScore;
        playerLevel     = gameSaveData.PlayerLevel;
        levelDesignData = levelDesignDataSet;
        playerMaxHealth = levelDesignDataSet.playerStats.MaxHealth;
        lifes           = levelDesignDataSet.levelStats.lifes;
        objective       = levelDesignDataSet.levelStats.objective;
        playerHealth    = playerMaxHealth;
    }

    public bool CheckObjectiveachieved()
    {
        switch(objective.objectiveType)
        {
            case GameObjectiveType.Distance_Survive:
                return distanceTravelled >= objective.killRDistance;
                
            case GameObjectiveType.kill_Count:
                return killCount >= objective.killRDistance;
            default:
                return false;
        }
        
    }

    public void ResetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
    }

    public bool CheckGameOver()
    {
        return lifes <= 0;
    }
    public void CalculateHighScore()
    {
        if (killCount > highScore) 
            highScore = killCount;
    }
}
public enum PlayerActivity { ALL,Life,Health,DistanceTravel,KillCount,Win,Loss}

public class GameSessionManager : MonoBehaviour, IGameSystem
{

    [Header("Connectors")]
    public GameSessionConnector gameSessionConnector;
    public ResourceConnector resourceConnector;
    public GameStateConnector gameStateConnector;
 
    private GameSessionData sessionData;
    private GameSaveData gameSaveData;
    public void Initilize()
    {
        gameSaveData = GameSaveData.CreateSaveDataFromPref();

        LevelDesignDataSet levelData = resourceConnector.GetLevelData(gameSaveData.PlayerLevel);

        if (levelData == null)
        {
            gameSaveData.PlayerLevel = 1;//Reset to level 1 As all levels done
            gameSaveData.Save();
            levelData = resourceConnector.GetLevelData(gameSaveData.PlayerLevel);
            
        }
        
        sessionData = new GameSessionData(gameSaveData, levelData);

        gameSessionConnector.RegisterEvent(PlayerActivityMonitor);
        gameSessionConnector.Register(GetSessionData);

       
    }
    private GameSessionData GetSessionData()
    {
        return sessionData;
    }
    private void PlayerActivityMonitor(PlayerActivity activity,int change)
    {
        if (sessionData.isGameover || sessionData.isSuccess) return;
        switch(activity)
        {
            
            case PlayerActivity.Health:
                sessionData.playerHealth -= change;//health change will trigger life
                break;
            case PlayerActivity.Life:
                sessionData.lifes -= change;
               
                 break;
            case PlayerActivity.KillCount:
                sessionData.killCount += change;
                sessionData.CalculateHighScore();
                break;
            case PlayerActivity.DistanceTravel:
                sessionData.distanceTravelled = change;
                break;
            
        }
        bool isGameOver = sessionData.CheckGameOver();
        bool success = sessionData.CheckObjectiveachieved();

        if (success)
        {
            sessionData.isSuccess = true;
            sessionData.playerLevel++;
            gameSaveData.Save(sessionData);
            gameSessionConnector.PlayerActivityChanged(PlayerActivity.Win, sessionData);
            gameStateConnector.GameStateChangeTrigger(GameState.GameOver);
            
        }
        else if (isGameOver)
        {
            sessionData.isGameover = true;
            gameSaveData.Save(sessionData);
            gameSessionConnector.PlayerActivityChanged(PlayerActivity.Loss, sessionData);
            gameStateConnector.GameStateChangeTrigger(GameState.GameOver);
            
        }
        else
        {
            if (activity == PlayerActivity.Life)
            {
                gameSessionConnector.SpawnPlayer();
                sessionData.ResetPlayerHealth();
            }
            gameSessionConnector.PlayerActivityChanged(activity, sessionData);
        }
    }

    public void DeInitilize()
    {
        gameSaveData.Save(sessionData);
        gameSessionConnector.UnRegisterEvent(PlayerActivityMonitor);
        gameSessionConnector.Register(null);
        gameSessionConnector.Reset();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
