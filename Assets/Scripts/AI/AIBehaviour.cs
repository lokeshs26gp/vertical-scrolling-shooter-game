using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Entity
{
    public class AIBehaviour : MonoBehaviour
    {

        [Header("connectors")]
        public GameWorldConnector gameWorldConnector;
        public JobSystemConnector jobSystemConnector;

        private EntityLevelDesign entityLevelDesign;
        private AIEntity thisEntity;
        private AIFire AIFire;
        private Locations movementInfo;
        public void Initilize(AIEntity pthisEntity, EntityLevelDesign levelDesign)
        {
            thisEntity = pthisEntity;
            entityLevelDesign = levelDesign;
            AIFire = (AIFire)thisEntity.GetFire;

            movementInfo = gameWorldConnector.GetSpawnLocations(entityLevelDesign.opponentStats.opponentType);

            jobSystemConnector.registerCountDown(entityLevelDesign.startSpawnTimeDelaySec, ActivateEntity);

            gameObject.SetActive(false);

            transform.position = movementInfo.start;

            AIFire.InitilizeFire(levelDesign.fireDelaySec);

        }


        private void ActivateEntity(bool done)
        {
            transform.position = movementInfo.start;
            //Debug.Log("Activated!");
            gameObject.SetActive(true);
            thisEntity.GetMovement.Set(movementInfo.end, entityLevelDesign.opponentStats.MovementSpeed);
            AIFire.Start();
        }

        public void Stop(bool isKill = false)
        {
            AIFire.Reset();
            gameObject.SetActive(false);
            jobSystemConnector.registerCountDown(isKill? entityLevelDesign.deathReSpawnSec: entityLevelDesign.reSpawnDelaySec, ActivateEntity);
           
        }

        public void Kill()
        {
            //Do some animation for kill
            Stop(true);
        }
    }

}