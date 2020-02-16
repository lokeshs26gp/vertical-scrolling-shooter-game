using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;
public class GameLevelLoader : MonoBehaviour, IGameSystem
{

    [Header("Connectors")]
    public ResourceConnector resourceConnector;
    public GameStateConnector gameStateConnector;
    public GameWorldConnector gameWorldConnector;
    public GameSpawnConnector gameSpawnConnector;
    public GameSessionConnector gameSessionConnector;

    private LevelDesignDataSet levelData;
    
    public void Initilize()
    {
        levelData = gameSessionConnector.GetSessionData().levelDesignData;

        
        SpawnPlayer();
        SpawnAI();

        gameSessionConnector.RegisterEvent(SpawnPlayer);
        gameSessionConnector.PlayerActivityChanged(PlayerActivity.ALL, gameSessionConnector.GetSessionData());
        gameStateConnector.GameStateChangeTrigger(GameState.Running);
        
    }

    public void SpawnPlayer()
    {
        Transform playerPoint = gameWorldConnector.GetSpawnPoint(SpawnLocation.Player);
        PlayerEntity player = gameSpawnConnector.GetPlayerEntity(EntityType.Player, playerPoint.position);
        player.Initilize(null, levelData.playerStats, levelData.playerBulletStats);
        
    }

    public void SpawnAI()
    {
        for (int i = 0; i < levelData.levelDesign.LevelSequenceDesign.Count; i++)
        {
            AIEntity AI1 = gameSpawnConnector.GetAIEntity(EntityType.Opponent, Vector3.one * 100);
            AI1.Initilize(null, levelData.levelDesign.LevelSequenceDesign[i].opponentStats, levelData.levelDesign.LevelSequenceDesign[i].bulletStats);
            AI1.InitilizeAI(levelData.levelDesign.LevelSequenceDesign[i]);
        }
    }

    public void DeInitilize()
    {
        gameSessionConnector.UnRegisterEvent(SpawnPlayer);
    }


    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
