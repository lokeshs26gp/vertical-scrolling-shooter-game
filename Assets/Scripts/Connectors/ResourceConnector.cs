using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = fileName, menuName = Connector.prefixPath+fileName)]
    public class ResourceConnector : Connector
    {
        public const string  fileName = "ResourceConnector";
        public delegate void LoadAllAssets(int completed, int count, bool success);
     
        private event LoadAllAssets loadedAllAssetsEvent;

        public Func<BackGroundsType, Sprite> GetLoadedBGSprite;
        public Func<SystemType, GameObject> GetLoadedPrefab;
        public Func<int, LevelDesignDataSet> GetLevelData;

        public override void Reset()
        {
            loadedAllAssetsEvent = null;
            GetLoadedBGSprite = null;
            GetLoadedPrefab = null;
            GetLevelData = null;
        }

        public void RegisterEvent(LoadAllAssets pEvent)
        {
            loadedAllAssetsEvent += pEvent;
        }

        public void UnRegisterEvent(LoadAllAssets pEvent)
        {
            loadedAllAssetsEvent -= pEvent;
        }

        public void InvokeLoadAllAssets(int completed,int count,bool success)
        {
            if (loadedAllAssetsEvent != null)
                loadedAllAssetsEvent(completed, count, success);
        }

        
    }

