
using UnityEngine;

    public class ResourceManager : MonoBehaviour,ISystem
    {
        public ResourcesDataSet resourcesData;
         [Header("Connectors")]
        public GameStateConnector GameStateConnector;
        public ResourceConnector resourceConnector;

        public ResourceLoader resourceLoader;

        public void Initilize()
        {
            GameStateConnector.RegisterListener(OnGameStateChange);
            resourceConnector.GetLoadedBGSprite += GetBackGroundSprite;
            resourceConnector.GetLoadedPrefab += GetLoadedPrefab;
             resourceConnector.GetLevelData += resourcesData.GetLevel;
            resourceConnector.RegisterEvent(OnResourceLoaded);

        }

        private void OnGameStateChange(GameState state,GameState previousState)
        {
            switch(state)
            {
                case GameState.LoadResources:
                  resourceLoader.LoadAllResources(resourcesData.GetAllResourcePaths(),resourceConnector.InvokeLoadAllAssets);
                    break;
                case GameState.Deinitilze:
                  
                    break;

            }
        }
        private void OnResourceLoaded(int completed, int count, bool success)
        {
            if (success)
            {
                GameStateConnector.GameStateChangeTrigger(GameState.Initilize);
            }
        }
        private Sprite GetBackGroundSprite(BackGroundsType groundsType)
        {
            string path = string.Empty;
            for(int i=0;i< resourcesData.BackGroundList.Count;i++)
            {
                if(resourcesData.BackGroundList[i].BgType == groundsType)
                {
                    path = resourcesData.BackGroundList[i].spritePath;
                    break;
                }
            }

             return resourceLoader.GetLoadedAsset<Sprite>(path);
                
        }

        private GameObject GetLoadedPrefab(SystemType pType)
        {
            string path = resourcesData.GetPrefabPath(pType);
            return resourceLoader.GetLoadedAsset<GameObject>(path);
        }
      
        public void DeInitilize()
        {
            GameStateConnector.UnRegisterListener(OnGameStateChange);
            resourceConnector.GetLoadedBGSprite -= GetBackGroundSprite;
            resourceConnector.GetLoadedPrefab -= GetLoadedPrefab;
            resourceConnector.GetLevelData -= resourcesData.GetLevel;
            resourceConnector.UnRegisterEvent(OnResourceLoaded);

            //resourceLoader.UnLoadResources();
         }

        public void Reset()
        {
       
        }

    #region UnUsedRChanged
    /*  private  U GetAsset<T,U>(T type)where T:System.Enum where U:UnityEngine.Object
        {
            Object @object = resourceLoader.GetLoadedAsset<T>(type);
            return (U)@object;
        }*/
    #endregion
}
