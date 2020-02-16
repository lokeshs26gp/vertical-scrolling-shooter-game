using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{

    public interface IFire
    {
        void Initilize(Entity fromentity, float cooldown, BulletStatsDataSet bulletStatst);
        void Shoot();

        void OnEntityDie();
        void Reset();
    }
}
