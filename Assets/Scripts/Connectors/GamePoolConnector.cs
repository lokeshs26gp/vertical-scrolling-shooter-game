using UnityEngine;
[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class GamePoolConnector : Connector
{
    public const string fileName = "GamePoolConnector";

    public delegate void MoveToPool(Entity.EntityType entityType, Entity.Entity entity);

    public  MoveToPool moveToPool;

    public System.Func<Entity.EntityType, Entity.Entity> pooledEntity;

    public void Register(MoveToPool action)
    {
        moveToPool += action;
    }

    public void UnRegister(MoveToPool action)
    {
        moveToPool -= action;
    }

    public void Register(System.Func<Entity.EntityType, Entity.Entity> action)
    {
        pooledEntity += action;
    }

    public void UnRegister(System.Func<Entity.EntityType, Entity.Entity> action)
    {
        pooledEntity -= action;
    }
    public override void Reset()
    {
        moveToPool = null;
        pooledEntity = null;
    }

}
