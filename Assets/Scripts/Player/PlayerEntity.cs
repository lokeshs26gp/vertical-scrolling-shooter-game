using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Entity
{
    public class PlayerEntity : Entity
    {
        
        public  GameSessionConnector gameSessionConnector;
        private IHealth iHealth;
        private IFire fire;
        private PlayerInputListener playerInput;
        #region public methods

        public override EntityType Type() { return EntityType.Player; }
        public IHealth GetHealth
        {
            get
            {
                if (iHealth is default(IHealth))
                    iHealth = GetComponent<IHealth>();
                if (iHealth is default(IHealth))
                    iHealth = GetComponentInChildren<IHealth>();
                return iHealth;
            }
        }

        

        public IFire GetFire
        {
            get
            {
                if (fire is default(IFire))
                    fire = GetComponent<IFire>();
                if (fire is default(IFire))
                    fire = GetComponentInChildren<IFire>();
                return fire;
            }
        }
       
        public PlayerInputListener PlayerInput
        {
            get
            {
                if (playerInput == null)
                    playerInput = GetComponent<PlayerInputListener>();
                if (playerInput == null)
                    playerInput = GetComponentInChildren<PlayerInputListener>();
                return playerInput;
            }
        }

        public override void Initilize(Entity parent, EntityStatsDataSet parentStats, BulletStatsDataSet childStats)
        {
            parentEntity = null;//AI,Player dont have parents, only bullet have to
            Stats = parentStats;
            GetHealth.Initilize(parentStats.MaxHealth);
            GetFire.Initilize(this, parentStats.fireCoolDownTime, childStats);
            GetHarmfull.Initilize(this, parentStats.damageAmount);
            base.Initilize(parent, parentStats, childStats);
       
        }
       
        public override void Damage(int amount,bool maxDamage = false)
        {
            if(!maxDamage)
                GetHealth.ReceiveDamage(this,amount);
            else
            {
                GetHealth.ReceiveDamage(this, Stats.MaxHealth);//or can directly make it die based on design
            }
            
            gameSessionConnector.OnPlayerActivityChange(PlayerActivity.Health, amount);
        }
        
        public override void Reset()
        {
            fire.Reset();
            gameObject.SetActive(false);
        }

        public override void Die()
        {
            Reset();
            gamePoolConnector.moveToPool(EntityType.Player, this);
            gameSessionConnector.OnPlayerActivityChange(PlayerActivity.Life, 1);

        }
        #endregion

    }
}
