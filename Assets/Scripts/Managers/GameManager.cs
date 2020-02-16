using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    LoadSystems,
    LoadResources,
    Initilize,
    Start,
    Running,
    GameOver,
    Deinitilze
}
public class GameManager : MonoBehaviour
{
    public ResourcesDataSet resourcesData;
    [Header("Connectors")]
    public GameStateConnector gameStateConnector;
    public ResourceConnector resourceConnector;

    private GameState gameState;

    public GameState GameState { get => gameState; private set => SetGameState(value); }
    
    private Dictionary<SystemType, ISystem> SystemDictionary;
    private void Awake()
    {

        SystemDictionary = new Dictionary<SystemType, ISystem>(25);

        SystemDictionary.Add(SystemType.ResourceManager, SpawnStaticManager.Spawn<ISystem>(resourcesData.Managers.ResourceManagerPath));
        SystemDictionary.Add(SystemType.UIManager, SpawnStaticManager.Spawn<ISystem>(resourcesData.Managers.UIManagerPath));
        SystemDictionary[SystemType.ResourceManager].Initilize();
        SystemDictionary[SystemType.UIManager].Initilize();

        gameStateConnector.RegisterListener(OnGameStateChange);
        gameStateConnector.RegisterListener(SetGameState);

        GameState = GameState.LoadSystems;
    }

    private void OnGameStateChange(GameState State, GameState prevState)
    {
        switch (State)
        {
            case GameState.LoadSystems:

                GameState = GameState.LoadResources;
                break;
            case GameState.Initilize:
                SystemDictionary.Add(SystemType.JobSystem, SpawnStaticManager.Spawn<ISystem>(resourceConnector, SystemType.JobSystem));
                SystemDictionary.Add(SystemType.InputManager, SpawnStaticManager.Spawn<ISystem>(resourceConnector, SystemType.InputManager));
                SystemDictionary.Add(SystemType.GameLoadManger, SpawnStaticManager.Spawn<ISystem>(resourceConnector, SystemType.GameLoadManger));
                SystemDictionary[SystemType.JobSystem].Initilize();
                SystemDictionary[SystemType.GameLoadManger].Initilize();
                 break;
            case GameState.Start:
                break;
            case GameState.Running:
                SystemDictionary[SystemType.InputManager].Initilize();
                break;
            case GameState.GameOver:
                gameStateConnector.PlayAgain += ReloadGame;
                break;
            case GameState.Deinitilze:
                Deinitilze();
                break;
        }
    }
    private void SetGameState(GameState nextState)
    {
        if (nextState != GameState)
        {
            gameStateConnector.InvokeGameStateChange(nextState, gameState);
            gameState = nextState;
        }
       // else
         //   Debug.LogError("Already state is changed to " + nextState);
    }
    private void Deinitilze()
    {
        foreach (KeyValuePair<SystemType, ISystem> system in SystemDictionary)
            system.Value.DeInitilize();

        gameStateConnector.UnRegisterListener(OnGameStateChange);
        gameStateConnector.UnRegisterListener(SetGameState);
        gameStateConnector.Reset();
    }
    private void ReloadGame()
    {
        GameState = GameState.Deinitilze;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    
    }

}
