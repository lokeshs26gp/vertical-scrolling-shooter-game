using System.Collections.Generic;
using UnityEngine;
namespace Entity
{
    public class Fire : MonoBehaviour, IFire
    {
        
        public List<Transform> bulletPoints;

        [Header("Connector")]
        public GameSpawnConnector gameSpawnConnector;
        public JobSystemConnector jobSystemConnector;

        protected Entity cacheThisEntity;
        protected BulletStatsDataSet cachedbulletStats;
        protected bool canShoot = true;
        protected bool forceQuit = false;
        private float fireCoolDown;
        public void Initilize(Entity entity,float cooldown, BulletStatsDataSet bulletStats)
        {
            cacheThisEntity = entity;
            cachedbulletStats = bulletStats;
            fireCoolDown = cooldown;
        }

        

        public virtual void Shoot()
        {
            if (canShoot && !forceQuit)
            {
                Entity bullet1 = gameSpawnConnector.GetEntity(EntityType.Bullet, bulletPoints[0].position);
                Entity bullet2 = gameSpawnConnector.GetEntity(EntityType.Bullet, bulletPoints[1].position);
                bullet1.Initilize(cacheThisEntity,null, cachedbulletStats);
                bullet2.Initilize(cacheThisEntity, null, cachedbulletStats);
                Vector3 bulletDir1 = bulletPoints[0].up.normalized * 20f;
                Vector3 bulletDir2 = bulletPoints[1].up.normalized * 20f;
                bullet1.GetMovement.Set(bulletDir1, cachedbulletStats.MovementSpeed);
                bullet2.GetMovement.Set(bulletDir2, cachedbulletStats.MovementSpeed);
                
                jobSystemConnector.registerCountDown(fireCoolDown, CoolDownDone);

                canShoot = false;
            }
        }

        private void CoolDownDone(bool done)
        {
            canShoot = done;
        }
        public void OnEntityDie()
        {
            if(!canShoot)
                jobSystemConnector.forceRemoveCountDown(CoolDownDone);
        }

        public virtual void Reset()
        {
            if (!canShoot)
                jobSystemConnector.forceRemoveCountDown(CoolDownDone);
            canShoot = false;
            forceQuit = true;
        }
    }
}