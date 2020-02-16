using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class GameStateConnector :Connector
{
    public const string fileName = "GameStateConnector";

    public delegate void GameStateChangeEvent(GameState state, GameState previousState);

    private GameStateChangeEvent gameStateChangeEvent;

    public System.Action<GameState> GameStateChangeTrigger;

    public System.Action PlayAgain;
    public void RegisterListener(GameStateChangeEvent listener)
    {
        gameStateChangeEvent += listener;
    }
    public void UnRegisterListener(GameStateChangeEvent listener)
    {
        gameStateChangeEvent -= listener;
    }
    public void RegisterListener(System.Action<GameState> listener)
    {
        GameStateChangeTrigger += listener;
    }
    public void UnRegisterListener(System.Action<GameState> listener)
    {
        GameStateChangeTrigger -= listener;
    }

    public void InvokeGameStateChange(GameState state, GameState previousState)
    {
        gameStateChangeEvent(state, previousState);
    }

    public void RegisterListener(System.Action action)
    {
        PlayAgain += action;
    }

    public override void Reset()
    {
        GameStateChangeTrigger = null;
        gameStateChangeEvent = null;
        PlayAgain = null;
    }

    
}
