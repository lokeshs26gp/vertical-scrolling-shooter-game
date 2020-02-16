using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class Health : MonoBehaviour,IHealth
    {
        int maxHealth;

        int health;
        public void Initilize(int pMax)
        {
            maxHealth = pMax;
            health = maxHealth;
        }
        public void ReceiveDamage(Entity entity, int pAmount)
        {
            health -= pAmount;
            if(health<=0)
            {
                entity.Die();
            }
           
        }
        public void Reset()
        {
           
        }

        
    }
}
