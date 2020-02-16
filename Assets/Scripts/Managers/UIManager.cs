using System;
using UnityEngine;

public class UIManager : MonoBehaviour,ISystem
{
    [Header("Connectors")]
    public GameStateConnector gameStateConnector;
    public ResourceConnector resourceConnector;
    public GameSessionConnector gameSessionConnector;

    [Space]
    public UIAllViewComponent view;


    public void Initilize()
    {
        gameStateConnector.RegisterListener(OnGameStateChange);
    }

    private void OnGameStateChange(GameState state,GameState prevState)
    {
        switch(state)
        {
            case GameState.LoadSystems:
            case GameState.LoadResources:
                resourceConnector.RegisterEvent(view.SetLoadingProgress);
                goto default;
            case GameState.Initilize:
                goto default;
            case GameState.Start:
                gameSessionConnector.RegisterEvent(OnPlayerActivityUpdater);
                goto default;
            case GameState.Running:
                goto default;
            case GameState.GameOver:
                gameSessionConnector.UnRegisterEvent(OnPlayerActivityUpdater);
                goto default;
            case GameState.Deinitilze:
                
                break;
            default:
                view.ActiviateUIPanel(state);
                break;
        }

        
    }

    private void OnPlayerActivityUpdater(PlayerActivity activity,GameSessionData data)
    {
        view.GamePlayUpdates(data.highScore, data.killCount, data.lifes, data.playerHealth, data.playerMaxHealth);
        if(activity == PlayerActivity.Win)
        {
            view.GameOverText("YOU WON!");
        }
        else if (activity == PlayerActivity.Loss)
        {
            view.GameOverText("YOU LOST!");
        }
    }

    public void DeInitilize()
    {
        gameStateConnector.UnRegisterListener(OnGameStateChange);
    }
    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAgain()
    {
        gameStateConnector.PlayAgain();
    }
}
