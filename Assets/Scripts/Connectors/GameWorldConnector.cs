using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class GameWorldConnector : Connector
{
    public const string fileName = "GameWorldConnector";

    public System.Func<SpawnLocation, Transform> GetSpawnPoint;

    public System.Func<OpponentType, Locations> GetSpawnLocations;

    public override void Reset()
    {
        GetSpawnPoint = null;
        GetSpawnLocations = null;
    }

    public void Register(System.Func<SpawnLocation, Transform> action)
    {
        GetSpawnPoint += action;
    }
    public void UnRegister(System.Func<SpawnLocation, Transform> action)
    {
        GetSpawnPoint -= action;
    }

    public void Register(System.Func<OpponentType, Locations> action)
    {
        GetSpawnLocations += action;
    }
    public void UnRegister(System.Func<OpponentType, Locations> action)
    {
        GetSpawnLocations -= action;
    }
}
