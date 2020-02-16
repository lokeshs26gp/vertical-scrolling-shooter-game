using System.Collections.Generic;
using UnityEngine;

    public enum BackGroundsType
    {
        Sea_1,
        Sea_2,
        Sea_3,
        Land_1,
        Land_2,
        Land_3,
        Ship,
    }
    public enum BulletType
    {
        Bullet_1, Bullet_2, Bullet_3, Bullet_4, Bullet_5
    }
    public enum OpponentType
    {
        Stright_Top_Bottom,
        Stright_Bottom_Top,
        Stright_Left_Right,
        Stright_Right_Left,
        Diagonal_TopLeft_Right,
        Diagonal_TopRight_Left,
        Diagonal_BottomLeft_Right,
        Diagonal_BottomRight_Left,
        Circle_ClockWise,
        Circle_AntiClockWise
    }
    public enum SystemType  {player,bgComponent,ResourceManager, InputManager, GameWorld, BackgroundManager,UIManager,Opponent,Bullet,GameLoadManger,GameSpawnFactory,JobSystem,GamePoolManager, GameLevelLoader ,GameSessionManager}
    [System.Serializable]
    public class BackGroundResource
    {
        public BackGroundsType BgType;
        [ResourcePath(typeof(Sprite))]
        public string spritePath;
    }
    [System.Serializable]
    public class BulletsResource
    {
        public BulletType bulletType;
        [ResourcePath(typeof(GameObject))]
        public string prefabPath;
    }
    [System.Serializable]
    public class OpponentsResource
    {
        public OpponentType opponentType;
        [ResourcePath(typeof(GameObject))]
        public string prefabPath;
    }
    [System.Serializable]
    public class ManagersResource
    {
        [ResourcePath(typeof(GameObject))]
        public string ResourceManagerPath;
        [ResourcePath(typeof(GameObject))]
        public string InputManagerPath;
        [ResourcePath(typeof(GameObject))]
        public string UIManagerPath;
        [ResourcePath(typeof(GameObject))]
        public string GameLoadManagerPath;
        [ResourcePath(typeof(GameObject))]
        public string JobSystemManagerPath;
       

     }
     [System.Serializable]
     public class GamePrefabs
     { 
        [ResourcePath(typeof(GameObject))]
        public string GameWorldPath;
        [ResourcePath(typeof(GameObject))]
        public string playerPrefabPath;
        [ResourcePath(typeof(GameObject))]
        public string bulletPrefabPath;
        [ResourcePath(typeof(GameObject))]
        public string opponentPrefabPath;
        [ResourcePath(typeof(GameObject))]
        public string backGroundPrefabPath;
        [ResourcePath(typeof(GameObject))]
        public string backGroundCompPrefabPath;
        [ResourcePath(typeof(GameObject))]
        public string GameSpawnFactoryPrefabPath;
    
        [ResourcePath(typeof(GameObject))]
        public string GamePoolPrefabPath;
        [ResourcePath(typeof(GameObject))]
        public string GameLevelLoaderPath;

        [ResourcePath(typeof(GameObject))]
        public string GameSessionManagerPath;


    }

[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
    public class ResourcesDataSet : DataSet
    {
        public const string fileName = "ResourcesDataSet";
        [Header("Drag&Drop Resource folder files only")]
        [Space(5)]
        public ManagersResource Managers;
        [Space(2)]
        public GamePrefabs gamePrefabs;
    
        [Space(2)]
        public List<BackGroundResource> BackGroundList;
        [Space(2)]
        public List<LevelDesignDataSet> LevelDesignDataList;
        public string GetBGResourcePath(string extra)
        {
            BackGroundsType bgtype = (BackGroundsType)System.Convert.ChangeType(extra, typeof(BackGroundsType));
            for (int i = 0; i < BackGroundList.Count; i++)
            {
                if (BackGroundList[i].BgType == bgtype)
                {
                    return BackGroundList[i].spritePath;
                }
            }
            
            
            return string.Empty;
        }
        public string GetPrefabPath(SystemType prefabType,string extra = "")
        {
            string path = string.Empty;
            switch(prefabType)
            {
                case SystemType.player              : path = gamePrefabs.playerPrefabPath; break;
                case SystemType.bgComponent         : path = gamePrefabs.backGroundCompPrefabPath; break;
                case SystemType.GameWorld           : path = gamePrefabs.GameWorldPath; break;
                case SystemType.ResourceManager     : path = Managers.ResourceManagerPath; break;
                case SystemType.BackgroundManager   : path = gamePrefabs.backGroundPrefabPath; break;
                case SystemType.InputManager        : path = Managers.InputManagerPath; break;
                case SystemType.Opponent            : path = gamePrefabs.opponentPrefabPath;break;
                case SystemType.Bullet              : path = gamePrefabs.bulletPrefabPath; break;
                case SystemType.UIManager           : path = Managers.UIManagerPath;break;
                case SystemType.GameLoadManger      : path = Managers.GameLoadManagerPath;break;
                case SystemType.GameSpawnFactory    : path = gamePrefabs.GameSpawnFactoryPrefabPath; break;
                case SystemType.JobSystem           : path = Managers.JobSystemManagerPath; break;
                case SystemType.GamePoolManager     : path = gamePrefabs.GamePoolPrefabPath;break;
                case SystemType.GameLevelLoader     : path = gamePrefabs.GameLevelLoaderPath; break;
                case SystemType.GameSessionManager  : path = gamePrefabs.GameSessionManagerPath; break;
        }
             return path;
        }
      
        public LevelDesignDataSet GetLevel(int level)
        {
              return  LevelDesignDataList.Find(x => x.level == level);
            
        }
        
        public string[] GetAllResourcePaths()
        {
            List<string> allPaths = new List<string>(100);
            for (int i = 0; i < BackGroundList.Count; i++)
                allPaths.Add(BackGroundList[i].spritePath);

           int length = System.Enum.GetNames(typeof(SystemType)).Length;
            for (int i = 0; i < length; i++)
                  allPaths.Add(GetPrefabPath((SystemType)i));

            return allPaths.ToArray();
        }
    }
