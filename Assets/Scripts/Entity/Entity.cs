using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public enum EntityType
    {
        None,
        Player,
        Opponent,
        Bullet
    }
    public abstract class Entity : MonoBehaviour
    {
        [Header("Connectors")]
        public GamePoolConnector gamePoolConnector;
        public abstract EntityType Type();

        [System.NonSerialized]public Entity parentEntity = null;

        protected EntityStatsDataSet Stats { get; set; }

        private IHarmfull iHarmfull;


        public IHarmfull GetHarmfull
        {
            get
            {
                if (iHarmfull is default(IHarmfull))
                    iHarmfull = GetComponent<IHarmfull>();
                //if (iHarmfull is default(IHarmfull))
                  //  iHarmfull = GetComponentInChildren<IHarmfull>();
                return iHarmfull;
            }
        }
        private IMovement movement;

        public IMovement GetMovement
        {
            get
            {
                if (movement is default(IMovement))
                    movement = GetComponent<IMovement>();
               // if (movement is default(IMovement))
                 //   movement = GetComponentInChildren<IMovement>();
                return movement;
            }
        }

        public virtual void Initilize(Entity parentEntity, EntityStatsDataSet parent, BulletStatsDataSet bullet)
        {
            IEntitySubSystem[] subsystems = GetComponentsInChildren<IEntitySubSystem>();

            for (int i = 0; i < subsystems.Length; i++)
            {
                subsystems[i].Initilize(this);
            }
            gameObject.SetActive(true);
        }

        public abstract void Reset();

        public virtual void Damage(int amount,bool maxDamage = false) { }

        public virtual void Die() { }

       
   
    }
}
