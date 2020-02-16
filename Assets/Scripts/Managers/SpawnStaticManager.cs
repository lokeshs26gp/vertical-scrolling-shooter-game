using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnStaticManager 
{

    public static T Spawn<T>(string resourcePath)
    {
        GameObject prefab = Resources.Load<GameObject>(resourcePath);

        return Spawn<T>(prefab);
    }
    public static T Spawn<T>(GameObject prefab)
    {
        GameObject @Object = GameObject.Instantiate(prefab);
        T system = @Object.GetComponent<T>();
        return system;
    }

    public static T Spawn<T>(ResourceConnector resource,SystemType prefabType)
    {
        GameObject prefab = resource.GetLoadedPrefab(prefabType);

        return Spawn<T>(prefab);
    }

    public static T Spawn<T>(ResourceConnector resource, SystemType prefabType,Vector3 worldPosition)
    {
        GameObject prefab = resource.GetLoadedPrefab(prefabType);
        prefab.transform.position = worldPosition;
        return Spawn<T>(prefab);
    }
}
