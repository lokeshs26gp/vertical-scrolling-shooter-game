using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;
public enum SpawnLocation
{
   Player, Top_Left,Top_Right, Top_Center,Bottom_Left, Bottom_Right,Bottom_Center,Left_Top,Left_Center,Left_Bottom,Right_Top,Right_Center,Right_Bottom
}
[System.Serializable]
public class SpawnInfo
{
    public SpawnLocation location;
    public Transform point;
}

public struct Locations
{
   public  Vector3 start,end;
}
public class GameWorld : MonoBehaviour, IGameSystem
{
    [FormerlySerializedAs("SpawnInfo")]
    public SpawnInfo[] spawnPoints;// = new SpawnInfo[13];

    public MovementDataSet movementDataSet;
    [Header("Connectors")]
    public GameStateConnector gameStateConnector;
    public ResourceConnector resourceConnector;
    public GameWorldConnector gameWorldConnector;

    private Dictionary<SpawnLocation, Transform> spawnPointsDictionary;
    public void Initilize()
    {
      
        spawnPointsDictionary = new Dictionary<SpawnLocation, Transform>(spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointsDictionary.Add(spawnPoints[i].location, spawnPoints[i].point);
        }

        gameWorldConnector.Register(GetSpawnPoint);
        gameWorldConnector.Register(GetMovementInfo);
    }

    private Transform GetSpawnPoint(SpawnLocation location)
    {
        return spawnPointsDictionary[location];
    }
    private Locations GetMovementInfo(OpponentType opponentType)
    {
        MovementInfo info = movementDataSet.movementInfos.Find(x => x.opponentType == opponentType);

        return new Locations 
        { 
            start = spawnPointsDictionary[info.startLocation].position, 
            end = spawnPointsDictionary[info.endLocation].position 
        };
    }
    
    public void DeInitilize()
    {
        gameWorldConnector.UnRegister(GetSpawnPoint);
        gameWorldConnector.UnRegister(GetMovementInfo);
        gameWorldConnector.Reset();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    [ContextMenu("GetAllSpawnPoints")]
    public void GetAllSpawns()
    {
        Transform container = transform.Find("SpawnPointContainer");
        List<SpawnInfo> infos = new List<SpawnInfo>(50);
        for (int i=0;i<container.childCount;i++)
        {
            SpawnLocation location = (SpawnLocation)System.Enum.Parse(typeof(SpawnLocation), container.GetChild(i).name);
            SpawnInfo info = new SpawnInfo
            {
                location = location,
                point = container.GetChild(i)
            };
         
            infos.Add(info);
        }
        spawnPoints = infos.ToArray();
    }

}
