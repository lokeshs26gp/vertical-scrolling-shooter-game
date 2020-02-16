using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;
public class GameSpawnFactory : MonoBehaviour,IGameSystem
{
    [Header("Connectors")]
    public ResourceConnector resourceConnector;
    public GamePoolConnector gamePoolConnector;
    public GameSpawnConnector gameSpawnConnector;

    private GameObject spawnContainer;
    private HashSet<Entity.Entity> allEntityHash;
    public void Initilize()
    {
        allEntityHash = new HashSet<Entity.Entity>();
        spawnContainer = new GameObject("SpawnContainer");
        spawnContainer.transform.SetParent(transform);

        gameSpawnConnector.Register(SpawnEntity);
        gameSpawnConnector.Register(SpawnEntity<PlayerEntity>);
        gameSpawnConnector.Register(SpawnEntity<AIEntity>);
        gameSpawnConnector.Register(SpawnEntity<BulletEntity>);
    }

   
    public T SpawnEntity<T>(EntityType etype,Vector3 worldPosition)where T:Entity.Entity
    {
        return (T)SpawnEntity(etype, worldPosition);
    }
    public Entity.Entity SpawnEntity(EntityType eType,Vector3 worldPosition) 
    {
        Entity.Entity spawnEntity = gamePoolConnector.pooledEntity(eType);
        if(spawnEntity == null)
        {
            switch (eType)
            {
                case EntityType.Player:
                    spawnEntity = SpawnStaticManager.Spawn<Entity.Entity>(resourceConnector, SystemType.player);
                    break;
                case EntityType.Opponent:
                    spawnEntity = SpawnStaticManager.Spawn<Entity.Entity>(resourceConnector, SystemType.Opponent);
                    break;
                case EntityType.Bullet:
                    spawnEntity = SpawnStaticManager.Spawn<Entity.Entity>(resourceConnector, SystemType.Bullet);
                    break;
                default:
                    break;
            }
            spawnEntity.transform.SetParent(spawnContainer.transform);
        }
        allEntityHash.Add(spawnEntity);
        spawnEntity.gameObject.SetActive(false);
        spawnEntity.transform.position = worldPosition;
        return spawnEntity;

    }


    public void DeInitilize()
    {
        Destroy(spawnContainer);
        gameSpawnConnector.UnRegister(SpawnEntity);
        gameSpawnConnector.UnRegister(SpawnEntity<PlayerEntity>);
        gameSpawnConnector.UnRegister(SpawnEntity<AIEntity>);
        gameSpawnConnector.UnRegister(SpawnEntity<BulletEntity>);
        gameSpawnConnector.Reset();
    
        foreach(Entity.Entity e in allEntityHash)
        {
            Destroy(e.gameObject);
        }
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
