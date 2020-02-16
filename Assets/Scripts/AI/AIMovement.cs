using UnityEngine;
namespace Entity
{
    public class AIMovement : Movement
    {
        public AIEntity thisEntity;
        public override void Set(Vector2 direction,float speed)
        {
            base.Set(direction, speed);
            
        }
        public override void MovementUpdate()
        {
            
            transform.position =  Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            Vector2 direction = nextPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

            transform.rotation = rotation;

        }

        public override void Update()
        {
            if (!canMove) return;

            MovementUpdate();

            float sqDis = (transform.position - nextPosition).sqrMagnitude;
            if (sqDis < 0.1f)
            {
                thisEntity.AIBehaviour.Stop();
            }
        }

    }
}
