using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Entity
{
    public class AIEntity : PlayerEntity
    {
        public override EntityType Type() { return EntityType.Opponent; }

        private AIBehaviour aIBehaviour;

        public AIBehaviour AIBehaviour
        {
            get
            {
                if (aIBehaviour == null)
                    aIBehaviour = GetComponent<AIBehaviour>();
                return aIBehaviour;
            }
        }

        public void InitilizeAI(EntityLevelDesign levelDesign)
        {
            AIBehaviour.Initilize(this, levelDesign);
        }

        public override void Damage(int amount, bool maxDamage = false)
        {
            // base.Damage(amount, maxDamage);
            if (!maxDamage)
                GetHealth.ReceiveDamage(this, amount);
            else
            {
                GetHealth.ReceiveDamage(this, Stats.MaxHealth);//or can directly make it die based on design
            }
        }

        public override void Die()
        {
            AIBehaviour.Kill();
            gameSessionConnector.OnPlayerActivityChange(PlayerActivity.KillCount, 1);
        }

    }
}