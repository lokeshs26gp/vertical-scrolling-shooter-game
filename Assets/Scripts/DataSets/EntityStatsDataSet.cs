
    using UnityEngine;
   
    public abstract class EntityStatsDataSet : DataSet
    {
        public new const  string  prefixPath = DataSet.prefixPath + "Entity/";
        [Range(0.1f,10.0f)]
        public float MovementSpeed;
        [Range(0, 100)]
        public int damageAmount;//Giving damage

        public int MaxHealth;
        public float fireCoolDownTime = 0.25f;

    }
   
   
    
