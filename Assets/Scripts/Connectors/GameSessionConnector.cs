using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class GameSessionConnector : Connector
{
    public const string fileName = "GameSessionConnector";

    public delegate void OnChangeDataEvent(PlayerActivity activity,int changeAmount) ;

    public OnChangeDataEvent OnPlayerActivityChange;

    public System.Func<GameSessionData> GetSessionData;

    public event System.Action<PlayerActivity, GameSessionData> OnPlayerActivityChangedEvent;

    public System.Action SpawnPlayer;

    public override void Reset()
    {
        OnPlayerActivityChange = null;
        GetSessionData = null;
        OnPlayerActivityChangedEvent = null;
        SpawnPlayer = null;
    }

    public void PlayerActivityChanged(PlayerActivity activity, GameSessionData data)
    {
        if (OnPlayerActivityChangedEvent != null)
            OnPlayerActivityChangedEvent(activity, data);
    }
    public void RegisterEvent(System.Action action)
    {
        SpawnPlayer += action;
    }
    public void UnRegisterEvent(System.Action action)
    {
        SpawnPlayer -= action;
    }

    public void RegisterEvent(OnChangeDataEvent action)
    {
        OnPlayerActivityChange += action;
    }
    public void UnRegisterEvent(OnChangeDataEvent action)
    {
        OnPlayerActivityChange -= action;
    }

    public void RegisterEvent(System.Action<PlayerActivity, GameSessionData> action)
    {
        OnPlayerActivityChangedEvent += action;
    }
    public void UnRegisterEvent(System.Action<PlayerActivity, GameSessionData> action)
    {
        OnPlayerActivityChangedEvent -= action;
    }

    public void Register(System.Func<GameSessionData> action)
    {
        GetSessionData = action;
    }
   

}
