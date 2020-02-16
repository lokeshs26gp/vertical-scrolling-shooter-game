using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class BulletEntity : Entity
    {
        public JobSystemConnector jobSystemConnector;
        public override EntityType Type() { return EntityType.Bullet; }
        public override void Initilize(Entity parentEntity,EntityStatsDataSet parentStats , BulletStatsDataSet bulletStats)
        {
            this.parentEntity = parentEntity;

            jobSystemConnector.registerCountDown(2, AutoDie);
            
            GetHarmfull.Initilize(this, bulletStats.damageAmount);
            
            gameObject.SetActive(true);
        }
  
        public override void Reset()
        {
            gamePoolConnector.moveToPool(EntityType.Bullet, this);
            jobSystemConnector.forceRemoveCountDown(AutoDie);
            
        }
        public override void Damage(int amount,bool maxDamage = false)
        {
            Die();
        }
        public override void Die()
        {
            GetMovement.Reset();
            gameObject.SetActive(false);
            Reset();
        }
        private void AutoDie(bool die)
        {
            Die();
        }
    }

}