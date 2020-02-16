using Entity;
using System;
using UnityEngine;
[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class GameSpawnConnector : Connector
{
    public const string fileName = "GameSpawnConnector";
    
    public Func<EntityType, Vector3,Entity.Entity> GetEntity;

    public delegate T GetEntityT<T>(EntityType type, Vector3 pos) where T : Entity.Entity;

    public GetEntityT<PlayerEntity> GetPlayerEntity;
    public GetEntityT<AIEntity> GetAIEntity;
    public GetEntityT<BulletEntity> GetBulletEntity;

    public override void Reset()
    {
        GetEntity = null;
        GetPlayerEntity = null;
        GetAIEntity = null;
        GetBulletEntity = null;
    }

    public void Register(Func<EntityType, Vector3, Entity.Entity> action)
    {
        GetEntity += action;
    }
    public void UnRegister(Func<EntityType, Vector3, Entity.Entity> action)
    {
        GetEntity -= action;
    }

    public void Register(GetEntityT<PlayerEntity> action)
    {
        GetPlayerEntity += action;
    }
    public void Register(GetEntityT<AIEntity> action)
    {
        GetAIEntity += action;
    }
     public void Register(GetEntityT<BulletEntity> action)
    {
        GetBulletEntity += action;
    }

    public void UnRegister(GetEntityT<PlayerEntity> action)
    {
        GetPlayerEntity -= action;
    }
    public void UnRegister(GetEntityT<AIEntity> action)
    {
        GetAIEntity -= action;
    }
    public void UnRegister(GetEntityT<BulletEntity> action)
    {
        GetBulletEntity -= action;
    }

}
