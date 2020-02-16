using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadManager : MonoBehaviour, ISystem
{

    [Header("Connectors")]
    public ResourceConnector resourceConnector;
    public GameStateConnector gameStateConnector;

    private Dictionary<SystemType, IGameSystem> gameSystemDictionary;
    //private GameWorld 
    public void Initilize()
    {
        
        gameStateConnector.RegisterListener(OnGameStateChange);
        gameSystemDictionary = new Dictionary<SystemType, IGameSystem>(25);
        gameSystemDictionary.Add(SystemType.GameSessionManager, SpawnStaticManager.Spawn<IGameSystem>(resourceConnector, SystemType.GameSessionManager));
        gameSystemDictionary.Add(SystemType.GameWorld, SpawnStaticManager.Spawn<IGameSystem>(resourceConnector, SystemType.GameWorld));
        gameSystemDictionary.Add(SystemType.BackgroundManager, SpawnStaticManager.Spawn<IGameSystem>(resourceConnector, SystemType.BackgroundManager));
        gameSystemDictionary.Add(SystemType.GameSpawnFactory, SpawnStaticManager.Spawn<IGameSystem>(resourceConnector, SystemType.GameSpawnFactory));
        gameSystemDictionary.Add(SystemType.GamePoolManager, SpawnStaticManager.Spawn<IGameSystem>(resourceConnector, SystemType.GamePoolManager));
        gameSystemDictionary.Add(SystemType.GameLevelLoader, SpawnStaticManager.Spawn<IGameSystem>(resourceConnector, SystemType.GameLevelLoader));

        gameSystemDictionary[SystemType.GameSessionManager].Initilize();
        gameSystemDictionary[SystemType.GameWorld].Initilize();
        gameSystemDictionary[SystemType.BackgroundManager].Initilize();
        gameSystemDictionary[SystemType.GameSpawnFactory].Initilize();
        gameSystemDictionary[SystemType.GamePoolManager].Initilize();

       
        gameStateConnector.GameStateChangeTrigger(GameState.Start);

    }

    private void OnGameStateChange(GameState State, GameState prevState)
    {
        switch(State)
        {
            case GameState.Initilize:
                break;
            case GameState.Start:
                gameSystemDictionary[SystemType.GameLevelLoader].Initilize();
                break;
            case GameState.Running:
                break;
            case GameState.GameOver:
                break;
            case GameState.Deinitilze:
                break;
        }
    }

    public void DeInitilize()
    {
        gameStateConnector.UnRegisterListener(OnGameStateChange);

        foreach (KeyValuePair<SystemType, IGameSystem> system in gameSystemDictionary)
            system.Value.DeInitilize();
    }
    public void Reset(){ }
}
