using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class Harmfull : MonoBehaviour, IHarmfull,ITriggerEnterReceiver
    {
                private int damangeAmount;
        private Entity thisEntity;
        public void Initilize(Entity thisEntity, int pDamage)
        {
            this.thisEntity = thisEntity;
            damangeAmount = pDamage;
        }

        public void SendDamage(Entity pOther)
        {
           // Debug.Log("SendDamage-> "+thisEntity.Type());
            if (thisEntity.parentEntity != null)//Bullet ->player,AI
            {
                if (pOther != thisEntity.parentEntity && pOther.Type() != thisEntity.parentEntity.Type())
                {
                    pOther.Damage(damangeAmount,false);
                    thisEntity.Damage(0,true);
                }
            }
            else if(pOther.Type() != thisEntity.Type() && pOther.parentEntity == null)//AI <->player
            {
                pOther.Damage(0,true);
                thisEntity.Damage(0,true);
            }
        }

        public void OnITriggerEnter(Entity collidedEntity, string fromName)
        {
            SendDamage(collidedEntity);
            
        }

      
    }
}
