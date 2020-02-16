using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class AIFire : Fire
    {

        private float fireFequencey;
        public void InitilizeFire(float fireFeq)
        {
            fireFequencey = fireFeq;
            jobSystemConnector.registerCountDown(fireFequencey, FireBullet);
        }

        private void FireBullet(bool done)
        {
            Shoot();
            jobSystemConnector.registerCountDown(fireFequencey, FireBullet);
        }

        public override void Reset()
        {
            base.Reset();
            jobSystemConnector.forceRemoveCountDown(FireBullet);
        }

        public void Restart()
        {
            forceQuit = false;
            canShoot = true;
            jobSystemConnector.registerCountDown(fireFequencey, FireBullet);
        }

    }
}
