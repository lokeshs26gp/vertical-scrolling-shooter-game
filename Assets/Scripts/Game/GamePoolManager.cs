using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePoolManager : MonoBehaviour, IGameSystem
{
    [Header("Connectors")]
    public GamePoolConnector gamePoolConnector;

    private Dictionary<Entity.EntityType, Queue<Entity.Entity>> poolDictionary;
    public void Initilize()
    {
        poolDictionary = new Dictionary<Entity.EntityType, Queue<Entity.Entity>>(100);
        gamePoolConnector.Register(MoveToPool);
        gamePoolConnector.Register(GetPooledEntity);
    }
    private Entity.Entity GetPooledEntity(Entity.EntityType type)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            return null;
        }
        else if (poolDictionary[type].Count <= 0)
        {
            return null;
        }
        else
            return poolDictionary[type].Dequeue();
    }
    private void MoveToPool(Entity.EntityType entityType, Entity.Entity entity)
    {
        if(!poolDictionary.ContainsKey(entityType))
        {
            poolDictionary.Add(entityType, new Queue<Entity.Entity>(50));
        }
        else
        {
            poolDictionary[entityType].Enqueue(entity);
        }
    }
    public void DeInitilize()
    {
        gamePoolConnector.UnRegister(MoveToPool);
        gamePoolConnector.UnRegister(GetPooledEntity);
        poolDictionary.Clear();
        gamePoolConnector.Reset();

    }

    

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
