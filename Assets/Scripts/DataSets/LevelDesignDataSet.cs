using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public class EntityLevelDesign
    {
        public float startSpawnTimeDelaySec;
        public OpponentStatsDataSet opponentStats;
        public BulletStatsDataSet bulletStats;
        public float reSpawnDelaySec;
        public float deathReSpawnSec = 5.0f;
        public float fireDelaySec;
       
    }
    [System.Serializable]
    public class LevelDesign
    {
        public List<EntityLevelDesign> LevelSequenceDesign;
    }
    public enum GameObjectiveType
    {
        Distance_Survive, kill_Count
}
    [System.Serializable]
    public class Objective
    {
        public GameObjectiveType objectiveType;
        public int killRDistance;
    }
    [System.Serializable]
    public class LevelStats
    {
        public int lifes;
        public Objective objective;
    }
    [CreateAssetMenu(fileName = "LevelDesign", menuName = prefixPath + fileName)]
    public class LevelDesignDataSet : DataSet
    {
        public const string fileName = "LevelDesignDataSet";
        public int level;
        public PlayerStatsDataSet playerStats;
        public BulletStatsDataSet playerBulletStats;
        public LevelStats levelStats;
        public LevelDesign levelDesign;
    }

